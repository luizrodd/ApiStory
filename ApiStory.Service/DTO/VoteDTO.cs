using ApiStory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service.DTO
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public bool Like { get; set; }
        public int ClientId { get; set; }
        public int StoryId { get; set; }
        public ClientDTO Client { get; set; } = null;
        public StoryDTO Story { get; set; } = null;
    }
}
