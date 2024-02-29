namespace ApiStory.Response
{
    public class StoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public IEnumerable<VoteResponse> Votes { get; set; } = new List<VoteResponse>();
    }
}
