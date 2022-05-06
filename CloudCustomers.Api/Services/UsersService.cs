using CloudCustomers.Api.Models;

namespace CloudCustomers.Api.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }



    public class UsersService : IUsersService
    {

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
