using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public class RaceRepository : IRaceRepository
    {
        private string _connectionString;

        public RaceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
DELETE FROM [itsRunning].[Race]
 WHERE Id = @Id";

                connection.Query(query, new { Id = id });
            }
        }

        public void EnableDisableRace(int id, bool value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[Race]
   SET [IsEnabled] = @IsEnabled
 WHERE Id = @Id";

                connection.Query(query, new { Id = id, IsEnabled = value});
            }
        }

        public IEnumerable<Race> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[RaceOrganizerId]
      ,[IsEnabled]
  FROM [itsRunning].[Race]";

                return connection.Query<Race>(query).ToList();
            }
        }

        public Race Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[RaceOrganizerId]
      ,[IsEnabled]
  FROM [itsRunning].[Race]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Race>(query, new { Id = id });
            }
        }

        public IEnumerable<Race> GetListById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[RaceOrganizerId]
      ,[IsEnabled]
  FROM [itsRunning].[Race]
  WHERE RaceOrganizerId = @RaceOrganizerId";

                return connection.Query<Race>(query, new { RaceOrganizerId = id }).ToList();
            }
        }

        public IEnumerable<Race> GetRaceEnabled()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[RaceOrganizerId]
      ,[IsEnabled]
  FROM [itsRunning].[Race]
  WHERE [IsEnabled] = 1";

                return connection.Query<Race>(query).ToList();
            }
        }

        public void Insert(Race value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[Race]
           ([Name]
           ,[Description]
           ,[RaceOrganizerId]
           ,[IsEnabled])
     VALUES
           (@Name
           ,@Description
           ,@RaceOrganizerId
           ,@IsEnabled)";

                connection.Query(query, value);
            }
        }

        public void Update(Race value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[Race]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[RaceOrganizerId] = @RaceOrganizerId
      ,[IsEnabled] = @IsEnabled
 WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
