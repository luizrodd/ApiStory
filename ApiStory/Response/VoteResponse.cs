using ApiStory.Data.Models;

namespace ApiStory.Response
{
    public class VoteResponse
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public ClientResponse Client { get; set; } = null;
        public bool Like { get; set; }
        public int StoryId { get; set; }
        public  StoryResponse Story { get; set; } = null;
    }
}
