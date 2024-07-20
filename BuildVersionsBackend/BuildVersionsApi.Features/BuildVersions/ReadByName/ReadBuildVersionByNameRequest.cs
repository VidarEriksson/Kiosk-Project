namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

public sealed class ReadBuildVersionByNameRequest
{
  public required string ProjectName { get; set; }
}