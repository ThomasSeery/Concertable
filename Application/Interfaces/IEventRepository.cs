﻿using Core.Entities;
using Core.Parameters;
using Core.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingByVenueIdAsync(int id);
        Task<IEnumerable<Event>> GetUpcomingByArtistIdAsync(int id);
        Task<PaginationResponse<Event>> GetHeadersAsync(SearchParams searchParams);
    }
}
