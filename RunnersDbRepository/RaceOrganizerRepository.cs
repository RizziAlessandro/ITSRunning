using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public class RaceOrganizerRepository : IRaceOrganizerRepository
    {
        private string _connectionString;

        public RaceOrganizerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RaceOrganizer> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[Username]
  FROM [itsRunning].[RaceOrganizer]";

                return connection.Query<RaceOrganizer>(query).ToList();
            }
        }

        public RaceOrganizer Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[Username]
  FROM [itsRunning].[RaceOrganizer]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<RaceOrganizer>(query, new { Id = id });
            }
        }

        public int GetIdByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[Username]
  FROM [itsRunning].[RaceOrganizer]
  WHERE Username = @Username";

                return connection.QueryFirstOrDefault<int>(query, new { Username = username });
            }
        }

        public void Insert(RaceOrganizer value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[RaceOrganizer]
           ([FirstName]
           ,[LastName]
           ,[Username])
     VALUES
           (@FirstName
           ,@LastName
           ,@Username)";

                connection.Query(query, value);
            }
        }

        public void Update(RaceOrganizer value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[RaceOrganizer]
   SET [FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[Username] = @Username
 WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
