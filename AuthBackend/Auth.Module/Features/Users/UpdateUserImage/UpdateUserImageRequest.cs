namespace Auth.Module.Features.Users.UpdateUserImage;

using Microsoft.AspNetCore.Http;

internal sealed class UpdateUserImageRequest
{
  public required IFormFile Image { get; set; }
}
