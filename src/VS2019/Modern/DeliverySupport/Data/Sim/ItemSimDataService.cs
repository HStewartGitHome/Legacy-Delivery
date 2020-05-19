using DeliverySupport.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data.Sim
{
    public class ItemSimDataService : IItemDataService
    {
        private readonly ILogger<IItemDataService> _logger;
        private List<IItemModel> _items = new List<IItemModel>();
        private bool _isDataInitialized { get; set; }
        private int _nextId = 1;
        public bool UseItemDelete { get; set; }


        public ItemSimDataService(ILogger<IItemDataService> logger,
                                  IConfiguration config)
        {
            _logger = logger;
            _isDataInitialized = false;

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
            if (_isDataInitialized == false)
                InitializeData();

            IItemModel NewItem = item;

            foreach (IItemModel WorkItem in _items)
            {
                if (WorkItem.ItemNum == item.ItemNum)
                {
                    _items.Remove(NewItem);
                }
            }
            item.Id = _nextId++;
            _items.Add(item);

            await Task.Delay(0);
        }



        public async Task<List<IItemModel>> GetItemsAsync()
        {
            if (_isDataInitialized == false)
                InitializeData();

            return await Task.FromResult(_items);

        }

        public async Task<List<IItemModel>> GetItemsByCategoryNumAsync(int categoryNum)
        {
            if (_isDataInitialized == false)
                InitializeData();

            List<IItemModel> WorkItems = new List<IItemModel>();

            foreach (IItemModel WorkItem in _items)
            {
                if (WorkItem.CategoryNum == categoryNum)
                    WorkItems.Add(WorkItem);
            }

            await Task.Delay(0);
            return WorkItems;

        }

        public async Task<IItemModel> GetItemAsync(int id)
        {
            if (_isDataInitialized == false)
                InitializeData();

            return await Task.FromResult(_items.Where(x => x.Id == id).FirstOrDefault());

        }

        public async Task DeleteTestItems()
        {
            if (_isDataInitialized == false)
                InitializeData();

            await Task.Run(() => { _items.Remove(_items.Where(x => x.IsTestObject == true).FirstOrDefault()); });

        }

        public async Task DeleteItem(int id)
        {
            if (_isDataInitialized == false)
                InitializeData();

            await Task.Run(() => { _items.Remove(_items.Where(x => x.Id == id).FirstOrDefault()); });

        }

        internal void InitializeData()
        {
            _items = InitializeItems.CreateDefaultItems();

            _nextId = InitializeItems.NextID;
            _isDataInitialized = true;

        }

        public bool IsSimulated()
        {
            return true;
        }

        public bool IsUseItemDelete()
        {
            return UseItemDelete;
        }
    }
}
