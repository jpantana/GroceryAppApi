using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using groceryapp.api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.DataModels
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _repo;

        public UserController(ILogger<UserController> logger, IUserRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _repo.GetAllUsers();
        }

        [HttpGet("{uid}")]
        public IEnumerable<User> GetSingle(string uid)
        {
            return _repo.GetSingleUser(uid);
        }

        [HttpGet("invite/{id}")]
        public IEnumerable<User> GetSingleById(int id)
        {
            return _repo.GetSingleUserById(id);
        }

        [HttpGet("lookup/{email}")]
        public IEnumerable<User> GetByEmail(string email)
        {
            return _repo.GetUserByEmail(email);
        }

        [HttpPost]
        public IActionResult CreateNewUser(CreateUserCommand newUserCommand)
        {
            var newUser = new User
            {
                FirstName = newUserCommand.FirstName,
                LastName = newUserCommand.LastName,
                Email = newUserCommand.Email,
                Uid = newUserCommand.Uid,
                FamilyId = newUserCommand.FamilyId,
                PhotoURL = newUserCommand.PhotoURL,
            };

            var repo = new UserRepository();
            // TRY TO USE _repo field that leans on the IUserRepo
            var userThatGotCreated = repo.Add(newUser);

            return Created($"/user/{userThatGotCreated.FirstName}", userThatGotCreated);
        }

        [HttpPut("{uid}")]
        public IActionResult UpdateUser(UpdateUserCommand updatedUserCommand, string uid)
        {
            //var repo = new UserRepository();

            var updatedUser = new UpdateUserCommand
            {
                FirstName = updatedUserCommand.FirstName,
                LastName = updatedUserCommand.LastName,
            };

            var userThatGotUpdated = _repo.Update(updatedUser, uid);

            return Ok(userThatGotUpdated);
        }

        [HttpPut("{toId}/{familyId}")]
        public IActionResult ChangeFamily(int toId, string familyId)
        {
            var updatedUser = new ChangeFamilyCommand
            {
                Id = toId,
                FamilyId = familyId,
            };

            var userThatGotUpdated = _repo.UpdateFamily(updatedUser);

            return Ok(userThatGotUpdated);
        }

        [HttpPut("uploadimage/{uid}")]
        public IActionResult AddProfilePicture(ChangeProfilePicCommand updatedProfile, string uid)
        {

            var updatedProfPic = new ChangeProfilePicCommand
            {
                PhotoURL = updatedProfile.PhotoURL
            };

            var userThatGotUpdated = _repo.UpdateProfilePic(updatedProfPic, uid);

            return Ok(userThatGotUpdated);
        }

        [HttpDelete("{uid}")]
        public IActionResult DeleteThisUser(string uid)
        {
            _repo.Remove(uid);

            return Ok();
        }

        [HttpGet("myfamily/{familyId}")]
        public ActionResult<IEnumerable<User>> GetMyFamilyMembers(string familyId)
        {
            return _repo.GetMyFamilyMembers(familyId);
        }
    }
}
