using Account.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Npgsql;

namespace Account.API.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private string _connectionString = string.Empty;

        public UserQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            NpgsqlConnection connection = GetConnection();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var result = await connection.QueryFirstAsync<UserViewModel>
                    (@"SELECT * FROM dbo.user WHERE ""IdentityUser""=@Id", new { Id = id });


                if (result == null)
                    throw new KeyNotFoundException();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro getting user data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public async Task<List<UserViewModel>> GetUsers()
        {

            NpgsqlConnection connection = GetConnection();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();  

                var result = await connection.QueryAsync<UserViewModel>
                    (@"SELECT * FROM dbo.user");

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro getting user data: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        private UserViewModel MapUser(dynamic result)
        {
            var user = new UserViewModel
            {
                IdentityUser = result[0].IdentityGuid,
                Email = result[0].Email,
                Name = result[0].Name
            };
            
            return user;
        }

    }
}
