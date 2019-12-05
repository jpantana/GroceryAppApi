using Dapper;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class GroceryStoreRepository : IGroceryStoreRepository
    {
        string _connectionString = "Server=localhost;Database=GroceriesDb;Trusted_Connection=True;";

        public IEnumerable<GroceryStore> GetAllGroceryStores()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var groceryStores = db.Query<GroceryStore>("SELECT * FROM GroceryStore");

                return groceryStores;
            }
        }
    }
}
