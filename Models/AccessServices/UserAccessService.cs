﻿using Microsoft.AspNetCore.Components.Authorization;
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

            await ctx.Entry(User)
                .Collection(user => user.AthleteTeams)
                .Query()
                .Include(at => at.Team)
                .Include(at => at.Group)
                .LoadAsync();

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
            user.CoachedTeams.Add(newTeam);
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to add team.");
            return newTeam;
        }

        public static async Task SetDefaultCoachedTeam(
            this IDbContextFactory<ApplicationDbContext> factory,
            ApplicationUser User,
            Team Team)
        {
            using var ctx = factory.CreateDbContext();

            ctx.Attach(User);
            User.DefaultCoachTeam = Team;

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }

        public static async Task SetDefaultAthleteTeam(
            this IDbContextFactory<ApplicationDbContext> factory,
            ApplicationUser User,
            Team Team)
        {
            using var ctx = factory.CreateDbContext();

            ctx.Attach(User);
            User.DefaultAthelteTeam = Team;

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }
    }
}
