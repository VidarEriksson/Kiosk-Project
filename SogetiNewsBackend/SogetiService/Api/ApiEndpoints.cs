using Microsoft.EntityFrameworkCore;
using SogetiService.Data;
using System.Diagnostics;

namespace SogetiService.Api
{
    public static class ApiEndpoints
    {
        public static void MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            // Hämta alla events
            app.MapGet("/SogetiNews/{amount}", (int amount,ILogger<Program> logger, SogetiNewsDbContext db) =>
              { 
                 // var watch = Stopwatch.StartNew();
                  var result = db.Posts.AsNoTracking().OrderByDescending(p => p.Date).Take(amount).AsAsyncEnumerable();
                  //watch.Stop();
                 // var time = watch.ElapsedMilliseconds;
                  //logger.LogInformation("timeelapse: {time}", time);
                  return result;
              })
                .WithName("getnews")
                .WithOpenApi();
        }
    }
}
