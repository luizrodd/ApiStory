
using ApiStory.Data.Models;
using ApiStory.Requests;
using ApiStory.Response;
using ApiStory.Service;
using ApiStory.Service.Interface;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiStory.Handlers;

public class CreateStoryHandler : IRequestHandler<CreateStoryRequest, CreateStoryResponse>
{
    IStoryService _storySerivce;

    public CreateStoryHandler(IStoryService storySerivce)
    {
        _storySerivce = storySerivce;
    }
    public Task<CreateStoryResponse> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
    {
        var story = new Story() { Title = request.Title, Area = request.Area, Description = request.Description };
        _storySerivce.Create(story.Title, story.Description, story.Area);

        var result = new CreateStoryResponse
        {
            Area = story.Area,
            Description = story.Description,
            Title = story.Title,
        };
        return Task.FromResult(result);
    }

}
