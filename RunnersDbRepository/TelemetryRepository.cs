using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private string _connectionString;

        public TelemetryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
DELETE FROM [itsRunning].[Telemetry]
 WHERE IdActivity = @IdActivity";

                connection.Query(query, new { IdActivity = id });
            }
        }

        public IEnumerable<Telemetry> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[Longitude]
      ,[Latitude]
      ,[Instant]
      ,[IdActivity]
      ,[SelfiUri]
  FROM [itsRunning].[Telemetry]";

                return connection.Query<Telemetry>(query).ToList();
            }
        }

        public Telemetry Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[Longitude]
      ,[Latitude]
      ,[Instant]
      ,[IdActivity]
      ,[SelfiUri]
  FROM [itsRunning].[Telemetry]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Telemetry>(query, new { Id = id });
            }
        }

        public IEnumerable<Telemetry> GetFirstAndLastTelemetry(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT *
FROM    [itsRunning].[Telemetry]
WHERE   Id = (SELECT MIN(Id)  FROM [itsRunning].[Telemetry] Where IdActivity = @IdActivity)

UNION ALL

SELECT *
FROM    [itsRunning].[Telemetry]
WHERE   Id = (SELECT MAX(Id)  FROM [itsRunning].[Telemetry] Where IdActivity = @IdActivity);";

                return connection.Query<Telemetry>(query, new { IdActivity = id }).ToList();
            }
        }

        public void Insert(Telemetry value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[Telemetry]
           ([Longitude]
           ,[Latitude]
           ,[Instant]
           ,[IdActivity]
           ,[SelfiUri])
     VALUES
           (@Longitude
           ,@Latitude
           ,@Instant
           ,@IdActivity
           ,@SelfiUri)";

                connection.Query(query, value);
            }
        }

        public void Update(Telemetry value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[Telemetry]
   SET [Id] = @Id
      ,[Longitude] = @Longitude
      ,[Latitude] = @Latitude
      ,[Instant] = @Instant
      ,[IdActivity] = @IdActivity
      ,[SelfiUri] = @SelfiUri
 WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
