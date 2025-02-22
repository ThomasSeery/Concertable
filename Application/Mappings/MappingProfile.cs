﻿using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Core.Responses;
using System.Diagnostics;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Venue
            CreateMap<VenueDto, Venue>();

            CreateMap<Venue, VenueHeaderDto>();
            CreateMap<VenueHeaderDto, Venue>();
            CreateMap<Venue, VenueDto>()
            .ForMember(
                dest => dest.Latitude,
                opt => opt.MapFrom(src => src.User.Latitude))
            .ForMember(
                dest => dest.Longitude,
                opt => opt.MapFrom(src => src.User.Longitude)
            )
            .ForMember(
                dest => dest.County,
                opt => opt.MapFrom(src => src.User.County))
            .ForMember(
                dest => dest.Town,
                opt => opt.MapFrom(src => src.User.Town));

            //Artist
            CreateMap<Artist, ArtistDto>()
            .ForMember(
                dest => dest.Genres,
                opt => opt.MapFrom(src => src.ArtistGenres.Select(ag => ag.Genre.Name))
            )
            .ForMember(
                dest => dest.County,
                opt => opt.MapFrom(src => src.User.County))
            .ForMember(
                dest => dest.Town,
                opt => opt.MapFrom(src => src.User.Town));
            CreateMap<CreateVenueDto, Venue>();

            CreateMap<ArtistDto, Artist>();
            CreateMap<Artist, ArtistHeaderDto>();
            CreateMap<ArtistHeaderDto, Artist>();
            //Listing
            CreateMap<Listing, ListingDto>();
            CreateMap<ListingDto, Listing>();
            CreateMap<Listing, ListingDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(
                src => src.ListingGenres.Select(lg => new GenreDto { Name = lg.Genre.Name })));

            //Events
            CreateMap<EventDto, Event>();
            CreateMap<Event, EventHeaderDto>();
            CreateMap<EventHeaderDto, Event>();
            CreateMap<Event, EventDto>()
            .ForMember(
                dest => dest.StartDate,
                opt => opt.MapFrom(src => src.Application.Listing.StartDate)
            )
            .ForMember(
                dest => dest.EndDate,
                opt => opt.MapFrom(src => src.Application.Listing.EndDate)
            )
            .ForMember(
                dest => dest.Venue,
                opt => opt.MapFrom(src => src.Application.Listing.Venue)
            )
            .ForMember(
                dest => dest.Artist,
                opt => opt.MapFrom(src => src.Application.Artist == null ? null : src.Application.Artist)
            );


            //Messages
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();

            //MessageSummary

            //Purchase
            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();

            //Genre
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            //ListingApplications
            CreateMap<ListingApplication, ListingApplicationDto>()
            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist));

            //Reviews
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Email, opt =>  opt.MapFrom(src => src.Ticket.User.Email));
            CreateMap<ReviewDto, Review>();

        }
    }
}
