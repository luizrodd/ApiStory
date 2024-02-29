using ApiStory.Requests;
using ApiStory.Service;
using ApiStory.Service.Interface;
using MediatR;
using NuGet.Protocol.Plugins;

namespace ApiStory.Handlers
{
    public class UpdateStoryHandler : IRequestHandler<UpdateStoryRequest, bool>
    {
        IStoryService _storyService;
        public UpdateStoryHandler(IStoryService storyService) 
        {
            _storyService = storyService;
        }

        public async Task<bool> Handle(UpdateStoryRequest request, CancellationToken cancellationToken)
        {
            await _storyService.Update(request.Id,request.Title, request.Description, request.Area);

            return await Task.FromResult(true);
        }
    }
}
