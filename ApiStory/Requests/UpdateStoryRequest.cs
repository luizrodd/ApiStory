using MediatR;

namespace ApiStory.Requests
{
    public class UpdateStoryRequest : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
    }
}
