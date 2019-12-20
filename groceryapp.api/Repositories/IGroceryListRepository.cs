using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IGroceryListRepository
    {
        IEnumerable<GroceryList> GetGroceryLists();
        IEnumerable<GroceryList> GetMyGroceries(string familyId);
        GroceryList Add(GroceryList newGroceryList);
        bool Remove(int glId);

    }
}
