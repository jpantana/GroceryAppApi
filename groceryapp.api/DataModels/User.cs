using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    public class User
    {
        public int Id { get; }
        public string UID { get; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SignUpDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public int FamilyId { get; }
    }
}
