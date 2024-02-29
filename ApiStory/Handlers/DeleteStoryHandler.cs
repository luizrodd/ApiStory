using ApiStory.Requests;
using ApiStory.Service.Interface;
using MediatR;

namespace ApiStory.Handlers;

public class DeleteStoryHandler : IRequestHandler<DeleteStoryRequest, bool>
{
    IStoryService _storySerivce;

    public DeleteStoryHandler(IStoryService storySerivce)
    {
        _storySerivce = storySerivce;
    }
    public async Task<bool> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
    {
        await _storySerivce.Delete(request.Id);
        return await Task.FromResult(true);
    }
}
