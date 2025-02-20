﻿using Core.Entities;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;

namespace Infrastructure.Services
{
    public class ListingApplicationService : IListingApplicationService
    {
        private readonly IListingApplicationRepository applicationRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthService authService;
        private readonly IMessageService messageService;
        private readonly IListingService listingService;
        private readonly IArtistService artistService;
        private readonly IMapper mapper;

        public ListingApplicationService(
            IListingApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IMessageService messageService,
            IListingService listingService,
            IArtistService artistService,
            IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.unitOfWork = unitOfWork;
            this.authService = authService;
            this.messageService = messageService;
            this.listingService = listingService;
            this.artistService = artistService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ListingApplicationDto>> GetAllForListingIdAsync(int id)
        {
            var applications = await applicationRepository.GetAllForListingIdAsync(id);

            return mapper.Map<IEnumerable<ListingApplicationDto>>(applications);
        }

        public async Task ApplyForListingAsync(int listingId)
        {
            // Prepare data for Service calls
            var artistDto = await artistService.GetDetailsForCurrentUserAsync();
            var application = new ListingApplication()
            {
                ListingId = listingId,
                ArtistId = artistDto.Id,
            };
            var user = await authService.GetCurrentUserAsync();
            var listingOwner = await listingService.GetOwnerByIdAsync(listingId);

            var applicationRepository = unitOfWork.GetRepository<ListingApplication>();

            // Add to application table
            await applicationRepository.AddAsync(application);

            // Send message to venue owner
            await messageService.SendAsync(
                fromUserId: user.Id, 
                toUserId: listingOwner.Id,
                content: $"{user.Email} has applied to your listing",
                action: "application");

            // Save changes after both have executed
            await unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the Artist and Venue from the ListingApplicationId Associated with it
        /// </summary>
        public async Task<(ArtistDto, VenueDto)> GetArtistAndVenueByIdAsync(int id)
        {
            var (artist, venue) = await applicationRepository.GetArtistAndVenueByIdAsync(id);

            return (mapper.Map<ArtistDto>(artist), mapper.Map<VenueDto>(venue));
        }
    }
}
