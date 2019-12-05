using Dapper;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        string _connectionString = "Server=localhost;Database=GroceriesDb;Trusted_Connection=True;";

        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var UserList = db.Query<User>("SELECT * FROM [USER]");

                return UserList;
            }
        }
    }
}
