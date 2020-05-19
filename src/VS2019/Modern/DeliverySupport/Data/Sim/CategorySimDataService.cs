using DeliverySupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data.Sim
{
    public class CategorySimDataService : ICategoryDataService
    {
        private bool _isDataInitialized { get; set; }
        private List<IItemCategoryModel> _itemCategories = new List<IItemCategoryModel>();
        private int _nextId = 1;

        public CategorySimDataService()
        {
            _isDataInitialized = false;
        }

        public async Task CreateOrUpdateDefaultList()
        {
            DataFactory Factory = new DataFactory();
            List<IItemCategoryModel> ItemCategories = Factory.CreateDefaultItemCategoryList();

            _isDataInitialized = true;
            await CreateOrUpdateList(ItemCategories);
        }

        public async Task CreateOrUpdateList(List<IItemCategoryModel> itemCategories)
        {
            foreach (IItemCategoryModel itemCategory in itemCategories)
                await CreateOrUpdateItemCategory(itemCategory);
        }


        public async Task CreateOrUpdateItemCategory(IItemCategoryModel item)
        {
            if (_isDataInitialized == false)
                await CreateOrUpdateDefaultList();

            IItemCategoryModel NewItem = item;

            foreach (IItemCategoryModel WorkItem in _itemCategories)
            {
                if (WorkItem.CategoryNum == item.CategoryNum)
                {
                    _itemCategories.Remove(NewItem);
                }
            }

            item.Id = _nextId++;
            _itemCategories.Add(item);

            await Task.Delay(0);
        }



        public async Task<List<IItemCategoryModel>> GetItemCategoriesAsync()
        {
            if (_isDataInitialized == false)
                await CreateOrUpdateDefaultList();

            return await Task.FromResult(_itemCategories);
        }

        public async Task<IItemCategoryModel> GetItemCategoryByCategoryNumAsync(int categoryNum)
        {
            if (_isDataInitialized == false)
                await CreateOrUpdateDefaultList();

            return await Task.FromResult(_itemCategories.Where(x => x.CategoryNum == categoryNum).FirstOrDefault());
        }


        public async Task<IItemCategoryModel> GetItemCategoryAsync(int id)
        {
            if (_isDataInitialized == false)
                await CreateOrUpdateDefaultList();

            return await Task.FromResult(_itemCategories.Where(x => x.Id == id).FirstOrDefault());

        }

        public async Task DeleteCategory(int id)
        {
            if (_isDataInitialized == false)
                await CreateOrUpdateDefaultList();

            await Task.Run(() => { _itemCategories.Remove(_itemCategories.Where(x => x.Id == id).FirstOrDefault()); });

        }
    }
}
