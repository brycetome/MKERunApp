using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Models
{
    public class ActivityFormModel
    {
        private readonly List<TeamGroup> selectedGroups = [];
        public IEnumerable<TeamGroup> GetSelectedGroups => selectedGroups;

        public ActivityType? SelectedActivityType { get; set; } = ActivityType.GetActivityType(ActivityType.WorkoutType.Run);
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
            if (activity.WorkoutType is ActivityType.WorkoutType type)
                SelectedActivityType = ActivityType.GetActivityType(type);
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
            SelectedActivityType = ActivityType.GetActivityType(ActivityType.WorkoutType.Run);
            IsWorkOut = false;
        }
    }
}
