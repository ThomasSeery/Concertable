﻿using Infrastructure.Repositories;
using Core.Exceptions;
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
using Core.Interfaces;
using Core.Entities.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task Register(string email, string password)
        {
            if (CheckEmailExistsAsync(email).Result)
                throw new BadRequestException("Email already exists");

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded) 
                throw new BadRequestException("Failed to Register User");

            await userManager.AddToRoleAsync(user, "Customer");
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser?> GetCurrentUser(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task<string> GetFirstUserRole(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.First();
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }
    }
}
