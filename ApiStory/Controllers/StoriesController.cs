using ApiStory.Service.DTO;
using ApiStory.Service.Interface;
using ApiStory.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json.Linq;

namespace ApiStory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<StoryDTO> storyDTO = await _storyService.GetAll();
            List<StoryViewModel> storyViewModel = storyDTO.Select(x => new StoryViewModel()
            {
                Id = x.Id,
                Area = x.Area,
                Description = x.Description,
                Title = x.Title,
                Votes = x.Votes.Select(y => new VoteViewModel()
                {
                    Like = y.Like,
                    Client = new ClientViewModel()
                    {
                        Name = y.Client.Name,
                    }
                })
            })
                .ToList();

            if(storyViewModel.Count == 0)
            {
                return NoContent();
            }

            return StatusCode(200, storyViewModel);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var storyDTO = await _storyService.GetById(id);
            if (storyDTO != null)
            {
                StoryViewModel storyViewModel = new StoryViewModel()
                {
                    Area = storyDTO.Area,
                    Description = storyDTO.Description,
                    Title = storyDTO.Title,
                    Votes = storyDTO.Votes.Select(x => new VoteViewModel()
                    {
                        Like = x.Like,
                        Client = new ClientViewModel() { Name = x.Client.Name }
                    }).ToList()
                };
                return Ok(storyViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(string title, string description, string area)
        {
            if(title == null || description == null || area == null) 
            {
                return BadRequest();
            }
            await _storyService.Create(area,title,description);

            return StatusCode(201,"Criado"); 
        }
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, string title, string description, string area)
        {

            if(title == null || description == null || area == null)
            {
                return BadRequest();
            }
            var story = await _storyService.Update(id,title,description,area); 
            if(story == false)
            {
                return NotFound();
            }
            return StatusCode(200, "Alterado");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {

            var story = await _storyService.Delete(id);
            if(story == false)
            {
                return NotFound();
            }
            else
            {
                return StatusCode(200, "Deletado");
            }
        }

    }
}


