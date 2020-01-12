using Dapper;
using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";
        string _connectionString = "Server=tcp:groceryappserver.database.windows.net,1433;Initial Catalog=GroceryDb;Persist Security Info=False;User ID=jpantana;Password=GroceryAppPwd1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var UserList = db.Query<User>("SELECT * FROM [USER]");

                return UserList;
            }
        }


        public User Add(User newUserCommand)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                string sqlTimeAsString = newUserCommand.SignUpDate.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                
                var sql = @"
                        INSERT INTO [User]
                                    ([FirstName]
                                    ,[LastName]
                                    ,[Uid]
                                    ,[Email]
                                    ,[SignUpDate]
                                    ,[IsActive]
                                    ,[FamilyId]
                                    ,[PhotoURL]
                                    )
	                        OUTPUT inserted.*
                                VALUES
                                    (@firstName
                                    ,@lastName
                                    ,@uid
                                    ,@email
                                    ,@signUpDate
                                    ,'true'
                                    ,@familyId
                                    ,@photoURL
                                    )";
                // Always passing int 1 for FamilyId to the create user bc 1 is the default 'family' 
                // for new users until the do a put to join a family
                return db.QueryFirst<User>(sql, newUserCommand);
            }
        }

        public IEnumerable<User> GetSingleUser(string uid)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"SELECT * FROM [User] WHERE [User].uid = @uid";

                var user = db.Query<User>(sql, new { uid });

                return user;
            }
        }

        public IEnumerable<User> GetSingleUserById(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"SELECT * FROM [User] WHERE [User].id = @id";

                var user = db.Query<User>(sql, new { id });

                return user;
            }
        }


        public IEnumerable<User> GetUserByEmail(string email)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"SELECT * FROM [User] WHERE [User].Email = @email";

                var user = db.Query<User>(sql, new { Email = email });

                return user;
            }
        }

        public ActionResult<User> Update(UpdateUserCommand updatedUser, string uid)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();


                var sql = @"UPDATE [User] 
                            SET [FirstName] = @firstName
                                ,[LastName] = @lastName
                            OUTPUT INSERTED.*
                            WHERE [Uid] = @uid";

                updatedUser.Uid = uid;

                var user = db.QueryFirst<User>(sql, updatedUser);
                return user;
            }
        }
        
        public ActionResult<User> UpdateProfilePic(ChangeProfilePicCommand updatedProfPic, string uid)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"UPDATE [User] 
                            SET [PhotoURL] = @imgUrl
                            OUTPUT INSERTED.*
                            WHERE [Uid] = @uid";

                var parameters = new
                {
                    uid,
                    imgUrl = updatedProfPic.PhotoURL,
                };

                var user = db.QueryFirst<User>(sql, parameters);

                return user;
            }
        }

        public ActionResult<User> UpdateFamily(ChangeFamilyCommand updatedUser)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"UPDATE [User] 
                            SET [FamilyId] = @familyId
                            OUTPUT INSERTED.*
                            WHERE [Id] = @toId";

                var parameters = new
                {
                    toId = updatedUser.Id,
                    familyId = updatedUser.FamilyId
                };

                var family = db.QueryFirst<User>(sql, parameters);

                return family;
            }
        }

        public bool Remove(string userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"DELETE
                            From [User]
                            Where [Uid] = @userId";

                return db.Execute(sql, new { userId }) == 1;
            }
        }

        public List<User> GetMyFamilyMembers(string familyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                // may want not * but just user names or emails??
                var sql = @"SELECT *
                            From [User]
                            Where [FamilyId] = @familyId";

                var familyMembers = db.Query<User>(sql, new { familyId });

                return familyMembers.AsList();

            }
        }

    }
}
