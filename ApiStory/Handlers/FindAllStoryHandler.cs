using ApiStory.Requests;
using ApiStory.Response;
using ApiStory.Service.DTO;
using ApiStory.Service;
using ApiStory.Service.Interface;
using MediatR;

namespace ApiStory.Handlers
{
    public class FindAllStoryHandler : IRequestHandler<FindAllStoryRequest, List<StoryResponse>>
    {
        IStoryService _storyService;

        public FindAllStoryHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }
        

        async Task<List<StoryResponse>> IRequestHandler<FindAllStoryRequest, List<StoryResponse>>.Handle(FindAllStoryRequest request, CancellationToken cancellationToken)
        {
         var story = await _storyService.GetAll();
            var storyResponse = story.Select(x => new StoryResponse
            {
                Id = x.Id,
                Title = x.Title,
                Area = x.Area,
                Description = x.Description,
                Votes = x.Votes.Select(y => new VoteResponse
                {
                    Like = y.Like,
                    Client = new ClientResponse
                    {
                        Name = y.Client.Name
                    }
                }).ToList(),
            }).ToList();

            return storyResponse;
        }
    }
}
