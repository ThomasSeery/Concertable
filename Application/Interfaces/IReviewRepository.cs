﻿using Application.DTOs;
using Core.Entities;
using Core.Parameters;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<double?> GetAverageRatingByEventIdAsync(int id);
        Task<double?> GetAverageRatingByArtistIdAsync(int id);
        Task<double?> GetAverageRatingByVenueIdAsync(int id);
        Task<ReviewSummaryDto> GetSummaryByVenueIdAsync(int id);
        Task<ReviewSummaryDto> GetSummaryByEventIdAsync(int id);
        Task<ReviewSummaryDto> GetSummaryByArtistIdAsync(int id);
        Task<PaginationResponse<Review>> GetByVenueIdAsync(int id, PaginationParams pageParams);
        Task<PaginationResponse<Review>> GetByArtistIdAsync(int id, PaginationParams pageParams);
        Task<PaginationResponse<Review>> GetByEventIdAsync(int id, PaginationParams pageParams);
        Task<bool> CanUserIdReviewEventIdAsync(int userId, int eventId);
        Task<bool> CanUserIdReviewArtistIdAsync(int userId, int artistId);
        Task<bool> CanUserIdReviewVenueIdAsync(int userId, int venueId);
    }
}
