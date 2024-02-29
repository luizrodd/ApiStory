using ApiStory.Requests;
using ApiStory.Response;
using ApiStory.Service.Interface;
using MediatR;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ApiStory.Handlers
{
    public class FindStoryByIdHandler : IRequestHandler<FindStoryByIdRequest, FindStoryByIdResponse>
    {
        IStoryService _storyService;

        public FindStoryByIdHandler(IStoryService storyService)
        {
            _storyService = storyService;
        }
        public async Task<FindStoryByIdResponse> Handle(FindStoryByIdRequest request, CancellationToken cancellationToken)
        {

            var result =  await _storyService.GetById(request.Id);
            if(result == null)
            {
                return null;
            }

            FindStoryByIdResponse findStoryByIdResponse = new FindStoryByIdResponse()
            {
                Description = result.Description,
                Id = result.Id,
                Title = result.Title,
            };

            return await Task.FromResult(findStoryByIdResponse);
        }
    }
}
