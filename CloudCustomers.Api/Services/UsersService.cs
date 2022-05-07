using CloudCustomers.Api.Models;

namespace CloudCustomers.Api.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }



    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;

        public UsersService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
