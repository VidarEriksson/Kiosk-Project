
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CalendarService.Api;
using CalendarService.Data;
using Microsoft.Extensions.DependencyInjection;
namespace CalendarService.Services
{
    public class GoogleCalendarBackgroundService : BackgroundService
    {
        private readonly ILogger<GoogleCalendarBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public GoogleCalendarBackgroundService(ILogger<GoogleCalendarBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Google Calendar Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Fetching Google Calendar events...");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CalendarContext>();

                    string serviceAccountEmail = "email-for-getting-calender";
                    string keyFilePath = "./yourfile.p12";
                    string keyPassword = "notasecret"; //notasecret standard password
                    string calendarId = "bunch of numbers and letters@group.calendar.google.com";

                    var allEvents = await GoogleCalendarService.GetCalendarEvents(serviceAccountEmail, keyFilePath, keyPassword, calendarId);
                    await EventRepository.SaveEventsToDatabase(allEvents, dbContext);
                }

                _logger.LogInformation("Google Calendar events fetched and saved.");

                // Vänta en minut innan nästa hämtning
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInformation("Google Calendar Background Service is stopping.");
        }
    }
}
