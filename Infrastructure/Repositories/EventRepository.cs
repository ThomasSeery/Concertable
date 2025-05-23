﻿using Core.Entities;
using Application.Interfaces;
using Core.Parameters;
using Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers;
using Application.Responses;
using Application.DTOs;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using GeoCoordinatePortable;
using NetTopologySuite.Geometries;

namespace Infrastructure.Repositories
{
    public class EventRepository : HeaderRepository<Event, EventHeaderDto>, IEventRepository
    {
        public EventRepository(
            ApplicationDbContext context,
            IGeometryService geometryService
            ) : base(context, geometryService)
        {
        }

        protected override Expression<Func<Event, EventHeaderDto>> Selector => e => new EventHeaderDto
        {
            Id = e.Id,
            Name = e.Name,
            ImageUrl = e.Application.Artist.ImageUrl,
            StartDate = e.Application.Listing.StartDate,
            EndDate = e.Application.Listing.EndDate,
            County = e.Application.Listing.Venue.User.County,
            Town = e.Application.Listing.Venue.User.Town,
            Latitude = e.Application.Listing.Venue.User.Location.Y,
            Longitude = e.Application.Listing.Venue.User.Location.X,
            DatePosted = e.DatePosted
        };

        public async Task<IEnumerable<EventHeaderDto>> GetHeaders(int amount)
        {
            return await base.GetHeadersQuery(amount).Where(e => e.DatePosted != null).OrderBy(e => e.StartDate).Take(10).ToListAsync();
        }

        protected override List<Expression<Func<Event, bool>>> Filters(SearchParams searchParams) {
            var filters = new List<Expression<Func<Event, bool>>>();

            if (searchParams.Date != null)
                filters.Add(e => e.Application.Listing.StartDate >= searchParams.Date);

            if (searchParams.GenreIds != null && searchParams.GenreIds.Any())
                filters.Add(e => e.EventGenres.Any(ag => searchParams.GenreIds.Contains(ag.GenreId)));

            // Only show events that haven't passed
            if (searchParams.ShowHistory != true)
                filters.Add(e => e.Application.Listing.StartDate >= DateTime.UtcNow);

            if (searchParams.ShowSold != true)
                filters.Add(e => e.AvailableTickets > 0);

            return filters;
        }

        // Event needs to be able to sort by Date Posted as well as name
        protected override IQueryable<Event> ApplyOrdering(IQueryable<Event> query, string? sort)
        {
            return sort?.ToLower() switch
            {
                "name_asc" => query.OrderBy(e => e.Name),
                "name_desc" => query.OrderByDescending(e => e.Name),
                "date_asc" => query.OrderBy(e => e.Application.Listing.StartDate),
                "date_desc" => query.OrderByDescending(e => e.Application.Listing.StartDate),
                _ => query.OrderBy(e => e.Application.Listing.StartDate)
            };
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            var query = context.Events
            .Where(e => e.Id == id)
            .Include(e => e.Application)
                .ThenInclude(la => la.Artist)
                    .ThenInclude(a => a.User)
            .Include(e => e.Application)
                .ThenInclude(la => la.Artist)
                    .ThenInclude(a => a.ArtistGenres)
                        .ThenInclude(ag => ag.Genre)
            .Include(e => e.Application.Listing)
                .ThenInclude(l => l.Venue)
                    .ThenInclude(v => v.User)
            .Include(e => e.EventGenres)
                .ThenInclude(eg => eg.Genre);

            return await query.FirstAsync();
        }

        protected IQueryable<EventHeaderDto> GetHeadersQuery(int userId, EventParams eventParams)
        {
            var query = context.Events.AsQueryable();

            query = ApplyFilters(query);
            query = ApplyOrdering(query);

            query = query.Include(e => e.Application.Listing.Venue.User)
                         .Include(e => e.EventGenres);

            query = query.Include(e => e.Application.Listing.Venue.User)
                         .Include(e => e.EventGenres);

            if (eventParams.GenreIds.Any())
                query = query.Where(e =>
                    e.EventGenres.Any(eg => eventParams.GenreIds.Contains(eg.GenreId)));

            if (eventParams.OrderByRecent)
                query = query.OrderByDescending(e => e.DatePosted);

            if (GeoHelper.HasValidCoordinates(eventParams))
                query = FilterByRadius(query, eventParams.Latitude!.Value, eventParams.Longitude!.Value, eventParams.RadiusKm ?? 10);

            return query.Select(Selector).Take(10);
        }

