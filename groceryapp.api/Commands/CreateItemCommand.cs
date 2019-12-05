using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Commands
{
    public class CreateItemCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroceryListId { get; set; }
        public int GroceryStoreId { get; set; }
    }
}
