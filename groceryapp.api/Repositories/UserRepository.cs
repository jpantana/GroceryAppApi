﻿using Dapper;
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
                return db.QueryFirst<User>(sql, newUserCommand);
            }
        }

        public IEnumerable<User> GetSingleUser(string uid)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"Select * From [User] Where [User].uid = @uid";

                var user = db.Query<User>(sql, new { uid });

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

        public bool Remove(int userId)
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

    }
}
