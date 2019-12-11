﻿using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item Add(Item newItem);
        IEnumerable<Item> GetOnlyMyItems(int gLId);
        bool Remove(int itemId);
    }
}
