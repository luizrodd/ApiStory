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
    public class StoryServiceTest
    {
        private readonly DataContext _dataContext;
        public StoryServiceTest()
        {
            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dataContext = new DataContext(options);
        }

        [Fact]
        public async Task GetAll_ExistingContent_ReturnsStories()
        {
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 2 });
            await _dataContext.SaveChangesAsync();

            var storyService = new StoryService(_dataContext);

            var result = await storyService.GetAll();

            Assert.NotNull(result);
            Assert.Equal("Rh", result[0].Title);
            Assert.Equal("Rh", result[0].Description);
            Assert.Equal("Rh", result[0].Area);
        }
        [Fact]
        public async Task Create_WhenValidStoryParams_ReturnsTrue()
        {
            string title = "rh";
            string description = "rh";
            string area = "rh";

            var storyService = new StoryService(_dataContext);

            var result = await storyService.Create(title, description, area);

            Assert.True(result);
            Assert.Equal("rh", title);
            Assert.Equal("rh", description);
            Assert.Equal("rh", area);
        }
        [Fact]
        public async Task Create_WhenInvalidStoryParams_ReturnsFalse()
        {
            Story story = new Story();

            var storyService = new StoryService(_dataContext);

            var result = await storyService.Create(story.Title, story.Description, story.Area);

            Assert.False(result);

        }
        [Fact]
        public async Task Update_WhenValidStoryParams_ReturnsTrue()
        {
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            await _dataContext.SaveChangesAsync();

            int id = 1;
            Story story = new Story();
            story.Title = "Title";
            story.Description = "Description";
            story.Area = "Area";

            var storyService = new StoryService(_dataContext);
            var result = await storyService.Update(id, story.Title, story.Description, story.Area);

            Assert.True(result);
        }
        [Theory]
        [InlineData(-1, null, null, null)]
        [InlineData(1, null, null, null)]
        [InlineData(1, "title", null, null)]
        [InlineData(1, "title", "description", null)]
        [InlineData(-1, "title", "description", "area")]
        public async Task Update_WhenInvalidStoryParams_ReturnsFalse(int id, string title, string description, string area)
        {
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            await _dataContext.SaveChangesAsync();

            var storyService = new StoryService(_dataContext);
            var result = await storyService.Update(id, title, description, area);

            Assert.False(result);
        }
        [Fact]
        public async Task Delete_WhenValidStoryId_ReturnsTrue()
        {
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            await _dataContext.SaveChangesAsync();

            var storyService = new StoryService(_dataContext);
            var result = await storyService.Delete(1);

            Assert.True(result);
        }
        [Fact]
        public async Task Delete_WhenInvalidStoryId_ReturnsFalse()
        {
            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            await _dataContext.SaveChangesAsync();

            var storyService = new StoryService(_dataContext);
            var result = await storyService.Delete(2);

            Assert.False(result);
        }
    }
}
