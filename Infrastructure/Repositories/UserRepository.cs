﻿using Concertible.Core.Interfaces;
using Concertible.Data;
using Concertible.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concertible.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
    }
}
