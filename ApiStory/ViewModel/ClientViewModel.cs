using ApiStory.Service.DTO;

namespace ApiStory.ViewModel
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        public IEnumerable<VoteViewModel> Votes { get; set; } = new List<VoteViewModel>();
    }
}
