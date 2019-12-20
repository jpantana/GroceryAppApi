using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IInvitationRepository
    {
        IEnumerable<Invitation> GetAllInvitations();
        IEnumerable<Invitation> GetJustMyInvitations(int toId);
        Invitation SendInvite(SendInvitationCommand newInvite);
    }
}
