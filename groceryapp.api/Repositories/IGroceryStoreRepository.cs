using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IGroceryStoreRepository
    {
        IEnumerable<GroceryStore> GetAllGroceryStores();
    }
}
