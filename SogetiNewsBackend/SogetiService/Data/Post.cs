namespace SogetiService.Data
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Title { get; set; }
        public string? Intro { get; set; }
        public string? ImageUrl { get; set; }
        public string? PageUrl { get; set; }
        public int TagId { get; set; }


    }
}
