using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStory.Service.Interface
{
    public interface IClientService
    {
        Task<bool> Create(string name);
    }
}
