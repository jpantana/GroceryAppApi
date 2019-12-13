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
    public class GroceryListController : ControllerBase
    {
        private readonly ILogger<GroceryListController> _logger;
        private readonly IGroceryListRepository _repo;

        public GroceryListController(ILogger<GroceryListController> logger, IGroceryListRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<GroceryList> GetAll()
        {
            return _repo.GetGroceryLists();
        }

        [HttpGet("{id}")]
        public IEnumerable<GroceryList> GetMyList(string id)
        {
            return _repo.GetMyGroceries(id);
        }

        [HttpPost]
        public IActionResult CreateNewGroceryList(CreateGroceryListCommand newGroceryListCommand)
        {
            var newGroceryList = new GroceryList
            {
                Name = newGroceryListCommand.Name,
                UserId = newGroceryListCommand.UserId,
                DateCreated = newGroceryListCommand.DateCreated
            };

            var repo = new GroceryListRepository();
            var groceryListThatGotCreated = repo.Add(newGroceryList);

            return Created($"/grocerylist/{groceryListThatGotCreated.Name}", groceryListThatGotCreated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroceryList(int id)
        {
            _repo.Remove(id);

            return Ok();
        }
    }
}
