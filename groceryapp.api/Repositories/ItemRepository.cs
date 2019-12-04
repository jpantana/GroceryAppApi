using Dapper;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public class ItemRepository : IItemRepository
    {

        string _connectionString = "Server=localhost;Database=GroceriesDb;Trusted_Connection=True;";

        public IEnumerable<Item> GetAllItems()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var groceryItem = db.Query<Item>("Select * From Item");

                return groceryItem;
            }
        } 
    }
}
