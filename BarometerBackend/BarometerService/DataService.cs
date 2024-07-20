namespace BarometerService;

using System.Data.SqlClient;

using Dapper;

using MySql.Data.MySqlClient;

public class DataService(string connectionString) : IDataService
{
  public async Task<IEnumerable<Measure>> GetMeasures()
  {
    var sql = "select * from Measures";
    using var connection = new MySqlConnection(connectionString);
    connection.Open();
    var measures = await connection.QueryAsync<Measure>(sql);
    return measures.AsEnumerable();
  }
}
