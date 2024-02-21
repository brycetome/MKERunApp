using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Team> CoachedTeams { get; set; } = [];
    }
}
