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

        [HttpPost]
        public IActionResult CreateNewUser(CreateUserCommand newUserCommand)
        {
            var newUser = new User
            {

            };

            var repo = new UserRepository();
            var userThatGotCreated = repo.Add(newUser);

            return Created($"/user/{userThatGotCreated.FirstName}", userThatGotCreated);
        }
    }
}
