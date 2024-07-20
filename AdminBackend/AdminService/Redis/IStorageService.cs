namespace AdminService.Redis;
using System.Collections.Generic;

public interface IStorageService
{
  IEnumerable<string> GetServices();
  void SetServices(IEnumerable<string> services);

  Dictionary<string, string> GetValues(string serviceName);
  void SetValues(string serviceName, Dictionary<string, string> values);
  KeyValuePair<string, string> SetValue(string serviceName, string key, string value);
  KeyValuePair<string,string> DeleteValue(string serviceName, string key);
}

public static class StorageServiceExtensions
{
  public static WebApplication AddTestData(this WebApplication app, IEnumerable<string> services)
  {
    using var scope = app.Services.CreateScope();
    var redis = scope.ServiceProvider.GetRequiredService<IStorageService>();
    redis.SetServices(services);
    foreach (var service in services)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      dict.Add("ServiceName", service);
      dict.Add("Key1", "Value1");
      dict.Add("Key2", "Value2");
      dict.Add("Key3", "Value3");
      redis.SetValues(service, dict);
    }
    return app;
  }
}
