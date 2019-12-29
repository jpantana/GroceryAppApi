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
    public class FamilyController : ControllerBase
    {
        private readonly ILogger<FamilyController> _logger;
        private readonly IFamilyRepository _repo;

        public FamilyController(ILogger<FamilyController> logger, IFamilyRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Family> GetAll()
        {
            return _repo.GetAllFamily();
        }

        [HttpGet("{familyId}")]
        public IEnumerable<Family> GetSingleFamily(Guid familyId)
        {
            return _repo.GetSingleFamily(familyId);
        }

        [HttpPost]
        public IActionResult CreateNewFamily(CreateFamilyCommand newFamilyCommand)
        {
            var newFamily = new CreateFamilyCommand
            {
                Name = newFamilyCommand.Name,
                Id = newFamilyCommand.Id
            };

            // var repo = new FamilyRepository();
            var familyThatGotCreated = _repo.Add(newFamily);

            return Created($"/family/{familyThatGotCreated.Name}", familyThatGotCreated);
        }

    }
}
