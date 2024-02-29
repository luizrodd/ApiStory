using ApiStory.Data.Models;

namespace ApiStory.Response
{
    public class CreateStoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public IEnumerable<Vote> Votes { get; set; } = new List<Vote>();
    }
}
