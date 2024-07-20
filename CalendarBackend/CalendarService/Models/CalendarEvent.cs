﻿namespace CalendarService.Models
{
    public class CalendarEvent
    {
        public string Id { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
