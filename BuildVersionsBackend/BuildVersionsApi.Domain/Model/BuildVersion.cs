namespace BuildVersionsApi.Domain.Model;

using System.Text.RegularExpressions;

public sealed partial class BuildVersion : BaseLoggedEntity
{
  //https://devopsnet.com/2011/06/09/build-versioning-strategy/

  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }

  public string SemanticVersionText { get; set; } = "{Major}.{Minor}.{Build}-dev.{Revision}";

  //Calculated values
  public Version Version => new(Major, Minor, Build, Revision);

  public string Release => $"{Major}.{Minor}";

  public string SemanticVersion => VersionReplacer().Replace(SemanticVersionText, match => GetValue(match.Value));

  private string GetValue(string variable) => variable.ToLower() switch
  {
    "{major}" => $"{Major}",
    "{minor}" => $"{Minor}",
    "{build}" => $"{Build}",
    "{revision}" => $"{Revision}",
    _ => "",
  };
  [GeneratedRegex(@"\{\w+?\}")]
  private static partial Regex VersionReplacer();
}