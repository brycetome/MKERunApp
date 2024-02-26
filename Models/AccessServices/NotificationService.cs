using Microsoft.EntityFrameworkCore;
using Models.DTO.Notifications;

namespace Models.AccessServices
{
    public static class NotificationService
    {
        public static async Task<List<AbstractNotification>> GetNotificationsForUser(
            this IDbContextFactory<ApplicationDbContext> factory,
            string userId)
        {
            List<AbstractNotification> notifications = [];

            using var ctx = factory.CreateDbContext();
            var user = await ctx.Users
                    .Include(u => u.Invitations)
                    .ThenInclude(inv => inv.Team)
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    ?? throw new NullReferenceException("Failed to load notifications.");

            foreach (var invite in user.Invitations)
            {
                notifications.Add(new InvitationNotification(invite, user.Id, factory));
            }
            return notifications;
        }
    }
}
