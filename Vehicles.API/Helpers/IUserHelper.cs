﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Helpers
{
   public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);

        Task AddUserToRole(User user, string roleName);

        Task<bool> IsUserInToleAsync(User user, string roleName);

    }
}
