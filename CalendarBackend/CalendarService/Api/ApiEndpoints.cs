namespace CalenderService.Api
{
    using Microsoft.EntityFrameworkCore;
    using CalendarService.Data;
    using CalendarService.Models;

    public static class ApiEndpoints
    {
        public static void MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            // Hämta alla events
            app.MapGet("/events", async (CalendarContext db) =>
                await db.Events.ToListAsync());

            // Hämta en specifik event med ID
            app.MapGet("/events/{id}", async (CalendarContext db, string id) =>
                await db.Events.FindAsync(id)
                    is CalendarEvent eventItem
                        ? Results.Ok(eventItem)
                        : Results.NotFound());

            // Hämta kommande events
            app.MapGet("/events/upcoming", async (CalendarContext db) =>
                await db.Events.Where(e => e.StartTime >= DateTime.UtcNow).ToListAsync());
        }
    }
}
