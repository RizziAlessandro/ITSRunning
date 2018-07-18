using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public class ActivityRepository : IActivityRepository
    {
        private string _connectionString;

        public ActivityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
DELETE FROM [itsRunning].[Activity]
 WHERE Id = @Id";

                connection.Query(query, new { Id = id });
            }
        }

        public IEnumerable<Activity> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[IdRunner]
      ,[CreationDate]
      ,[Location]
      ,[Type]
      ,[IdRace]
  FROM [itsRunning].[Activity]";

                return connection.Query<Activity>(query).ToList();
            }
        }

        public Activity Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[IdRunner]
      ,[CreationDate]
      ,[Location]
      ,[Type]
      ,[IdRace]
  FROM [itsRunning].[Activity]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Activity>(query, new { Id = id });
            }
        }

        public IEnumerable<Activity> GetListByIdRunner(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[IdRunner]
      ,[CreationDate]
      ,[Location]
      ,[Type]
      ,[IdRace]
  FROM [itsRunning].[Activity]
  WHERE IdRunner = @IdRunner";

                return connection.Query<Activity>(query, new { IdRunner = id }).ToList();
            }
        }

        public void Insert(Activity value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[Activity]
           ([Id]
           ,[IdRunner]
           ,[CreationDate]
           ,[Location]
           ,[Type]
           ,[IdRace])
     VALUES
           (@Id
           ,@IdRunner
           ,@CreationDate
           ,@Location
           ,@Type
           ,@IdRace)";

                connection.Query(query, value);
            }
        }

        public int InsertAndReturnId(Activity value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[Activity]
           ([IdRunner]
           ,[CreationDate]
           ,[Location]
           ,[Type]
           ,[IdRace])
     VALUES
           (@IdRunner
           ,@CreationDate
           ,@Location
           ,@Type
           ,@IdRace);
SELECT CAST(SCOPE_IDENTITY() as int)";

                int id = connection.Query<int>(query, new {
                    IdRunner = value.IdRunner,
                    CreationDate = value.CreationDate,
                    Location = value.Location,
                    Type = value.Type,
                    IdRace = value.IdRace
                }).Single();

                return id;
            }
        }

        public void Update(Activity value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[Activity]
   SET [Id] = @Id
      ,[IdRunner] = @IdRunner
      ,[CreationDate] = @CreationDate
      ,[Location] = @Location
      ,[Type] = @Type
      ,[IdRace] = @IdRace
 WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
