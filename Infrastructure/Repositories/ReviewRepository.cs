﻿using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Parameters;
using Application.Responses;
using Infrastructure.Data.Identity;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) { }

        public Task<ReviewSummaryDto> GetSummaryByArtistIdAsync(int id)
        {
            return GetSummaryAsync(r => r.Ticket.Event.Application.ArtistId == id);
        }

        public Task<ReviewSummaryDto> GetSummaryByEventIdAsync(int id)
        {
            return GetSummaryAsync(r => r.Ticket.EventId == id);
        }

        public Task<ReviewSummaryDto> GetSummaryByVenueIdAsync(int id)
        {
            return GetSummaryAsync(r => r.Ticket.Event.Application.Listing.VenueId == id);
        }


        public Task<double?> GetAverageRatingByArtistIdAsync(int id)
        {
            return GetAverageRatingAsync(r => r.Ticket.Event.Application.ArtistId == id);
        }

        public Task<double?> GetAverageRatingByEventIdAsync(int id)
        {
            return GetAverageRatingAsync(r => r.Ticket.EventId == id);
        }

        public Task<double?> GetAverageRatingByVenueIdAsync(int id)
        {
            return GetAverageRatingAsync(r => r.Ticket.Event.Application.Listing.VenueId == id);
        }

        private async Task<double?> GetAverageRatingAsync(Expression<Func<Review, bool>> filter)
        {
            var query = context.Reviews.Where(filter);

            return (await query.AverageAsync(r => (double?)r.Stars)) is double avg
                ? Math.Round(avg, 1)
                : null;
        }

        private async Task<ReviewSummaryDto> GetSummaryAsync(Expression<Func<Review, bool>> filter)
        {
            var query = context.Reviews.Where(filter);

            var averageRating = await query.AverageAsync(r => (double?)r.Stars);
            var totalReviews = await query.CountAsync();

            return new ReviewSummaryDto
            {
                AverageRating = averageRating is double avg ? Math.Round(avg, 1) : null,
                TotalReviews = totalReviews
            };
        }


        public async Task<PaginationResponse<Review>> GetByArtistIdAsync(int id, PaginationParams pageParams)
        {
            var query = context.Reviews
                .Include(r => r.Ticket)
                .ThenInclude(t => t.User)
                .Where(r => r.Ticket.Event.Application.ArtistId == id);

            return await PaginationHelper.CreatePaginatedResponseAsync(query, pageParams);
        }

        public async Task<PaginationResponse<Review>> GetByEventIdAsync(int id, PaginationParams pageParams)
        {
            var query = context.Reviews
                .Include(r => r.Ticket)
                .ThenInclude(t => t.User)
                .Where(r => r.Ticket.EventId == id);

            return await PaginationHelper.CreatePaginatedResponseAsync(query, pageParams);
        }

        public async Task<PaginationResponse<Review>> GetByVenueIdAsync(int id, PaginationParams pageParams)
        {
            var query = context.Reviews
                .Include(r => r.Ticket)
                .ThenInclude(t => t.User)
                .Where(r => r.Ticket.Event.Application.Listing.VenueId == id);

            return await PaginationHelper.CreatePaginatedResponseAsync(query, pageParams);
        }
    }
}
