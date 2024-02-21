using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.Notifications
{
    public class InvitationNotification(
        TeamInvitation invite,
        string userId,
        IDbContextFactory<ApplicationDbContext> factory)
        : AbstractNotification
    {
        public override string Title => $"You were invited to {Invite.Team.Name}";
        public override string Description => "Chose an option.";

        public override string ClearTitle => "Deny";
        public override string ActionTitle => "Accept";

        private TeamInvitation Invite = invite;
        private string UserId = userId;
        private IDbContextFactory<ApplicationDbContext> Factory = factory;

        public override async Task Action()
        {
            using var ctx = Factory.CreateDbContext();
            var user = await ctx.Users.FindAsync(UserId)
                ?? throw new NullReferenceException("Failed to load the user.");
            var newTeam = new TeamAthlete()
            {
                User = user,
                Team = Invite.Team
            };
            user.AthleteTeams.Add(newTeam);
            ctx.TeamInvitation.Remove(Invite);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }

        public override async Task Clear()
        {
            using var ctx = Factory.CreateDbContext();
            ctx.TeamInvitation.Remove(Invite);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }
    }
}
