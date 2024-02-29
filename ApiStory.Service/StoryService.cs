using ApiStory.Data;
using ApiStory.Data.Models;
using ApiStory.Service.DTO;
using ApiStory.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service;

public class StoryService(DataContext data) : IStoryService
{
    private readonly DataContext _data = data;

    public async Task<List<StoryDTO>> GetAll()
    {
        var story = await _data.Stories
            .Select(s => new StoryDTO()
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Area = s.Area,
                Votes = s.Votes.Select(y => new VoteDTO()
                {
                    Like = y.Like,
                    Client = new ClientDTO()
                    {
                        Name = y.Client.Name,
                    }
                })
            }).OrderByDescending(x => x.Votes.Sum(x => x.Like ? 1 : -1))
            .ToListAsync();
        return story;
    }
    public async Task<StoryDTO> GetById(int id)
    {
        var findId = await _data.Stories.Include(s => s.Votes).ThenInclude(x => x.Client).FirstOrDefaultAsync(x => x.Id == id);
        if (findId == null)
        {
            return null;
        }
        StoryDTO storyDTO = new StoryDTO()
        {
            Area = findId.Area,
            Title = findId.Title,
            Description = findId.Description,
            Votes = findId.Votes.Select(x => new VoteDTO()
            {
                Like = x.Like,
                Client = new ClientDTO() { Name = x.Client.Name }
            }).ToList()
        };
        return storyDTO; 
    }

    public async Task<bool> Create(string title, string description, string area)
    {
        if (title == null || description == null || area == null)
        {
            return false;
        }
        Story story = new Story
        {
            Title = title,
            Description = description,
            Area = area
        };
        await _data.Stories.AddAsync(story);
        await _data.SaveChangesAsync();
            return true;
    }
    public async Task<bool> Update(int id, string title, string description, string area)
    {
        var findId = await _data.Stories.FirstOrDefaultAsync(x => x.Id == id);
        if (findId == null || title == null || description == null || area == null)
        {
            return false;
        }
        findId.Title = title;
        findId.Description = description;
        findId.Area = area;
        _data.Stories.Update(findId);
        await _data.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Delete(int id)
    {
        Story storyToDelete = await _data.Stories.FindAsync(id);

        if (storyToDelete == null)
        {
            return false;
        }
        else
        {
            _data.Stories.Remove(storyToDelete);
            await _data.SaveChangesAsync();
            return true;
        }
    }
}

