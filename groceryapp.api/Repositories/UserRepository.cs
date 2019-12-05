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


        public User Add(User newUser)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                string sqlTimeAsString = newUser.SignUpDate.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                
                var sql = @"
                        INSERT INTO [User]
                                    ([FirstName]
                                    ,[LastName]
                                    ,[Uid]
                                    ,[Email]
                                    ,[SignUpDate]
                                    ,[IsActive]
                                    ,[FamilyId]
                                    )
	                        OUTPUT inserted.*
                                VALUES
                                    (@firstName
                                    ,@lastName
                                    ,@uid
                                    ,@email
                                    ,@signUpDate
                                    ,'true'
                                    ,1
                                    )";
                // Always passing int 1 for FamilyId to the create user bc 1 is the default 'family' 
                // for new users until the do a put to join a family
                return db.QueryFirst<User>(sql, newUser);
            }
        }
    }
}
