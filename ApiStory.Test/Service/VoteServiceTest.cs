using ApiStory.Data;
using ApiStory.Data.Models;
using ApiStory.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Test.Service
{
    public class VoteServiceTest
    {
        private readonly DataContext _dataContext;
        public VoteServiceTest()
        {

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _dataContext = new DataContext(options);

        }
        [Fact]
        public async Task Create_WhenValidVoteService_ReturnsTrue()
        {

            var testVote = new Vote
            {
                Id = 1,
                ClientId = 1,
                StoryId = 1,
                Like = true
            };

            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 1 });
            _dataContext.Clients.Add(new Client { Id = 1, Name = "Luiz" });
            await _dataContext.SaveChangesAsync();
            var voteService = new VoteService(_dataContext);

            var result = await voteService.Create(testVote.Like, testVote.StoryId, testVote.ClientId);

            Assert.True(result);

            var createdVote = await _dataContext.Votes.FirstOrDefaultAsync(v => v.ClientId == 1 && v.StoryId == 1);

            Assert.NotNull(createdVote);
            Assert.Equal(1, createdVote.ClientId);
            Assert.Equal(1, createdVote.StoryId);
            Assert.True(createdVote.Like);

        }
        [Fact]
        public async Task Create_WhenInvalidVoteService_ReturnsFalse()
        {
            var testVote = new Vote
            {
                Id = 1,
                ClientId = 1,
                StoryId = 1,
                Like = true
            };

            _dataContext.Stories.Add(new Story { Area = "Rh", Description = "Rh", Title = "Rh", Id = 2 });
            _dataContext.Clients.Add(new Client { Name = "Luiz", Id = 2 });
            await _dataContext.SaveChangesAsync();
            var voteService = new VoteService(_dataContext);

            var result = await voteService.Create(testVote.Like, testVote.StoryId, testVote.ClientId);

            Assert.False(result);

        }
    }
}
