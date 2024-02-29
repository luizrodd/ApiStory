using ApiStory.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Requests
{
    public class FindStoryByIdRequest : IRequest<FindStoryByIdResponse>
    {
        public int Id { get; set; }
    }
}
