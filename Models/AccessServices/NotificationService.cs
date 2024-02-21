using Microsoft.EntityFrameworkCore;
using Models.DTO.Notifications;

namespace Models.AccessServices
{
    public static class NotificationService
    {
        public static List<AbstractNotification> GetNotifications(
            this ApplicationUser user,
            IDbContextFactory<ApplicationDbContext> factory)
        {
            List<AbstractNotification> notifications = [];
            foreach(var invite in user.Invitations)
            {
                notifications.Add(new InvitationNotification(invite, user.Id, factory));
            }
            return notifications;
        }
    }
}
