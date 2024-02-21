using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AccessServices
{
    public static class InvitationService
    {

        public static async Task SendInvite(this UserManager<ApplicationUser> UserManager, Team team, string email)
        {
            var user = await UserManager.FindByEmailAsync(email)
                ?? throw new NullReferenceException("Could not find user's email.");

            var invitation = new TeamInvitation(team);
            user.Invitations.Add(invitation);
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to invite the user.");
        }
    }
}
