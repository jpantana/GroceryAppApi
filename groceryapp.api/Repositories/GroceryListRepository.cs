using Dapper;
using groceryapp.api.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class GroceryListRepository : IGroceryListRepository
    {
        // FOR MAC
        string _connectionString = "Server=localhost;Database=GroceriesDb2;User Id=sa; Password=reallyStrongPwd123";
        // FOR PC
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";

        public IEnumerable<GroceryList> GetGroceryLists()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var groceryLists = db.Query<GroceryList>("Select * From GroceryList");

                return groceryLists;
            }
        }

        public IEnumerable<GroceryList> GetMyGroceries(string familyId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"SELECT * FROM GroceryList WHERE [FamilyId] = @familyId";

                var parameters = new { familyId };

                var myGroceryList = db.Query<GroceryList>(sql, parameters);

                return myGroceryList;
            }
        }

   

        public GroceryList Add(GroceryList newGroceryList)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"
                    INSERT INTO [GroceryList]
                                ([Name]
                                ,[FamilyId]
                                ,[DateCreated]
                                )
	                    OUTPUT inserted.*
                            VALUES
                                (@Name
                                ,@familyId
                                ,@DateCreated
                                )";

                return db.QueryFirst<GroceryList>(sql, newGroceryList);

            }
        }

        public bool Remove(int glId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"DELETE
                            From [GroceryList]
                            Where [Id] = @glId";


                return db.Execute(sql, new { glId }) == 1;
            }
        }

    }
}
