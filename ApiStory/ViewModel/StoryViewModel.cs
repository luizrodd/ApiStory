using ApiStory.Data.Models;

namespace ApiStory.ViewModel
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public IEnumerable<VoteViewModel> Votes { get; set; } = new List<VoteViewModel>();
    }
}
