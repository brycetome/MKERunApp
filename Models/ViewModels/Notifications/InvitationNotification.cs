using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels.Notifications
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
        public override string ActionSuccessMessage => $"Accepted invitaion for {Invite.Team.Name}";

        private readonly TeamInvitation Invite = invite;
        private readonly string UserId = userId;
        private IDbContextFactory<ApplicationDbContext> Factory = factory;

        public override async Task NotificationAction()
        {
            using var ctx = Factory.CreateDbContext();
            var user = await ctx.Users.FindAsync(UserId)
                ?? throw new NullReferenceException("Failed to load the user.");

            var newTeam = new TeamAthlete()
            {
                Team = Invite.Team
            };
            user.AthleteTeams.Add(newTeam);

            var OldInvite = await ctx.TeamInvitation.FindAsync(Invite.Id);
            if (OldInvite != null)
                ctx.TeamInvitation.Remove(OldInvite);

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }

        public override async Task ClearNotification()
        {
            using var ctx = Factory.CreateDbContext();
            ctx.TeamInvitation.Remove(Invite);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
        }
    }
}
