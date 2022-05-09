using CloudCustomers.Api.Models;
using CloudCustomers.Api.Models.Config;
using Microsoft.Extensions.Options;

namespace CloudCustomers.Api.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }



    public class UsersService : IUsersService
    {
        private readonly HttpClient httpClient;
        private readonly UsersApiOptions apiConfig;

        public UsersService(
            HttpClient _httpClient,
            IOptions<UsersApiOptions> _apiConfig)
        {
            httpClient = _httpClient;
            apiConfig = _apiConfig.Value;

        }

        public async Task<List<User>> GetAllUsers()
        {
            var response = await httpClient.GetAsync(apiConfig.Endpoint);

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
