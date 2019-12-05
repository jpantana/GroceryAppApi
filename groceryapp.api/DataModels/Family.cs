using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    public class Family
    {
        public int Id { get; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
