using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Response
{
    public class FindStoryByIdResponse 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
         
    }
}
