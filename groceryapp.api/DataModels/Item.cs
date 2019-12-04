using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroceryListId { get; }
        public int GroceryStoreId { get; }
    }
}
