using MudBlazor;

namespace Models
{
    public class ActivityType(ActivityType.WorkoutType type, string name, string icon)
    {
        public enum WorkoutType
        {
            None,
            Run,
            Bike,
            CrossTrain,
            Swim,
            WarmUp,
            CoolDown,
            Strength,
        }

        public string Name => name;
        public string Icon => icon;
        public WorkoutType Type => type;

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<ActivityType> GetActivityTypes()
        {
            return ((WorkoutType[])Enum.GetValues(typeof(WorkoutType))).Select(GetActivityType);
        }

        public static ActivityType GetActivityType(WorkoutType type)
        {
            switch (type)
            {
                case WorkoutType.Run:
                    return new ActivityType(WorkoutType.Run, "Run", Icons.Material.Filled.DirectionsRun);
                case WorkoutType.Bike:
                    return new ActivityType(WorkoutType.Bike, "Bike", Icons.Material.Filled.DirectionsBike);
                case WorkoutType.CrossTrain:
                    return new ActivityType(WorkoutType.CrossTrain, "Cross Train", Icons.Material.Filled.DirectionsBike);
                case WorkoutType.Swim:
                    return new ActivityType(WorkoutType.Swim, "Swim", Icons.Material.Filled.Pool);
                case WorkoutType.WarmUp:
                    return new ActivityType(WorkoutType.WarmUp, "Warm Up", Icons.Material.Filled.Whatshot);
                case WorkoutType.CoolDown:
                    return new ActivityType(WorkoutType.CoolDown, "Cool Down", Icons.Material.Filled.Thermostat);
                case WorkoutType.Strength:
                    return new ActivityType(WorkoutType.Strength, "Srength/Lift", Icons.Material.Filled.FitnessCenter);
                default:
                    break;
            }
            return new ActivityType(WorkoutType.None, "Activity", Icons.Material.Filled.RunCircle);
        }

    }
}
