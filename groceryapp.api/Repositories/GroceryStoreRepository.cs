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
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";
        string _connectionString = "Server=tcp:groceryappserver.database.windows.net,1433;Initial Catalog=GroceryDb;Persist Security Info=False;User ID=jpantana;Password=GroceryAppPwd1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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
