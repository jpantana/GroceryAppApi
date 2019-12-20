using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Commands
{
    public class SendInvitationCommand
    {
        public int Id { get; set; }
        public string FamilyId { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
