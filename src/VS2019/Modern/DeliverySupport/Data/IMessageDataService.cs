using DeliverySupport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public interface IMessageDataService
    {
        Task Create(IMessageModel message);
        Task DeleteTestMessages();
        Task<IMessageModel> GetByTimeLocation(DateTime time, string location);
        Task<List<IMessageModel>> GetMessagesAsync();
        Task<IMessageModel> GetMessageAsync(int id);
        Task DeleteMessagesBeforeTime(DateTime time);
    }
}