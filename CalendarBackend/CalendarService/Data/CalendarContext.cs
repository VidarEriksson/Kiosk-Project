using CalendarService.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarService.Data
{
    public class CalendarContext : DbContext
    {

        public DbSet<CalendarEvent> Events { get; set; }


        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
        {

        }
    }

}
