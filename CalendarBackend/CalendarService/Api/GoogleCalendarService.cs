namespace CalendarService.Api
{
    // Api/GoogleCalendarService.cs
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Services;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    public static class GoogleCalendarService
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public static async Task<List<Google.Apis.Calendar.v3.Data.Event>> GetCalendarEvents(string serviceAccountEmail, string keyFilePath, string keyPassword, string calendarId)
        {
            var certificate = new X509Certificate2(keyFilePath, keyPassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = Scopes
                }.FromCertificate(certificate));

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var request = service.Events.List(calendarId);
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 2500;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            List<Google.Apis.Calendar.v3.Data.Event> allEvents = new List<Google.Apis.Calendar.v3.Data.Event>();
            string pageToken = null;

            do
            {
                if (pageToken != null)
                {
                    request.PageToken = pageToken;
                }

                var events = await request.ExecuteAsync();
                if (events.Items != null && events.Items.Count > 0)
                {
                    allEvents.AddRange(events.Items);
                }

                pageToken = events.NextPageToken;
            } while (pageToken != null);

            return allEvents;
        }
    }

}
