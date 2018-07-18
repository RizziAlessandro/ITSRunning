using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using ITSRunningDbRepository.Models;

namespace ITSRunningDbRepository
{
    public class RunnerRepository : IRunnerRepository
    {
        private string _connectionString;

        public RunnerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Runner> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[DateOfBirth]
      ,[Username]
      ,[Gender]
      ,[PhotoUri]
  FROM [itsRunning].[Runner]";

                return connection.Query<Runner>(query).ToList();
            }
        }

        public Runner Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[FirstName]
      ,[LastName]
      ,[DateOfBirth]
      ,[Username]
      ,[Gender]
      ,[PhotoUri]
  FROM [itsRunning].[Runner]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Runner>(query, new { Id = id });
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
      ,[DateOfBirth]
      ,[Username]
      ,[Gender]
      ,[PhotoUri]
  FROM [itsRunning].[Runner]
  WHERE Username = @Username";

                return connection.QueryFirstOrDefault<int>(query, new { Username = username });
            }
        }

        public void Insert(Runner value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [itsRunning].[Runner]
           ([FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Username]
           ,[Gender]
           ,[PhotoUri])
     VALUES
           (@FirstName
           ,@LastName
           ,@DateOfBirth
           ,@Username
           ,@Gender
           ,@PhotoUri)";

                connection.Query(query, value);
            }
        }

        public void Update(Runner value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
UPDATE [itsRunning].[Runner]
   SET [FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[DateOfBirth] = @DateOfBirth
      ,[Username] = @Username
      ,[Gender] = @Gender
      ,[PhotoUri] = @PhotoUri
 WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
