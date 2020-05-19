using DeliverySupport.DataAccess;
using DeliverySupport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public class CategorySqlDataService : ICategoryDataService
    {
        private readonly ISqlDataAccess _dataAccess;

        public CategorySqlDataService(ISqlDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task CreateOrUpdateDefaultList()
        {
            DataFactory Factory = new DataFactory();
            List<IItemCategoryModel> ItemCategories = Factory.CreateDefaultItemCategoryList();

            await CreateOrUpdateList(ItemCategories);
        }

        public async Task CreateOrUpdateList(List<IItemCategoryModel> itemCategories)
        {
            foreach (IItemCategoryModel itemCategory in itemCategories)
                await CreateOrUpdateItemCategory(itemCategory);
        }


        public async Task CreateOrUpdateItemCategory(IItemCategoryModel item)
        {
            var p = new
            {
                item.CategoryNum,
                item.CategoryDescription
            };
            await _dataAccess.SaveData("dbo.spItemCategory_CreateOrUpdate", p, "SQLDB");

        }



        public async Task<List<IItemCategoryModel>> GetItemCategoriesAsync()
        {
            var itemCategories = await _dataAccess.LoadData<ItemCategoryModel, dynamic>("dbo.spItemCategorie_Get",
                                                                        new { },
                                                                        "SQLDB");
            return itemCategories.ToList<IItemCategoryModel>();
        }

        public async Task<IItemCategoryModel> GetItemCategoryByCategoryNumAsync(int categoryNum)
        {
            var itemCategories = await _dataAccess.LoadData<ItemCategoryModel, dynamic>("dbo.spItemCategories_GetByCategoryNum",
                                                                        new { CategoryNum = categoryNum },
                                                                        "SQLDB");
            return itemCategories.FirstOrDefault();
        }

        public async Task<IItemCategoryModel> GetItemCategoryAsync(int id)
        {
            var itemCategories = await _dataAccess.LoadData<ItemCategoryModel, dynamic>("dbo.spItemCategories_GetOne",
                                                                        new { Id = id },
                                                                        "SQLDB");
            return itemCategories.FirstOrDefault();
        }

        public async Task DeleteCategory(int id)
        {
            await _dataAccess.SaveData("dbo.spItemCategories_Delete", new { Id = id }, "SQLDB");
        }
    }
}
