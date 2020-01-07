using Dapper;
using groceryapp.api.Commands;
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

        // FOR MAC
        string _connectionString = "Server=localhost;Database=GroceriesDb2;User Id=sa; Password=reallyStrongPwd123";
        // FOR PC
        // string _connectionString = "Server=localhost;Database=GroceriesDb2;Trusted_Connection=True;";

        public IEnumerable<Item> GetAllItems()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var groceryItem = db.Query<Item>("Select * From Item");

                return groceryItem;
            }
        } 

        public Item Add(CreateItemCommand newItem)
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

        public IEnumerable<Item> GetOnlyMyItems(int gLId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var sql = @"
                           SELECT * FROM [Item]
                             WHERE GroceryListId = @groceryListId";

                var parameters = new { groceryListId = gLId };

                var myItems = db.Query<Item>(sql, parameters);

                return myItems;
            }
        }

        public bool Remove(int itemId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE
                            From [Item]
                            Where [Id] = @itemId";

                return db.Execute(sql, new { itemId }) == 1;
            }
        }
    }
}
