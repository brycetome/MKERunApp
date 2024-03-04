using Microsoft.EntityFrameworkCore;
using System;
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

        private readonly List<Activity> activities;
        private readonly IEnumerable<TeamGroup> groups;
        private readonly List<TeamGroup> selectedGroups = [];
        private readonly DateTime day;
        private readonly IDbContextFactory<ApplicationDbContext> factory;

        public CalendarDayActivities(
            IEnumerable<TeamGroup> Groups,
            DateTime Day,
            IDbContextFactory<ApplicationDbContext> Factory)
        {
            groups = Groups;
            day = Day;
            activities = Groups
                 .SelectMany(g => g.Activities)
                 .Where(act => act.DayAndTime.Date == day.Date)
                 .GroupBy(x => x.Id)
                 .Select(g => g.First())
                 .ToList();
            factory = Factory;
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
