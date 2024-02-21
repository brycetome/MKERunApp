using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccessServices
{
    public static class UserAccessService
    {
        public static async Task<ApplicationUser> LoadUser(
            UserManager<ApplicationUser> UserManager,
            AuthenticationStateProvider AuthenticationStateProvider)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            var userId = UserManager.GetUserId(user);
            var User = await UserManager.Users
                .Include(u => u.CoachedTeams)
                .SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new NullReferenceException("Failed to load the user.");
            return User;
        }

    }
}
