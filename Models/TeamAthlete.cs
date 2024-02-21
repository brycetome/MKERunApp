using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TeamAthlete
    {
        public string UserId { get; set; } = "";
        public int TeamId { get; set; }

        public Team Team { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
    }
}
