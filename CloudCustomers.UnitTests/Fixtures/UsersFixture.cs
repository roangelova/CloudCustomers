using CloudCustomers.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User
                {
                    Id = 1,
                    Name = "Roslava",
                    Address = new Address()
                    {
                        Street = "First Str.",
                        City = "Varna",
                        ZipCode = "1246"
                    },
                    Email = "testRosi@abv.bg"
                },
                new User
                {
                    Id = 1,
                    Name = "Ivailo",
                    Address = new Address()
                    {
                        Street = "Second Str.",
                        City = "Varna",
                        ZipCode = "9000"
                    },
                    Email = "testRosi@abv.bg"
                },
                new User
                {
                    Id = 1,
                    Name = "Angel",
                    Address = new Address()
                    {
                        Street = "Third Str.",
                        City = "Varna",
                        ZipCode = "9023"
                    },
                    Email = "ivailo@abv.bg"
                }

            };
    }
}
