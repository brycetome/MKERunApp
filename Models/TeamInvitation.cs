namespace Models
{
    public class TeamInvitation
    {
        public int Id { get; set; }
        public Team Team { get; set; } = default!;

        public TeamInvitation() { }

        public TeamInvitation(Team team)
        {
            Team = team;
        }
    }
}
