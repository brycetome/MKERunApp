using MudBlazor;

namespace Models
{
    public class ActivityType(ActivityType.WorkoutType type, string name, string icon, string color, int order)
    {
        public enum WorkoutType
        {
            None = 0,
            Run = 1,
            Bike = 2,
            CrossTrain = 3,
            Swim = 4,
            WarmUp = 5,
            CoolDown = 6,
            Strength = 7,
            Hills = 8,
            LongRun = 9,
            Double =10,
        }

        public string Name => name;
        public string Icon => icon;
        public string Color => color;
        public WorkoutType Type => type;
        public int Order => order;

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
                    return new ActivityType(WorkoutType.Run, "Run", Icons.Material.Filled.DirectionsRun, "", 1);
                case WorkoutType.Bike:
                    return new ActivityType(WorkoutType.Bike, "Bike", Icons.Material.Filled.DirectionsBike, Colors.Orange.Lighten1, 4);
                case WorkoutType.CrossTrain:
                    return new ActivityType(WorkoutType.CrossTrain, "Cross Train", Icons.Material.Filled.DirectionsBike, Colors.Orange.Darken1, 3);
                case WorkoutType.Swim:
                    return new ActivityType(WorkoutType.Swim, "Swim", Icons.Material.Filled.Pool, Colors.Cyan.Lighten4, 7);
                case WorkoutType.WarmUp:
                    return new ActivityType(WorkoutType.WarmUp, "Warm Up", Icons.Material.Filled.Whatshot, Colors.DeepOrange.Default, 2);
                case WorkoutType.CoolDown:
                    return new ActivityType(WorkoutType.CoolDown, "Cool Down", Icons.Material.Filled.Thermostat, Colors.Cyan.Lighten3, 8);
                case WorkoutType.Strength:
                    return new ActivityType(WorkoutType.Strength, "Srength/Lift", Icons.Material.Filled.FitnessCenter, Colors.Blue.Default, 9);
                case WorkoutType.Hills:
                    return new ActivityType(WorkoutType.Hills, "Hills", Icons.Material.Filled.WifiChannel, Colors.DeepPurple.Default, 10);
                case WorkoutType.LongRun:
                    return new ActivityType(WorkoutType.LongRun, "Long Run", Icons.Material.Filled.DirectionsRun, Colors.Yellow.Default, 5);
                case WorkoutType.Double:
                    return new ActivityType(WorkoutType.Double, "Double", Icons.Material.Filled.DirectionsRun, Colors.Green.Darken1, 6);
                default:
                    break;
            }
            return new ActivityType(WorkoutType.None, "Activity", Icons.Material.Filled.RunCircle, "", 0);
        }

    }
}
