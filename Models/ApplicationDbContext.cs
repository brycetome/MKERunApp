using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Team> Team { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
