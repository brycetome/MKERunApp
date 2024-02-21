using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Team> CoachedTeams { get; set; } = [];
    }
}
