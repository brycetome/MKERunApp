using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccessServices
{
    public static class TeamAccessService
    {
        public static async Task<Team> LoadTeam(this IDbContextFactory<ApplicationDbContext> factory, int teamId)
        {
            using var ctx = factory.CreateDbContext();
            var team = await ctx.Team
                .Include(t => t.Athletes)
                .ThenInclude(ath => ath.User)
                .Include(t => t.Groups)
                .ThenInclude(g => g.Activities)
                .ThenInclude(act => act.WorkoutItems)
                .FirstOrDefaultAsync(t => t.Id == teamId)
                ?? throw new NullReferenceException("Could not find the team.");

            await ctx.Entry(team).Collection(t => t.Groups).Query()
                .Include(g => g.Activities)
                .ThenInclude(act => act.ActivityReports)
                .LoadAsync();

            await ctx.DisposeAsync();
            return team;
        }

        public static async Task<TeamGroup> CreateGroupInTeam(this IDbContextFactory<ApplicationDbContext> factory, string GroupName, int TeamId)
        {
            using var ctx = factory.CreateDbContext();
            var teamGroup = new TeamGroup()
            {
                Name = GroupName,
            };
            var team = await ctx.Team.FindAsync(TeamId)
                ?? throw new NullReferenceException("Could not find the team.");
            team.Groups.Add(teamGroup);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            return teamGroup;
        }
    }
}
