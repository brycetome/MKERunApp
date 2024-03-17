using static Models.ActivityType;

namespace Models
{
    public class ActivityFormModel
    {
        private readonly List<TeamGroup> selectedGroups = [];
        public IEnumerable<TeamGroup> GetSelectedGroups => selectedGroups;

        public ActivityType SelectedActivityType { get; set; } = ActivityType.GetActivityType(ActivityType.WorkoutType.Run);
        public int Minutes { get; set; }
        public string Description { get; set; } = "";
        public bool IsWorkOut { get; set; }

        public ActivityFormModel() { }

        public ActivityFormModel(Activity activity)
        {
            Minutes = (int)activity.DurationSeconds / 60;
            selectedGroups.AddRange(activity.Groups);
            Description = activity.Description;
            IsWorkOut = activity.IsWorkOut;
            if (activity.WorkoutType is WorkoutType type)
                SelectedActivityType = GetActivityType(type);
        }

        public void ApplyForm(Activity activity)
        {
            activity.DurationSeconds = Minutes * 60;
            activity.Description = Description;
            activity.WorkoutType = SelectedActivityType.Type;
            activity.IsWorkOut = IsWorkOut;
            activity.Groups.Clear();
            activity.Groups.AddRange(selectedGroups);
        }

        public void ToggleSelectedGroup(TeamGroup group)
        {
            if (!selectedGroups.Remove(group))
                selectedGroups.Add(group);
        }

        public void ResetForm()
        {
            selectedGroups.Clear();
            Minutes = 0;
            Description = "";
            SelectedActivityType = GetActivityType(WorkoutType.Run);
            IsWorkOut = false;
        }
    }
}
