using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Models.AccessServices
{
    public static class UserAccessService
    {
        public static async Task<ApplicationUser> LoadUser(
            this IDbContextFactory<ApplicationDbContext> factory,
            UserManager<ApplicationUser> UserManager,
            AuthenticationStateProvider AuthenticationStateProvider)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            using var ctx = factory.CreateDbContext();

            var userId = UserManager.GetUserId(user);
            var User = await ctx.Users
                .Include(u => u.CoachedTeams)
                .Include(u => u.AthleteTeams)
                .ThenInclude(at => at.Team)
                .Include(u => u.Invitations)
                .ThenInclude(inv => inv.Team)
                .SingleOrDefaultAsync(u => u.Id == userId)
                ?? throw new NullReferenceException("Failed to load the user.");
            return User;
        }

        public static async Task<Team> AddCoahedTeam(
            this UserManager<ApplicationUser> UserManager,
            ClaimsPrincipal UserClaim,
            string teamName)
        {
            var newTeam = new Team(teamName);
            var user = await UserManager.GetUserAsync(UserClaim)
                ?? throw new NullReferenceException("Failed to load user.");
            user.CoachedTeams.Add(new Team(teamName));
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to add team.");
            return newTeam;
        }
    }
}
