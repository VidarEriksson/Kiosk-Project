using InfoService.Data;

namespace InfoService.Services
{
    public interface ITextFileContentService
    {   
        Task<int> UploadTextFileAsync(IFormFile file, string expirationDate);
        Task<List<TextFileContent>> GetNonExpiredTextFilesAsync();
        Task<List<TextFileContent>> GetAllTextFilesAsync();
        Task DeleteTextFileAsync(int id);
    }
}
