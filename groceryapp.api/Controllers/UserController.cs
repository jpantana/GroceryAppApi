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


        [HttpPost]
        public IActionResult CreateNewUser(CreateUserCommand newUserCommand)
        {
            var newUser = new User
            {
                FirstName = newUserCommand.FirstName,
                LastName = newUserCommand.LastName,
                Email = newUserCommand.Email,
                Uid = newUserCommand.Uid,
                // fam id is new. sql used to just say '1'
                // FamilyId = newUserCommand.FamilyId
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

            var trainerThatGotUpdated = _repo.Update(updatedUser, uid);

            return Ok(trainerThatGotUpdated);
        }

        [HttpDelete("{uid}")]
        public IActionResult DeleteThisUser(int uid)
        {
            _repo.Remove(uid);

            return Ok();
        }
    }
}
