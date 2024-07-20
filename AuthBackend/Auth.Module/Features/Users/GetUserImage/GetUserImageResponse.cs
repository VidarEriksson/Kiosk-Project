namespace Auth.Module.Features.Users.GetUserImage;

using Microsoft.AspNetCore.Http;

sealed class GetUserImageResponse
{
  public required IFormFile Image { get; set; }
}
