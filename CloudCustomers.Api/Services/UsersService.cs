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

        public async Task<List<User>> GetAllUsers()
        {
            var response = await httpClient.GetAsync("https://example.com");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }

            var responseContent = response.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();

            return allUsers.ToList();
        }
    }
}
