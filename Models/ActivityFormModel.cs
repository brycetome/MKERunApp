using static Models.ActivityType;

namespace Models
{
    public class ActivityFormModel
    {
        private readonly List<TeamGroup> selectedGroups = [];
        public IEnumerable<TeamGroup> GetSelectedGroups => selectedGroups;

        public ActivityType SelectedActivityType { get; set; } = GetActivityType(WorkoutType.Run);
        public string? Title { get; set; }
        public int Minutes { get; set; }
        public string Description { get; set; } = "";
        public string Recovery { get; set; } = "";
        public bool IsWorkOut { get; set; }
        public bool UseUIWorkOut { get; set; }
        public string WorkoutSpecs { get; set; } = "";
        public List<WorkoutItem> WorkoutItems { get; set; } = [];

        public ActivityFormModel() { }

        public ActivityFormModel(Activity activity)
        {
            Minutes = (int)activity.DurationSeconds / 60;
            selectedGroups.AddRange(activity.Groups);
            Description = activity.Description;
            IsWorkOut = activity.IsWorkOut;
            Recovery = activity.Recovery;
            WorkoutItems.AddRange(activity.WorkoutItems);
            UseUIWorkOut = activity.UseUIWorkOut;
            WorkoutSpecs = activity.WorkoutSpecs;
            Title = activity.Title;
            if (activity.WorkoutType is WorkoutType type)
                SelectedActivityType = GetActivityType(type);
        }

        public void ApplyForm(Activity activity)
        {
            activity.DurationSeconds = Minutes * 60;
            activity.Description = Description;
            activity.WorkoutType = SelectedActivityType.Type;
            activity.IsWorkOut = IsWorkOut;
            activity.Title = Title;
            activity.Recovery = Recovery;
            activity.Groups.Clear();
            activity.Groups.AddRange(selectedGroups);
            activity.WorkoutItems.Clear();
            activity.WorkoutItems.AddRange(WorkoutItems);
            activity.UseUIWorkOut = UseUIWorkOut;
            activity.WorkoutSpecs = WorkoutSpecs;
        }

        public void ToggleSelectedGroup(TeamGroup group)
        {
            if (!selectedGroups.Remove(group))
                selectedGroups.Add(group);
        }

        public void ResetForm()
        {
            selectedGroups.Clear();
            WorkoutItems.Clear();
            Minutes = 0;
            Recovery = "";
            Description = "";
            Title = null;
            SelectedActivityType = GetActivityType(WorkoutType.Run);
            IsWorkOut = false;
        }
    }
}
