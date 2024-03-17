﻿using static Models.ActivityType;

namespace Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime DayAndTime { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public double DurationSeconds { get; set; }
        public string Description { get; set; } = "";
        public bool IsWorkOut { get; set; }
        public List<TeamGroup> Groups { get; set; } = [];
    }
}
