using ApiStory.Data;
using ApiStory.Data.Models;
using ApiStory.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiStory.Service
{
    public class ClientService(DataContext data) : IClientService
    {
        private readonly DataContext _data = data;

        public async Task<bool> Create(string name)
        {
            if(name == null)
            {
                return false;
            }
            Client client = new Client()
            {
                Name = name
            };
            _data.Clients.Add(client);
            _data.SaveChanges();
            return true;
        }
    }
}
