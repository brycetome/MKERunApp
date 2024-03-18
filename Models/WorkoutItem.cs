using static Models.ActivityType;

namespace Models
{
    public class WorkoutItem
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Measurement { get; set; } = "min";
        public WorkoutType WorkoutType { get; set; }
        public int Order { get; set; }
    }
}
