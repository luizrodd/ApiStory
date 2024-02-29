using ApiStory.Response;
using MediatR;

namespace ApiStory.Requests
{
    public class FindAllStoryRequest : IRequest<List<StoryResponse>>
    {

    }
}
