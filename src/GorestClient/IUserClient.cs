using EmployeeManager.Data;
using EmployeeManager.Data.Common;
using GorestClient.Data;

namespace GorestClient
{
    public interface IUserClient
    {
        PagedResponse<User[]> GetAll(int page);
        PagedResponse<User[]> GetByName(string firstName);
        User GetById(int id);

        ActionResult<User> Create(User user);
        ActionResult<User> Update(User user);
        ActionResult Delete(int id);
    }
}
