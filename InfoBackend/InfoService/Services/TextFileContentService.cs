using InfoService.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;

namespace InfoService.Services
{
    public class TextFileContentService : ITextFileContentService
    {
        private readonly AppDbContext _dbContext;

        public TextFileContentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> UploadTextFileAsync(IFormFile file, string expirationDate)
        {
            if (file == null || string.IsNullOrEmpty(expirationDate))
            {
                throw new ArgumentException("Missing file or expiration date");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            //Regex tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
            //bool hasTags = tagRegex.IsMatch(content);
            //if (!hasTags)
            //{
                content = HttpUtility.HtmlEncode(content);
                content = content.Replace("\r\n", "</br>");
                content = $"<div>{content}</div>";
           // }

            if (!DateTime.TryParse(expirationDate, out var parsedExpirationDate))
            {
                throw new ArgumentException("Invalid expiration date format");
            }

            var textFileContent = new TextFileContent
            {
                Content = content,
                ExpirationDate = parsedExpirationDate,
                RegisteredAt = DateTime.UtcNow
            };

            _dbContext.TextFiles.Add(textFileContent);
            await _dbContext.SaveChangesAsync();

            return textFileContent.Id;
        }

        public async Task<List<TextFileContent>> GetNonExpiredTextFilesAsync()
        {
            var currentDateTime = DateTime.UtcNow;
            return await _dbContext.TextFiles
                .Where(tf => tf.ExpirationDate > currentDateTime)
                .ToListAsync();
        }

        public async Task<List<TextFileContent>> GetAllTextFilesAsync()
        {
            return await _dbContext.TextFiles.ToListAsync();
        }

        public async Task DeleteTextFileAsync(int id)
        {
            var textFileContent = await _dbContext.TextFiles.FindAsync(id);
            if (textFileContent == null)
            {
                throw new KeyNotFoundException("Text file content not found");
            }

            _dbContext.TextFiles.Remove(textFileContent);
            await _dbContext.SaveChangesAsync();
        }
    }
}
