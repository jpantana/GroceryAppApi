﻿using groceryapp.api.Commands;
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
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemRepository _repo;

        public ItemController(ILogger<ItemController> logger, IItemRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Item> GetAll()
        {
            return _repo.GetAllItems();
        }

        [HttpPost]
        public IActionResult CreateNewItem(CreateItemCommand newItemCommand)
        {
            var newItem = new CreateItemCommand
            {
                Name = newItemCommand.Name,
                GroceryListId = newItemCommand.GroceryListId,
                GroceryStoreId = newItemCommand.GroceryStoreId,
            };

            // var repo = new ItemRepository();
            var itemThatGotCreated = _repo.Add(newItem);

            return Created($"/item/{itemThatGotCreated.Name}", itemThatGotCreated);
        }

        [HttpGet("{gLId}")]
        public IEnumerable<Item> GetMyItems(int gLId)
        {
            return _repo.GetOnlyMyItems(gLId);
        }

        [HttpDelete("{itemId}")]
        public IActionResult DeleteItem(int itemId)
        {
            _repo.Remove(itemId);
            return Ok();
        }
    }
}
