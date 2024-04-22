using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AthleteActivityReport
    {
        public Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
