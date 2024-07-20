namespace AdminService.Redis;

public class RedisConfiguration
{
  public static string Redis = "Redis";
  public required string Host { get; set; }
  public int Port { get; set; }
  public required string Password { get; set; }
}
