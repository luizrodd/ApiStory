using ApiStory.Controllers;
using ApiStory.Service.DTO;
using ApiStory.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Test
{
    public class ClientTest
    {
        [Fact]
        public async Task Create_WhenValidClient_ReturnsOk()
        {
            var mockServices = new Mock<IClientService>();
            string name = "Teste";

            mockServices.Setup(repo => repo.Create(name)).ReturnsAsync(true);

            var controller = new ClientsController(mockServices.Object);

            var result = await controller.Create(name);

            Assert.NotNull(result);
            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(201, obj.StatusCode);
        }
        [Fact]
        public async Task Create_WhenNotValidClient_ReturnsBadRequest()
        {
            var mockServices = new Mock<IClientService>();
            string name = null;

            mockServices.Setup(repo => repo.Create(name)).ReturnsAsync(false);

            var controller = new ClientsController(mockServices.Object);

            var result = await controller.Create(name);

            Assert.NotNull(result);
            var obj = result as BadRequestResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(400, obj.StatusCode);
        }
    }
}
