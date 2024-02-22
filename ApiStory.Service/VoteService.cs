using ApiStory.Data;
using ApiStory.Data.Models;
using ApiStory.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service
{
    public class VoteService(DataContext data) : IVoteService
    {
        private readonly DataContext _data = data;
        public async Task<bool> Create(bool like, int storyId, int clientId)
        {
            var client =  _data.Clients.FirstOrDefault(x => x.Id == clientId);
            var story = _data.Stories.FirstOrDefault(x => x.Id == storyId);
            if (client == null || story == null) 
            {
                return false;
            }
            Vote vote = new Vote()
            {
                Like = like,
                StoryId = storyId,
                ClientId = clientId
            };
            await _data.Votes.AddAsync(vote);
            await _data.SaveChangesAsync();
            return true;
        }
    }
}
