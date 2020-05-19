using DeliverySupport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public interface ICategoryDataService
    {
        Task CreateOrUpdateDefaultList();
        Task CreateOrUpdateItemCategory(IItemCategoryModel item);
        Task CreateOrUpdateList(List<IItemCategoryModel> itemCategories);
        Task DeleteCategory(int id);
        Task<List<IItemCategoryModel>> GetItemCategoriesAsync();
        Task<IItemCategoryModel> GetItemCategoryAsync(int id);
        Task<IItemCategoryModel> GetItemCategoryByCategoryNumAsync(int categoryNum);
    }
}