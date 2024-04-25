using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<TeamAthlete> TeamAthlete { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamGroup> TeamGroup { get; set; }
        public DbSet<TeamInvitation> TeamInvitation { get; set; }
        public DbSet<WorkoutItem> WorkoutItem { get; set; }
        public DbSet<AthleteActivityReport> Reports { get; set; }

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

            modelBuilder.Entity<Team>()
                .HasMany(team => team.Coaches)
                .WithMany(coach => coach.CoachedTeams);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.DefaultAthelteTeam)
                .WithMany()
                .HasForeignKey(u => u.DefaultAthelteTeamId);


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.DefaultCoachTeam)
                .WithMany()
                .HasForeignKey(u => u.DefaultCoachTeamId);

            modelBuilder.Entity<TeamGroup>()
                .HasMany(group => group.Athletes)
                .WithOne(ath => ath.Group)
                .HasForeignKey(ath => ath.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TeamGroup>()
                .HasMany(group => group.Activities)
                .WithMany(activity => activity.Groups);

            modelBuilder.Entity<Activity>()
                .HasMany(act => act.ActivityReports)
                .WithOne(r => r.Activity)
                .HasForeignKey(r => r.ActivityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Activity>()
                .HasMany(act => act.WorkoutItems)
                .WithOne(r => r.Activity)
                .HasForeignKey(r => r.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AthleteActivityReport>()
                .HasKey(r => new { r.UserId, r.ActivityId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
