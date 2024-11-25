using Concertible.Infrastructure.Repositories;
using Core.Entities.Identity;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Concertible.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Register(string firstName, string lastName, string email, string password)
        {
            if (CheckEmailExistsAsync(email).Result)
                throw new BadRequestException("Email already exists");

            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded) 
                throw new BadRequestException("Failed to Register User");

            await userManager.AddToRoleAsync(user, "Customer");
        }

        public async Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task<string> GetFirstUserRole(ClaimsPrincipal principal)
        {
            var user = await GetCurrentUser(principal);
            var roles = await userManager.GetRolesAsync(user);
            return roles.First();
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }
    }
}