        public async Task<IEnumerable<EventHeaderDto>> GetHeaders(int userId, EventParams eventParams)
        {
            var query = GetHeadersQuery(userId, eventParams);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUpcomingByVenueIdAsync(int id)
        {
            var query = context.Events // Get all events
                .Where(e => e.Application.Listing.VenueId == id // That are associated with the venue
                            && e.Application.Listing.StartDate >= DateTime.UtcNow // And event hasn't passed yet
                            && e.DatePosted != null) // And has been posted
                .Include(e => e.Application)
                    .ThenInclude(a => a.Listing) // To retrieve the start date
                 .Include(e => e.Application.Listing.Venue)
                    .ThenInclude(v => v.User)
                 .Include(e => e.Application.Artist)
                    .ThenInclude(a => a.User);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUpcomingByArtistIdAsync(int id)
        {
            var query = context.Events // Get all events
                .Where(e => e.Application.ArtistId == id // That are associated with the artist
                            && e.Application.Listing.StartDate >= DateTime.UtcNow // And event hasn't passed yet
                            && e.DatePosted != null) // And has been posted
                .Include(e => e.Application)
                    .ThenInclude(a => a.Listing) // To retrieve the start date
                .Include(e => e.Application.Listing.Venue)
                    .ThenInclude(v => v.User)
                 .Include(e => e.Application.Artist)
                    .ThenInclude(a => a.User);

            return await query.ToListAsync();
        }


        public async Task<Event?> GetByApplicationIdAsync(int applicationId)
        {
            var query = context.Events
                .Where(e => e.ApplicationId == applicationId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetHistoryByArtistIdAsync(int id)
        {
            var query = context.Events // Get all events
            .Where(e => e.Application.ArtistId == id // That are associated with the artist
                        && e.Application.Listing.StartDate < DateTime.UtcNow // That have already started
                        && e.DatePosted != null) // And have been posted
            .Include(e => e.Application)
                .ThenInclude(a => a.Listing) // To retrieve start/end dates
             .Include(e => e.Application.Listing.Venue)
                .ThenInclude(v => v.User)
             .Include(e => e.Application.Artist)
                .ThenInclude(a => a.User);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetHistoryByVenueIdAsync(int id)
        {
            var query = context.Events // Get all events
            .Where(e => e.Application.Listing.VenueId == id // That are associated with the venue
                        && e.Application.Listing.StartDate < DateTime.UtcNow // That have already started
                        && e.DatePosted != null) // And have been posted
            .Include(e => e.Application)
                .ThenInclude(a => a.Listing) // To retrieve start/end dates
             .Include(e => e.Application.Listing.Venue)
                .ThenInclude(v => v.User)
             .Include(e => e.Application.Artist)
                .ThenInclude(a => a.User);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUnpostedByArtistIdAsync(int id)
        {
            return await context.Events
                .Where(e => e.Application.ArtistId == id && e.DatePosted == null)
                .Include(e => e.Application.Listing)
                .Include(e => e.Application.Listing.Venue)
                    .ThenInclude(v => v.User)
                 .Include(e => e.Application.Artist)
                    .ThenInclude(a => a.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUnpostedByVenueIdAsync(int id)
        {
            return await context.Events
                .Where(e => e.Application.Listing.VenueId == id && e.DatePosted == null)
                .Include(e => e.Application.Listing)
                .Include(e => e.Application.Listing.Venue)
                    .ThenInclude(v => v.User)
                 .Include(e => e.Application.Artist)
                    .ThenInclude(a => a.User)
                .ToListAsync();
        }

        public async Task<bool> ArtistHasEventOnDateAsync(int artistId, DateTime date)
        {
            return await context.Events
                .Where(e => e.Application.ArtistId == artistId)
                .AnyAsync(e => e.Application.Listing.StartDate.Date == date.Date);
        }

        public Task<bool> ListingHasEventAsync(int listingId)
        {
            return context.Events
                .AnyAsync(e => e.Application.ListingId == listingId);
        }

        protected override IQueryable<Event> FilterByRadius(IQueryable<Event> query, double latitude, double longitude, double radiusKm)
        {
            var center = geometryService.CreatePoint(latitude, longitude);

            query = query.Where(e =>
                e.Application.Listing.Venue.User.Location != null &&
                e.Application.Listing.Venue.User.Location.Distance(center) <= (radiusKm * 1000));

            return query;
        }

        private IQueryable<Event> GetDefaultHeadersQuery()
        {
            return context.Events
                .Where(e => e.DatePosted != null)
                .Where(e => e.Application.Listing.EndDate > DateTime.UtcNow)
                .OrderBy(e => e.Application.Listing.StartDate); 
        }

        protected override IQueryable<Event> ApplyFilters(IQueryable<Event> query)
        {
            return query
                .Where(e => e.DatePosted != null)
                .Where(e => e.Application.Listing.EndDate > DateTime.UtcNow);
        }

        protected override IQueryable<Event> ApplyOrdering(IQueryable<Event> query)
        {
            return query.OrderBy(e => e.Application.Listing.StartDate);
        }

        public async Task<bool> VenueHasEventOnDateAsync(int venueId, DateTime date)
        {
            return await context.Events
                .Where(e => e.Application.Listing.VenueId == venueId) 
                .AnyAsync(e => e.Application.Listing.StartDate.Date == date.Date);
        }
    }
}