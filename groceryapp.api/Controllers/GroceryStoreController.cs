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
    public class GroceryStoreController
    {
        private readonly ILogger<GroceryStoreController> _logger;
        private readonly IGroceryStoreRepository _repo;

        public GroceryStoreController(ILogger<GroceryStoreController> logger, IGroceryStoreRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<GroceryStore> GetAll()
        {
            return _repo.GetAllGroceryStores();
        }

    }
}
