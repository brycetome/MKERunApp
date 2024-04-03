using Microsoft.EntityFrameworkCore;

namespace Models.ViewModels
{
    public class CalendarDayActivityViewModel
    {
        public IEnumerable<Activity> GetActivities => activities;
        public IEnumerable<TeamGroup> GetGroups => groups;
        
        public DateTime GetDay => day;

        private List<Activity> activities;
        private readonly IEnumerable<TeamGroup> groups;
        private DateTime day;
        private readonly IDbContextFactory<ApplicationDbContext> factory;

        public static async Task<CalendarDayActivityViewModel> CreateDayFromTeam(IDbContextFactory<ApplicationDbContext> Factory, Team Team)
        {
            using var ctx = Factory.CreateDbContext();
            var team = await ctx.Team
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(t => t.Id == Team.Id);
            await ctx.DisposeAsync();
            // TODO: Use local browser time
            var today = DateTime.UtcNow.AddHours(-5).ToUniversalTime();
            var dayActivities = new CalendarDayActivityViewModel(team?.Groups ?? [], today, Factory);
            await dayActivities.ChangeDay(today);
            return dayActivities;
        }


        public CalendarDayActivityViewModel(
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
                        .Include(act => act.WorkoutItems)
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

        public async Task<Activity> AddNewActivity(ActivityFormModel Form)
        {
            var newActivity = new Activity()
            {
                DayAndTime = day.ToUniversalTime(),
            };

            Form.ApplyForm(newActivity);
            newActivity.Groups.Clear();

            using var ctx = factory.CreateDbContext();

            foreach (var group in Form.GetSelectedGroups)
            {
                var loadedGroup = await ctx.TeamGroup.FindAsync(group.Id) ?? throw new NullReferenceException();
                loadedGroup.Activities.Add(newActivity);
            }

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            activities.Add(newActivity);
            Form.ResetForm();
            return newActivity;
        }

        public async Task<Activity> UpdateActivity(int ActivityId, ActivityFormModel Form)
        {
            var act = activities.Find(act => act.Id == ActivityId) ?? throw new NullReferenceException();
            using var ctx = factory.CreateDbContext();

            ctx.Attach(act);
            ctx.UpdateRange(Form.WorkoutItems);
            Form.ApplyForm(act);

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();

            using var ctx2 = factory.CreateDbContext();
            var loadedActivity = await ctx2.Activity
                .Include(act => act.WorkoutItems)
                .Include(act => act.Groups)
                .FirstOrDefaultAsync(act => act.Id == ActivityId)
                ?? throw new NullReferenceException();

            var toDelete = loadedActivity.WorkoutItems.Where(item => !Form.WorkoutItems.Any(item2 => item2.Id == item.Id));
            ctx2.WorkoutItem.RemoveRange(toDelete);

            await ctx2.SaveChangesAsync();
            await ctx2.DisposeAsync();
            return act;
        }

        public async Task DeleteActivity(Activity activity)
        {
            using var ctx = factory.CreateDbContext();
            var loadedActivity = await ctx.Activity.FindAsync(activity.Id)
                ?? throw new NullReferenceException();
            ctx.Activity.Remove(loadedActivity);
            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();
            activities.RemoveAll(act => act.Id == loadedActivity.Id);
        }
    }
}
