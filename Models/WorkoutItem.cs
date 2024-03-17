using static Models.ActivityType;

namespace Models
{
    public class WorkoutItem
    {
        public int Id { get; set; }
        public int DurationSeconds { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}
