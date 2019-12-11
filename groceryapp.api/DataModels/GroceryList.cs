﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    public class GroceryList
    {
        public int Id { get; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
