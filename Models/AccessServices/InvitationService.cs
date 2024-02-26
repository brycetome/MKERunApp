using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Models.AccessServices
{
    public static class InvitationService
    {
        public static async Task SendInvite(this UserManager<ApplicationUser> UserManager, Team team, string email)
        {

            var user = await UserManager.Users
                .Include(u => u.AthleteTeams)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new NullReferenceException($"Could not find user email {email}.");

            if (user.AthleteTeams.Any(at => at.TeamId == team.Id))
                throw new Exception($"Athlete already has an invitation for {team.Name}.");

            var invitation = new TeamInvitation(team);
            user.Invitations.Add(invitation);
            var result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Failed to invite the user.");
        }
    }
}
