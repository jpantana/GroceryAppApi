using groceryapp.api.Commands;
using groceryapp.api.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace groceryapp.api.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User Add(User newUserCommand);
        IEnumerable<User> GetSingleUser(string uid);
        IEnumerable<User> GetUserByEmail(string email);
        ActionResult<User> Update(UpdateUserCommand updatedUser, string uid);
        bool Remove(int userId);
        List<User> GetMyFamilyMembers(string familyId);

        ActionResult<User> UpdateFamily(ChangeFamilyCommand updatedUser);
    }
}
