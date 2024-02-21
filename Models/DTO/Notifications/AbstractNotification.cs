namespace Models.DTO.Notifications
{
    public abstract class AbstractNotification
    {
        public abstract string Title { get; }
        public abstract string Description { get; }

        public virtual string ClearTitle => "Dismiss";
        public abstract string ActionTitle { get; }

        public abstract Task Clear();
        public abstract Task Action();
    }
}
