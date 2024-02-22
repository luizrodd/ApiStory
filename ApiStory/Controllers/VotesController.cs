using ApiStory.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiStory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _voteService;
        public VotesController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create(bool like, int storyId, int clientId)
        {
            var vote = await _voteService.Create(like,storyId,clientId);
            if(vote == false)
            {
                return BadRequest();
            }
            return StatusCode(201, "Criado");
        }
    }
}
