using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Vote> Votes { get; } = new List<Vote>();
    }
}
