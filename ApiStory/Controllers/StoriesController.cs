using ApiStory.Handlers;
using ApiStory.Requests;
using ApiStory.Response;
using ApiStory.Service;
using ApiStory.Service.DTO;
using ApiStory.Service.Interface;
using ApiStory.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;

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
        public async Task<IActionResult> GetAll([FromServices] IMediator mediator)
        {
            var request = new FindAllStoryRequest();
            var story = await mediator.Send(request);
            List<StoryViewModel> storyViewModel = story.Select(x => new StoryViewModel()
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

            if (storyViewModel.Count == 0)
            {
                return NoContent();
            }

            return StatusCode(200, storyViewModel);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("/id")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromServices] IMediator mediator, [FromQuery] FindStoryByIdRequest command)
        {
            var storyDTO = await mediator.Send(command);
            return Ok(storyDTO);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromServices] IMediator mediator, [FromBody] CreateStoryRequest command)
        {
            if (command.Title == null || command.Description == null || command.Area == null)
            {
                return BadRequest();
            }
            var response = await mediator.Send(command);
            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromServices] IMediator mediator, int id, [FromBody] UpdateStoryRequest command)
        {
            command.Id = id;
            if (command.Title == null || command.Description == null || command.Area == null)
            {
                return BadRequest();
            }
            var story = await mediator.Send(command);
            if (story == false)
            {
                return NotFound();
            }
            return StatusCode(200, "Alterado");
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromServices] IMediator mediator, [FromRoute] int id)
        {
            var command = new DeleteStoryRequest { Id = id };
            var story = await mediator.Send(command);
            if(!story)
            {
                return NotFound();
            }
            return StatusCode(200);
        }

    }
}


