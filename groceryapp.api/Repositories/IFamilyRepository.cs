using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> GetAllFamily();
        Family Add(Family newFamilyCommand);
    }
}
