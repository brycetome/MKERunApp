using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamInvitation> TeamInvitation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TeamAthlete>()
                .HasKey(at => new { at.UserId, at.TeamId });

            modelBuilder.Entity<TeamAthlete>()
                .HasOne(at => at.User)
                .WithMany(u => u.AthleteTeams)
                .HasForeignKey(at => at.UserId);

            modelBuilder.Entity<TeamAthlete>()
                .HasOne(at => at.Team)
                .WithMany(t => t.Athletes)
                .HasForeignKey(at => at.TeamId);

            modelBuilder.Entity<Team>()
                .HasMany(team => team.Groups)
                .WithOne(group => group.Team)
                .HasForeignKey(grop => grop.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeamGroup>()
                .HasMany(group => group.Athletes)
                .WithOne(ath => ath.Group)
                .HasForeignKey(ath => ath.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
