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
                DurationSeconds = Form.Minutes * 60,
                Description = Form.Description,
                WorkoutType = Form.SelectedActivityType.Type,
                IsWorkOut = Form.IsWorkOut,
            };

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

            using var ctx = factory.CreateDbContext();

            var loadedActivity = await ctx.Activity
                .Include(act => act.Groups)
                .FirstOrDefaultAsync(act => act.Id == ActivityId)
                ?? throw new NullReferenceException();

            loadedActivity.DurationSeconds = Form.Minutes * 60;
            loadedActivity.Groups.Clear();
            loadedActivity.Description = Form.Description;
            loadedActivity.WorkoutType = Form.SelectedActivityType.Type;
            loadedActivity.IsWorkOut = Form.IsWorkOut;

            foreach (var group in Form.GetSelectedGroups)
            {
                var loadedGroup = await ctx.TeamGroup.FindAsync(group.Id) ?? throw new NullReferenceException();
                loadedActivity.Groups.Add(loadedGroup);
            }

            await ctx.SaveChangesAsync();
            await ctx.DisposeAsync();

            activities.RemoveAll(act => act.Id == loadedActivity.Id);
            activities.Add(loadedActivity);
            return loadedActivity;
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
