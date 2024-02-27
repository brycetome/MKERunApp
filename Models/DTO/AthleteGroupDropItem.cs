using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class AthleteGroupDropItem(TeamAthlete athlete, IDbContextFactory<ApplicationDbContext> factory)
    {
        public static readonly string UnassignedGroup = "-1";
        public string Identifier => groupId?.ToString() ?? UnassignedGroup;
        public string Name => name;

        private readonly string name = athlete.User?.UserName ?? "";
        private readonly int teamId = athlete.TeamId;
        private readonly string userId = athlete.UserId;
        public int? groupId = athlete.GroupId;
        private IDbContextFactory<ApplicationDbContext> factory = factory;

        public async Task SaveGroupChange(string NewGroupIdString)
        {
            var oldGroupId = groupId;
            if (NewGroupIdString == UnassignedGroup)
                groupId = null;
            else if (int.TryParse(NewGroupIdString, out int NewGroupId))
                groupId = NewGroupId;
            else
                throw new ArgumentException("Failed to save the group");

            try
            {
                using var ctx = factory.CreateDbContext();
                var athlete = await ctx.TeamAthlete.FindAsync([userId, teamId]);
                athlete!.GroupId = groupId;
                await ctx.SaveChangesAsync();
                await ctx.DisposeAsync();
            }
            catch
            {
                groupId = oldGroupId;
                throw new Exception("Failed to save the group.");
            }
        }
    }
}
