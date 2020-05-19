
using DeliverySupport.DataAccess;
using DeliverySupport.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public class ItemSqlDataService : IItemDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly ILogger<IItemDataService> _logger;
        public bool UseItemDelete { get; set; }

        public ItemSqlDataService(ISqlDataAccess dataAccess,
                                 ILogger<IItemDataService> logger,
                                 IConfiguration config)
        {
            _dataAccess = dataAccess;
            _logger = logger;


            string strValue = config["AppSettings:UseItemDelete"];
            if (string.IsNullOrEmpty(strValue) == true)
                strValue = "0";
            if (strValue == "1")
                UseItemDelete = true;
            else
                UseItemDelete = false;
        }


        public async Task CreateOrUpdateList(List<IItemModel> items)
        {
            foreach (IItemModel item in items)
                await CreateOrUpdateItem(item);
        }


        public async Task CreateOrUpdateItem(IItemModel item)
        {
            var p = new
            {
                item.ItemNum,
                item.Description,
                item.Amount,
                item.CategoryNum,
                item.DefaultQuantity,
                item.ImageFileName,
                item.IsTestObject
            };

            await _dataAccess.SaveData("dbo.spItem_CreateOrUpdate", p, "SQLDB");

        }



        public async Task<List<IItemModel>> GetItemsAsync()
        {
            //_logger.LogInformation("Getting all items");
            var items = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_Get",
                                                                        new { },
                                                                        "SQLDB");
            return items.ToList<IItemModel>();
        }

        public async Task<List<IItemModel>> GetItemsByCategoryNumAsync(int categoryNum)
        {
            //_logger.LogInformation("Getting all items with CategoryNum {categorynum} ", categoryNum );
            var items = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetByCategoryNum",
                                                                        new { CategoryNum = categoryNum },
                                                                        "SQLDB");
            return items.ToList<IItemModel>();
        }

        public async Task<IItemModel> GetItemAsync(int id)
        {
            var items = await _dataAccess.LoadData<ItemModel, dynamic>("dbo.spItems_GetOne",
                                                                        new { Id = id },
                                                                        "SQLDB");
            return items.FirstOrDefault();
        }



        // we need special store procedure

        public async Task DeleteTestItems()
        {
            await _dataAccess.SaveData("dbo.spItems_DeleteTest", new { }, "SQLDB");
        }

        public async Task DeleteItem(int id)
        {
            await _dataAccess.SaveData("dbo.spItems_Delete", new { Id = id }, "SQLDB");
        }

        public bool IsSimulated()
        {
            return false;
        }

        public bool IsUseItemDelete()
        {
            return UseItemDelete;
        }
    }
}
