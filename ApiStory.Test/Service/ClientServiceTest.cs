using ApiStory.Data;
using ApiStory.Data.Models;
using ApiStory.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Test.Service
{
    public class ClientServiceTest
    {
        private readonly DataContext _dataContext;
        public ClientServiceTest() 
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dataContext = new DataContext(options);
        }
        [Fact]
        public async Task Create_WhenValidParamsClient_ReturnsTrue()
        {
            string name = "Luiz";

            var clientService = new ClientService(_dataContext);

            var result = await clientService.Create(name);

            Assert.True(result);
        }
        [Fact]
        public async Task Create_WhenInvalidParamsClient_ReturnsFalse()
        {
            Client client = new Client();
            var clientService = new ClientService(_dataContext);

            var result = await clientService.Create(client.Name);

            Assert.False(result);
        }
    }
}
