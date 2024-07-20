using InfoService.Data;
using InfoService.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;


namespace InfoService.Api
{
    public static class ApiEndpoints
    {
        public static void MapApiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/upload", async (IFormFile file, string expirationDate, ITextFileContentService service) =>
            {
                try
                {
                    var id = await service.UploadTextFileAsync(file, expirationDate);
                    return Results.Ok(new { Id = id });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
                .DisableAntiforgery(); 

                
              
            app.MapGet("/content", async (ITextFileContentService service) =>
            {
                var nonExpiredTextFiles = await service.GetNonExpiredTextFilesAsync();
                return Results.Ok(nonExpiredTextFiles);
            })
                .WithOpenApi(); ;
               
            
                          
            app.MapGet("/all-content", async (ITextFileContentService service) =>
            {
                var allTextFiles = await service.GetAllTextFilesAsync();
                return Results.Ok(allTextFiles);
            })
                .WithOpenApi(); ;

          
            app.MapDelete("/delete/{id}", async (int id, ITextFileContentService service) =>
            {
                try
                {
                    await service.DeleteTextFileAsync(id);
                    return Results.Ok();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            })
                .WithOpenApi(); ;

        }

    }
}
