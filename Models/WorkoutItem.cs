using static Models.ActivityType;

namespace Models
{
    public class WorkoutItem
    {
        public int Id { get; set; }
        public string Time { get; set; } = "";
        public int? Distance { get; set; }
        public string DistanceMeasurment { get; set; } = "none";
        public WorkoutType WorkoutType { get; set; }
        public int Order { get; set; }
    }
}
