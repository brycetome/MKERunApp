using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Team> CoachedTeams { get; set; } = [];
        public List<TeamAthlete> AthleteTeams { get; set; } = [];
        public List<TeamInvitation> Invitations { get; set; } = [];

/*        public Team? DefaultAthelteTeam { get; set; }
        public Team? DefaultCoachTeam { get; set; }*/

    }
}
