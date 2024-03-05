using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.DTO
{
    public class CalendarDayActivities
    {
        public IEnumerable<Activity> GetActivities => activities;
        public IEnumerable<TeamGroup> GetGroups => groups;
        public IEnumerable<TeamGroup> GetSelectedGroups => selectedGroups;
        public DateTime GetDay => day;

        private List<Activity> activities;
        private readonly IEnumerable<TeamGroup> groups;
        private readonly List<TeamGroup> selectedGroups = [];
        private DateTime day;
        private readonly IDbContextFactory<ApplicationDbContext> factory;

        public static async Task<CalendarDayActivities> CreateDayFromTeam(IDbContextFactory<ApplicationDbContext> Factory, Team Team)
        {
            using var ctx = Factory.CreateDbContext();
            var team = await ctx.Team
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(t => t.Id == Team.Id);
            await ctx.DisposeAsync();
            var today = DateTime.UtcNow;
            var dayActivities = new CalendarDayActivities(team?.Groups ?? [], today, Factory);
            await dayActivities.ChangeDay(today);
            return dayActivities;
        }


        public CalendarDayActivities(
            IEnumerable<TeamGroup> Groups,
            DateTime Day,
            IDbContextFactory<ApplicationDbContext> Factory)
        {
            groups = Groups;
            day = Day;
            activities = Groups
                 .SelectMany(g => g.Activities)
                 .Where(act => act.DayAndTime.Date == day.Date.ToUniversalTime())
                 .GroupBy(x => x.Id)
                 .Select(g => g.First())
                 .ToList();
            factory = Factory;
        }

        public IEnumerable<Activity> GetActivitiesForGroup(TeamGroup Group)
        {
            return activities.Where(act => act.Groups.Any(g => g.Id == Group.Id));
        }

        public async Task ChangeDay(DateTime Day)
        {
            day = Day;
            using var ctx = factory.CreateDbContext();
            activities.Clear();
            foreach (TeamGroup group in groups)
            {
                var loadedGroup = await ctx.TeamGroup.FindAsync(group.Id);
                if (loadedGroup is not null)
                {
                    var activitesForGroups = await ctx.Entry(loadedGroup)
                        .Collection(g => g.Activities)
                        .Query()
                        .Include(act => act.Groups)
                        .Where(act => act.DayAndTime.Date == day.Date)
                        .ToListAsync();
                    activities.AddRange(activitesForGroups);
                }
            }
            activities = activities
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();
            await ctx.DisposeAsync();
        }

        public void ToggleSelectedGroup(TeamGroup group)
        {
            if (!selectedGroups.Remove(group))
                selectedGroups.Add(group);
        }

        public async Task AddActivityWithMinutes(int Minutes)
        {
            var newActivity = new Activity()
            {
                DayAndTime = day.ToUniversalTime(),
                DurationSeconds = Minutes * 60
            };

            using var ctx = factory.CreateDbContext();

            foreach (var group in selectedGroups)
            {
                var loadedGroup = await ctx.TeamGroup.FindAsync(group.Id) ?? throw new NullReferenceException();
                loadedGroup.Activities.Add(newActivity);
            }

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            activities.Add(newActivity);
            selectedGroups.Clear();
        }
    }
}
