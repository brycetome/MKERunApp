﻿namespace Models
{
    public class TeamGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int TeamId { get; set; }
        public Team Team { get; set; } = default!;


        public List<TeamAthlete> Athletes { get; set; } = [];
        public List<Activity> Activities { get; set; } = [];
    }
}
