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

        public Item Add(Item newItem)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"
                        INSERT INTO [Item]
                                    ([Name]
                                    ,[GroceryListId]
                                    ,[GroceryStoreId]
                                    )
	                        OUTPUT inserted.*
                                VALUES
                                    (@name
                                    ,@groceryListId
                                    ,@groceryStoreId
                                    )";

                return db.QueryFirst<Item>(sql, newItem);

            }
        }
    }
}
