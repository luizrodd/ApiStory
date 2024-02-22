using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service.Interface
{
    public interface IVoteService
    {
        Task<bool> Create(bool like, int storyId, int clientId);
    }
}

