﻿using Dapper;
using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {

        //string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";
        string _connectionString = "Server=tcp:groceryappserver.database.windows.net,1433;Initial Catalog=GroceryDb;Persist Security Info=False;User ID=jpantana;Password=GroceryAppPwd1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IEnumerable<Invitation> GetAllInvitations()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var invitations = db.Query<Invitation>("SELECT * FROM [Invitation]");

                return invitations;
            }
        }

        public IEnumerable<Invitation> GetJustMyInvitations(int toId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"SELECT *
                              FROM [Invitation]
                              WHERE [ToId] = @toId";

                var myInvites = db.Query<Invitation>(sql, new { toId });

                return myInvites;
            }
        }

        public Invitation SendInvite(SendInvitationCommand newInvite)
        {

            string sqlTimeAsString = newInvite.DateCreated.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"                        
                            INSERT INTO [Invitation]
                                    ([FamilyId]
                                    ,[ToId]
                                    ,[FromId]
                                    ,[DateCreated]
                                    )
	                        OUTPUT inserted.*
                                VALUES
                                    (@familyId
                                    ,@toId
                                    ,@fromId
                                    ,@dateCreated
                                    )";

                return db.QueryFirst<Invitation>(sql, newInvite);
            }

        }

        public bool DeleteThisInvite(int inviteId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            From [Invitation]
                            Where [Id] = @inviteId";

                return db.Execute(sql, new { inviteId }) == 1;
            }
        }


    }
}
