using DeliverySupport.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public interface IItemDataService
    {
        Task CreateOrUpdateList(List<IItemModel> items);
        Task DeleteTestItems();
        Task DeleteItem(int id);
        Task<List<IItemModel>> GetItemsAsync();
        Task<List<IItemModel>> GetItemsByCategoryNumAsync(int categoryNum);
        Task<IItemModel> GetItemAsync(int id);
        Task CreateOrUpdateItem(IItemModel item);
        bool IsSimulated();
        bool IsUseItemDelete();
    }
}