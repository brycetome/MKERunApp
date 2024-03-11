namespace Models
{
    public class TeamInvitation
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; } = default!;

        public TeamInvitation() { }

        public TeamInvitation(Team team)
        {
            TeamId = team.Id;
        }
    }
}
