using DeliverySupport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data.Sim
{
    public class MessageSimDataService : IMessageDataService
    {
        private List<IMessageModel> _messages = new List<IMessageModel>();
        private int _nextId = 1;

        public async Task Create(IMessageModel message)
        {
            await Task.Run(() =>
            {
                message.Id = _nextId++;
                _messages.Add(message);
            });

        }

        public async Task<IMessageModel> GetByTimeLocation(DateTime timeCreated, string locationCreated)
        {
            return await Task.FromResult(_messages.Where(x =>
                                         (x.LocationCreated != locationCreated) &&
                                         (x.TimeCreated > timeCreated)).FirstOrDefault());
        }


        public async Task<List<IMessageModel>> GetMessagesAsync()
        {
            return await Task.FromResult(_messages);

        }

        public async Task<IMessageModel> GetMessageAsync(int id)
        {
            return await Task.FromResult(_messages.Where(x => x.Id == id).FirstOrDefault());

        }

        public async Task DeleteTestMessages()
        {
            await Task.Run(() => { _messages.Remove(_messages.Where(x => x.IsTestObject == true).FirstOrDefault()); });
        }

        public async Task DeleteMessage(int id)
        {
            await Task.Run(() => { _messages.Remove(_messages.Where(x => x.Id == id).FirstOrDefault()); });

        }

        public async Task DeleteMessagesBeforeTime(DateTime time)
        {
            await Task.Run(() => { _messages.Remove(_messages.Where(x => x.TimeCreated < time).FirstOrDefault()); });

        }
    }
}
