﻿namespace Models
{
    public class Activity
    {
        public int Id { get; set; }
        public DateTime DayAndTime { get; set; }
        public ActivityType? ActivityType { get; set; }
        public double DurationSeconds { get; set; }
    }
}