﻿using Application.DTOs;
using Core.Entities;
using Core.Parameters;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IArtistRepository : IHeaderRepository<Artist, ArtistHeaderDto>
    {
        Task<Artist?> GetByUserIdAsync(int id);
    }
}
