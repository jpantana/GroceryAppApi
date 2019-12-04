using Dapper;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class GroceryListRepository : IGroceryListRepository
    {

        string _connectionString = "Server=localhost;Database=GroceriesDb;Trusted_Connection=True;";

        public IEnumerable<GroceryList> GetGroceryLists()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var groceryLists = db.Query<GroceryList>("Select * From GroceryList");

                return groceryLists;
            }
        }
    }
}
