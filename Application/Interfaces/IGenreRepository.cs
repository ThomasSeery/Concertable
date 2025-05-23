﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenreRepository: IRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<IEnumerable<Genre>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
