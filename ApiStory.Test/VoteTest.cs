using ApiStory.Controllers;
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
    public class VoteTest
    {
        [Fact]
        public async Task Create_WhenValidVote_ReturnsOk()
        {   
            var mockServices = new Mock<IVoteService>();
            bool like = true;
            int clientId = 1;
            int storyId = 1;

            mockServices.Setup(x => x.Create(like, storyId, clientId)).ReturnsAsync(true);

            var controller = new VotesController(mockServices.Object);

            var result = await controller.Create(like,storyId,clientId);

            Assert.NotNull(result);

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(201, obj.StatusCode);

        }
        [Fact]
        public async Task Create_WhenNotValidVote_ReturnsBadRequest()
        {
            var mockServices = new Mock<IVoteService>();
            bool like = true;
            int clientId = 1;
            int storyId = 1;

            mockServices.Setup(x => x.Create(like, storyId, clientId)).ReturnsAsync(false);

            var controller = new VotesController(mockServices.Object);

            var result = await controller.Create(like, storyId, clientId);

            Assert.NotNull(result);

            var obj = result as BadRequestResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(400, obj.StatusCode);
        }
    }
}
