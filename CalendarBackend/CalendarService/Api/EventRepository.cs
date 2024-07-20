namespace CalendarService.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using CalendarService.Models;
    using CalendarService.Data;

    public static class EventRepository
    {
        public static async Task SaveEventsToDatabase(List<Google.Apis.Calendar.v3.Data.Event> events, CalendarContext context)
        {
            foreach (var eventItem in events)
            {
                var existingEvent = await context.Events.FindAsync(eventItem.Id);

                if (existingEvent == null)
                {
                    context.Events.Add(new CalendarEvent
                    {
                        Id = eventItem.Id,
                        Summary = eventItem.Summary,
                        Description = eventItem.Description,
                        Location = eventItem.Location,
                        StartTime = eventItem.Start.DateTime ?? DateTime.Parse(eventItem.Start.Date),
                        EndTime = eventItem.End.DateTime ?? DateTime.Parse(eventItem.End.Date)
                    });
                }
                else
                {
                    existingEvent.Summary = eventItem.Summary;
                    existingEvent.Description = eventItem.Description;
                    existingEvent.Location = eventItem.Location;
                    existingEvent.StartTime = eventItem.Start.DateTime ?? DateTime.Parse(eventItem.Start.Date);
                    existingEvent.EndTime = eventItem.End.DateTime ?? DateTime.Parse(eventItem.End.Date);

                    context.Events.Update(existingEvent);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
