﻿using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        public Task Register(string email, string password);

        public Task Logout();

        public Task<ApplicationUser?> GetCurrentUser(ClaimsPrincipal principal);

        public Task<string> GetFirstUserRole(ApplicationUser user);
    }
}
