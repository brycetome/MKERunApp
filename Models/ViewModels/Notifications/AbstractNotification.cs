﻿namespace Models.ViewModels.Notifications
{
    public abstract class AbstractNotification
    {
        public abstract string Title { get; }
        public abstract string Description { get; }

        public virtual string ClearTitle => "Dismiss";
        public abstract string ActionTitle { get; }

        public abstract string ActionSuccessMessage { get; }

        public abstract Task ClearNotification();
        public abstract Task NotificationAction();
    }
}
