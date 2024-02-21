using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public Team() { }

        public Team(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
