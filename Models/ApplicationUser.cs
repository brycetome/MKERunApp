using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Team> CoachedTeams { get; set; } = [];
        public List<TeamAthlete> AthleteTeams { get; set; } = [];
        public List<TeamInvitation> Invitations { get; set; } = [];
        public List<AthleteActivityReport> ActivityReports { get; set; } = [];

        public Team? DefaultAthelteTeam { get; set; }
        public int? DefaultAthelteTeamId { get; set; }
        public Team? DefaultCoachTeam { get; set; }
        public int? DefaultCoachTeamId { get; set; }

    }
}
