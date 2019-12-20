using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Commands
{
    public class CreateUserCommand
    {
        public int Id { get; }
        public string Uid { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SignUpDate = DateTime.Now;
        public bool IsActive { get; set; }
        public string FamilyId { get; set; }
        //public string PhotoURL { get; set; }
    }
}
