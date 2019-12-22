using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using groceryapp.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationController : ControllerBase
    {
        private readonly ILogger<InvitationController> _logger;
        private readonly IInvitationRepository _repo;

        public InvitationController(ILogger<InvitationController> logger, IInvitationRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Invitation> GetAll()
        {
            return _repo.GetAllInvitations();
        }

        [HttpGet("{toId}")]
        public IEnumerable<Invitation> GetMyInvitations(int toId)
        {
            return _repo.GetJustMyInvitations(toId);
        }

        [HttpPost]
        public IActionResult sendInvitation(SendInvitationCommand newInvitation)
        {
            var newInvite = new SendInvitationCommand { 
                FamilyId = newInvitation.FamilyId,
                ToId = newInvitation.ToId,
                FromId = newInvitation.FromId
            };

            var invitationThatGotSent = _repo.SendInvite(newInvite);

            return Created($"/invitation/{invitationThatGotSent.FamilyId}", invitationThatGotSent);
        }

        [HttpDelete("{inviteId}")]
        public IActionResult DeleteInvitation(int inviteId)
        {
            _repo.DeleteThisInvite(inviteId);
            return Ok();
        }
    }
}
