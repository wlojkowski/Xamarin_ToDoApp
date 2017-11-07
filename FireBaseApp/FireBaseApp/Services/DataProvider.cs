using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseApp.Models
{
    public class DataProvider
    {
        IFireBaseDb fireBaseDb;

        List<ToDoItem> ToDoItemCollection;

        public DataProvider(IFireBaseDb _fireBaseDb)
        {
            fireBaseDb = _fireBaseDb;
        }

        public void ClearToken()
        {
            fireBaseDb.ClearToken();
        }

        public void SetToken(string _token)
        {
            fireBaseDb.SetToken(_token);
        }


        public void CreateToDoItem(ToDoItem item)
        {
            fireBaseDb.CreateToDoItem(item);
        }

        public async Task LoadDataFromDatabase()
        {
            if(ToDoItemCollection == null)
                ToDoItemCollection = new List<ToDoItem>(await fireBaseDb.GetToDoItemsCollection());
        }

        public List<ToDoItem> GetToDoItemCollection()
        {
            return ToDoItemCollection;
        }
    }
}
