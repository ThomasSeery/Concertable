﻿using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<int> GetIdByApplicationIdAsync(int id);
        Task<int> GetIdByEventIdAsync(int id);
    }
}
