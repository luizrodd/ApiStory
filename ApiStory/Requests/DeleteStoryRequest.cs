using MediatR;

namespace ApiStory.Requests
{
    public class DeleteStoryRequest : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
