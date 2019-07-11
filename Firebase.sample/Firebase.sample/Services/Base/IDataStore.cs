using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Firebase.sample.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItem(T item);
        Task<bool> DeleteItem(T item);
        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
