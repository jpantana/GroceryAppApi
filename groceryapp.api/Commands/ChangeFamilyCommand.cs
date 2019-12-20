using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Commands
{
    public class ChangeFamilyCommand
    {
        public int Id { get; set; }
        public string FamilyId { get; set; }
    }
}
