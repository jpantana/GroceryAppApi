﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Commands
{
    public class CreateGroceryListCommand
    {
        public string Name { get; set; }
        public string FamilyId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
