using ApiStory.Response;
using MediatR;

namespace ApiStory.Requests;

public class CreateStoryRequest : IRequest<CreateStoryResponse>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Area { get; set; }
}
