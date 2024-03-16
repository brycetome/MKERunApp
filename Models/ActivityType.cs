using MudBlazor;

namespace Models
{
    public class ActivityType(ActivityType.WorkoutType type, string name, string icon)
    {
        public enum WorkoutType
        {
            None,
            Run,
            TrainingRun,
            Workout,
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


        public static ActivityType GetActivityType(WorkoutType type)
        {
            switch (type)
            {
                case WorkoutType.Run:
                    return new ActivityType(WorkoutType.Run, "Run", Icons.Material.Filled.DirectionsRun);
                case WorkoutType.TrainingRun:
                    return new ActivityType(WorkoutType.TrainingRun, "Training Run", Icons.Material.Filled.DirectionsRun);
                case WorkoutType.Workout:
                    return new ActivityType(WorkoutType.Workout, "Workout", Icons.Material.Filled.RunCircle);
                case WorkoutType.Bike:
                    return new ActivityType(WorkoutType.Bike, "Bike", Icons.Material.Filled.DirectionsBike);
                case WorkoutType.CrossTrain:
                    return new ActivityType(WorkoutType.CrossTrain, "Workout", Icons.Material.Filled.DirectionsBike);
                case WorkoutType.Swim:
                    return new ActivityType(WorkoutType.Swim, "Swim", Icons.Material.Filled.Pool);
                case WorkoutType.WarmUp:
                    return new ActivityType(WorkoutType.WarmUp, "Warm Up", Icons.Material.Filled.Whatshot);
                case WorkoutType.CoolDown:
                    return new ActivityType(WorkoutType.CoolDown, "Warm Up", Icons.Material.Filled.Thermostat);
                case WorkoutType.Strength:
                    return new ActivityType(WorkoutType.Strength, "Srength/Lift", Icons.Material.Filled.FitnessCenter);
                default:
                    break;
            }
            return new ActivityType(WorkoutType.Workout, "Activity", Icons.Material.Filled.RunCircle);
        }

    }
}
