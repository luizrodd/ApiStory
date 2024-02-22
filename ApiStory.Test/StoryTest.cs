using ApiStory.Controllers;
using ApiStory.Data.Models;
using ApiStory.Service;
using ApiStory.Service.DTO;
using ApiStory.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ApiStory.Test
{
    public class StoryTest
    {


        [Fact]
        public async Task GetAll_ExistingContent_ReturnsOkWithListStoryViewModel()
        {
            var mockServices = new Mock<IStoryService>();
            var stories = new List<StoryDTO>()
            {
                new StoryDTO {
                    Area = "RH",
                    Description = "Work",
                    Title = "WORK HARD",
                    Votes = new List<VoteDTO>()
                    {
                        new VoteDTO{
                            Like = true
                        },
                        new VoteDTO{
                            Like = false
                        }
                    }
                },
                new StoryDTO {
                    Area = "Infra",
                    Description = "PC",
                    Title = "WORK HARD",
                    Votes = new List<VoteDTO>()
                    {
                        new VoteDTO{
                            Like = false
                        },
                        new VoteDTO{
                            Like = false
                        }
                    }
                }
            };
            mockServices.Setup(repo => repo.GetAll()).ReturnsAsync(stories);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.GetAll();

            Assert.NotNull(result); // Ensure the result is not null

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(200, obj.StatusCode);
        }
        [Fact]
        public async Task GetAll_NoContent_ReturnsNoCotent()
        {
            var mockServices = new Mock<IStoryService>();
            var stories = new List<StoryDTO>() { };
            mockServices.Setup(repo => repo.GetAll()).ReturnsAsync(stories);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.GetAll();

            Assert.NotNull(result);

            var obj = result as NoContentResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(204, obj.StatusCode);
        }
        [Fact]
        public async Task GetById_ExistingId_ReturnsOkWithStoryViewModel()
        {
            var mockServices = new Mock<IStoryService>();
            StoryDTO stories = new StoryDTO()
            {
                Id = 1,
                Area = "RH",
                Description = "RH",
                Title = "Title",
                Votes = new List<VoteDTO>()
                {
                    new VoteDTO()
                    {
                        Id = 1,
                        Like = true,
                        Client = new ClientDTO()
                        {
                            Name = "Luiz"
                        }
                    }
                }
            };
            mockServices.Setup(repo => repo.GetById(stories.Id)).ReturnsAsync(stories);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.GetById(stories.Id);
            Assert.NotNull(result);

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(200, obj.StatusCode);
        }
        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 999;
            var mockStoryService = new Mock<IStoryService>();
            mockStoryService.Setup(service => service.GetById(It.IsAny<int>())).ReturnsAsync((StoryDTO)null);

            var controller = new StoriesController(mockStoryService.Object);

            // Act
            var result = await controller.GetById(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task Create_WhenValidStory_ReturnsTrue()
        {
            var mockServices = new Mock<IStoryService>();
            string area = "RH";
            string title = "work";
            string description = "Work hard";

            mockServices.Setup(repo => repo.Create(title, description, area)).ReturnsAsync(true);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Create(title, area, description);

            Assert.NotNull(result);

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(201, obj.StatusCode);

        }
        [Fact]
        public async Task Create_NoContentRequest_ReturnsBadRequest()
        {
            var mockServices = new Mock<IStoryService>();
            string area = null;
            string title = null;
            string description = null;

            mockServices.Setup(repo => repo.Create(title, description, area)).ReturnsAsync(false);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Create(title, area, description);

            Assert.NotNull(result);

            var obj = result as BadRequestResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(400, obj.StatusCode);

        }
        [Fact]
        public async Task Update_ValidData_ReturnsOk()
        {
            var mockServices = new Mock<IStoryService>();
            int id = 1;
            string area = "rh";
            string title = "rh";
            string description = "rh";

            mockServices.Setup(repo => repo.Update(id, title, description, area)).ReturnsAsync(true);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Update(id, title, area, description);

            Assert.NotNull(result);

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(200, obj.StatusCode);
        }
        [Fact]
        public async Task Update_InvalidId_ReturnsNotFound()
        {
            var mockServices = new Mock<IStoryService>();
            StoryDTO storyDTO = new StoryDTO()
            {
                Area = "RH",
                Description = "RH",
                Title = "rh",
            };

            mockServices.Setup(repo => repo.Update(storyDTO.Id, storyDTO.Title, storyDTO.Description, storyDTO.Area)).ReturnsAsync(false);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Update(storyDTO.Id, storyDTO.Title, storyDTO.Description, storyDTO.Area);

            Assert.IsType<NotFoundResult>(result);

            var obj = result as NotFoundResult;

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(404, obj.StatusCode);

        }
        [Fact]
        public async Task Update_InvalidData_ReturnsBadRequest()
        {
            var mockServices = new Mock<IStoryService>();
            StoryDTO storyDTO = new StoryDTO() { };

            mockServices.Setup(repo => repo.Update(storyDTO.Id, storyDTO.Title, storyDTO.Description, storyDTO.Area)).ReturnsAsync(false);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Update(storyDTO.Id, storyDTO.Title, storyDTO.Description, storyDTO.Area);

            Assert.NotNull(result);

            var obj = result as BadRequestResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(400, obj.StatusCode);

        }
        [Fact]
        public async Task Delete_ExistingId_ReturnsOk()
        {
            var mockServices = new Mock<IStoryService>();
            int id = 1;
            var storyDTO = new StoryDTO() { };

            mockServices.Setup(repo => repo.Delete(id)).ReturnsAsync(true);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Delete(id);

            var obj = result as ObjectResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(200, obj.StatusCode);
        }
        [Fact]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            var mockServices = new Mock<IStoryService>();
            int id = 1;
            var storyDTO = new StoryDTO() { };

            mockServices.Setup(repo => repo.Delete(id)).ReturnsAsync(false);

            var controller = new StoriesController(mockServices.Object);

            var result = await controller.Delete(id);

            var obj = result as NotFoundResult;
            Assert.NotNull(obj);

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"ObjectResult: {obj}");

            Assert.Equal(404, obj.StatusCode);
        }
    }
}