using ApiStory.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service.Interface;

public interface IStoryService
{
    Task<List<StoryDTO>> GetAll();
    Task<StoryDTO> GetById(int id);
    Task<bool> Create(string title, string description, string area);
    Task<bool> Update(int id,string title, string description, string area);
    Task<bool> Delete(int id);
}
