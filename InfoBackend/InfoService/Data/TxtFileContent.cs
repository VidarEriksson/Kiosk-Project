namespace InfoService.Data
{
    public class TextFileContent
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
