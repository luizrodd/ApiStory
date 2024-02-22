using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Data.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = null;
        public bool Like { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; } = null;

    }
}
