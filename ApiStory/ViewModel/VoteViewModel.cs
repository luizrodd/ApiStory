using ApiStory.Service.DTO;

namespace ApiStory.ViewModel
{
    public class VoteViewModel
    {
        public bool Like { get; set; }
        public ClientViewModel Client { get; set; } 
        public StoryViewModel Story { get; set; } 
    }
}
