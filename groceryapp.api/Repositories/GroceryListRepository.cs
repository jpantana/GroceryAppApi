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

        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";
        string _connectionString = "Server=tcp:groceryappserver.database.windows.net,1433;Initial Catalog=GroceryDb;Persist Security Info=False;User ID=jpantana;Password=GroceryAppPwd1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
