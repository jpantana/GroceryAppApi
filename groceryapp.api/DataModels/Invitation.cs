using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    public class Invitation
    {
        public int Id { get; set; }
        public string FamilyId { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

