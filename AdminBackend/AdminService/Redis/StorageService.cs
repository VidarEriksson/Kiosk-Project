namespace AdminService.Redis;
using System.Collections.Generic;
using System.Text.Json;

using Microsoft.Extensions.Options;

using StackExchange.Redis;

public class StorageService
  : IStorageService
{
  private readonly ILogger<StorageService> logger;
  private readonly RedisConfiguration redisConfiguration;
  private readonly ConnectionMultiplexer redis;
  private readonly IDatabase db;

  public StorageService(ILogger<StorageService> logger, IOptions<RedisConfiguration> options)
  {
    this.logger = logger;
    redisConfiguration = options.Value;
    ConfigurationOptions conf = new()
    {
      EndPoints = { $"{redisConfiguration.Host}:{redisConfiguration.Port}" },
      Password = redisConfiguration.Password,
      AllowAdmin = true,
      //User = "user"
    };
    redis = ConnectionMultiplexer.Connect(conf);
    db = redis.GetDatabase();
  }

  public Dictionary<string, string> GetValues(string serviceName)
  {
    logger.LogDebug("Getting values for service {service}", serviceName);

    RedisValue json = db.StringGet(serviceName);

    if (json.IsNullOrEmpty)
    {
      return [];
    }

    Dictionary<string, string>? result = JsonSerializer.Deserialize<Dictionary<string, string>>(json!);

    return result is null ? ([]) : result;
  }

  public string GetValue(string serviceName, string key)
  {
    logger.LogDebug("Getting value {key} for service {service}", key, serviceName);

    Dictionary<string, string> values = GetValues(serviceName);
    return values.TryGetValue(key, out string? value) ? value : string.Empty;
  }

  public KeyValuePair<string, string> SetValue(string serviceName, string key, string value)
  {
    logger.LogDebug("Setting value {key} for service {service} (value is {value})", key, serviceName, value);

    Dictionary<string, string> dict = GetValues(serviceName);

    if (!dict.TryAdd(key, value))
    {
      dict[key] = value;
    }

    SetValues(serviceName, dict);

    return dict.Single(kv => kv.Key == key);
  }

  public void SetValues(string serviceName, Dictionary<string, string> values)
  {
    logger.LogDebug("Setting values for service {service}", serviceName);

    string json = JsonSerializer.Serialize(values);
    _ = db.StringSet(serviceName, json);
  }

  public IEnumerable<string> GetServices()
  {
    logger.LogDebug("Getting services");

    RedisValue json = db.StringGet("ServicesRegistered");

    if (json.IsNullOrEmpty)
    {
      return [];
    }

    IEnumerable<string>? result = JsonSerializer.Deserialize<IEnumerable<string>>(json!);

    return result is null ? ([]) : result;
  }

  public void SetServices(IEnumerable<string> services)
  {
    logger.LogDebug("Setting services");
    string json = JsonSerializer.Serialize(services);
    _ = db.StringSet("ServicesRegistered", json);
  }

  public KeyValuePair<string, string> DeleteValue(string serviceName, string key)
  {
    logger.LogDebug("Deleting value {key} for service {service}", key, serviceName);
    Dictionary<string, string> dict = GetValues(serviceName);
    KeyValuePair<string, string> result = dict.Single(kv => kv.Key.Equals(key));

    if (dict.ContainsKey(key))
    {
      _ = dict.Remove(key);
    }

    SetValues(serviceName, dict);

    return result;
  }
}
