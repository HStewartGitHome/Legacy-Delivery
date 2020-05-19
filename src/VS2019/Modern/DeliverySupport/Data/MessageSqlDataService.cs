
using DeliverySupport.DataAccess;
using DeliverySupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public class MessageSqlDataService : IMessageDataService
    {
        private readonly ISqlDataAccess _dataAccess;

        public MessageSqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task Create(IMessageModel message)
        {
            var p = new
            {
                message.ToName,
                message.FromName,
                message.MessageText,
                message.TimeCreated,
                message.LocationCreated,
                message.IsTestObject
            };

            await _dataAccess.SaveData("dbo.spMessage_Create", p, "SQLDB");
        }

        public async Task<IMessageModel> GetByTimeLocation(DateTime timeCreated, string locationCreated)
        {

            var p = new
            {
                timeCreated,
                locationCreated
            };

            var messages = await _dataAccess.LoadData<MessageModel, dynamic>("dbo.spMessages_GetTimeLocation",
                                                                   p,
                                                                   "SQLDB");
            return messages.FirstOrDefault();
        }


        public async Task<List<IMessageModel>> GetMessagesAsync()
        {
            var messages = await _dataAccess.LoadData<MessageModel, dynamic>("dbo.spMessages_Get",
                                                                              new { },
                                                                              "SQLDB");
            return messages.ToList<IMessageModel>();
        }

        public async Task<IMessageModel> GetMessageAsync(int id)
        {
            var messages = await _dataAccess.LoadData<MessageModel, dynamic>("dbo.spMessages_GetOne",
                                                                              new { Id = id },
                                                                              "SQLDB");
            return messages.FirstOrDefault();
        }

        public async Task DeleteTestMessages()
        {
            await _dataAccess.SaveData("dbo.spMessages_DeleteTest", new { }, "SQLDB");
        }

        public async Task DeleteMessage(int id)
        {
            await _dataAccess.SaveData("dbo.spMessages_Delete", new { Id = id }, "SQLDB");
        }

        public async Task DeleteMessagesBeforeTime(DateTime time)
        {
            await _dataAccess.SaveData("dbo.spMessages_DeleteBeforeDate", new { Time = time }, "SQLDB");
        }
    }
}
