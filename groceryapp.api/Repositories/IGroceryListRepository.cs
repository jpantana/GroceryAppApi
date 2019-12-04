﻿using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IGroceryListRepository
    {
        IEnumerable<GroceryList> GetGroceryLists();
    }
}
