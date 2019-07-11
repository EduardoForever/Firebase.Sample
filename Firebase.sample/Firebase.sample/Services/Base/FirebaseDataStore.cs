using Firebase.sample.Models;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Offline;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase.sample.Services
{
    public class FirebaseDataStore<T> : IDataStore<T>
        where T : FirebaseModel
    {
        private readonly ChildQuery _query;
        private readonly RealtimeDatabase<T> _database;
        private readonly string uid;

        public FirebaseDataStore(IFirebaseAuthService authService, string path)
        {
            var token = authService.AuthLink.FirebaseToken;

            this.uid = authService.AuthLink.User.LocalId;

            _query = new FirebaseClient(Config.ApiKeys.FirebaseUrl, () => Task.FromResult(token))
                .Child(path);
        }

        public async Task<bool> AddItem(T item)
        {
            try
            {
                item.Id = Guid.NewGuid().ToString();
                item.OwnerId = uid;

                await _query
                    .Child(item.Id)
                    .PutAsync(item);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItem(T item)
        {
            try
            {
                await _query
                    .Child(item.Id)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        public async Task<List<T>> GetItemsAsync(bool forceRefresh = false)
        {
            var firebaseObjects = await _query
                    .OnceAsync<T>();

            return firebaseObjects
                .Select(x =>
                {
                    var item = x.Object;

                    item.Id = x.Key;
                    item.OwnerId = uid;

                    return item;
                }).ToList();
        }
    }
}
