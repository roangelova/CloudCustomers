using CloudCustomers.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace CloudCustomers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService _usersService)
        {
            usersService = _usersService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await usersService.GetAllUsers();

            if (users.Any())
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }

        }
    }
}