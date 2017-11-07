using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Database;
using FireBaseApp.Models;
using FireBaseApp.Droid.Persistence;
using Xamarin.Forms;
using System.Threading.Tasks;
using Firebase.Xamarin.Database.Query;
using FireBaseApp.Config;

[assembly: Dependency(typeof(FireBaseDb))]
namespace FireBaseApp.Droid.Persistence
{
    public class FireBaseDb : IFireBaseDb
    {
        private readonly string databaseUrl = Keys.FireBaseDbUrl;
        private string token;

        FirebaseClient firebaseClient;
        
        public FireBaseDb()
        {
            firebaseClient = new FirebaseClient(databaseUrl);
        }

        public void SetToken(string _token)
        {
            token = _token;
        }

        public void ClearToken()
        {
            token = "";
        }

        private bool IsTokenSet()
        {
            return !String.IsNullOrEmpty(token);
        }

        public async Task CreateToDoItem(ToDoItem toDoItem)
        {
            if (!IsTokenSet())
            {
                System.Diagnostics.Debug.WriteLine("ERROR! Token not set!");
                return;
            }

            await firebaseClient.Child("ToDoItems").WithAuth(token).PostAsync<ToDoItem>(toDoItem);
        }

        public async Task<List<ToDoItem>> GetToDoItemsCollection()
        {
            if (!IsTokenSet())
            {
                System.Diagnostics.Debug.WriteLine("ERROR! Token not set!");
                return null;
            }

            var items = await firebaseClient.Child("ToDoItems").WithAuth(token).OnceAsync<ToDoItem>();
            List<ToDoItem> toDoItemCollection = new List<ToDoItem>();
            foreach (var item in items)
            {
                ToDoItem receivedToDoItem = new ToDoItem();
                receivedToDoItem.Id = item.Object.Id;
                receivedToDoItem.Title = item.Object.Title;
                receivedToDoItem.Description = item.Object.Description;
                toDoItemCollection.Add(receivedToDoItem);
            }
            return toDoItemCollection;
        }
    }
}