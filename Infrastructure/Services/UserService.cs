using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concertible.Infrastructure.Services
{
    internal class UserService
    {
        private readonly UserManager<ApplicationUser> userManager;
    }
}
