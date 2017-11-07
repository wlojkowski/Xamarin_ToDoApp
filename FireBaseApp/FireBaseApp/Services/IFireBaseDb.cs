using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseApp.Models
{
    public interface IFireBaseDb
    {
        void SetToken(string token);
        void ClearToken();
        Task CreateToDoItem(ToDoItem toDoItem);
        Task<List<ToDoItem>> GetToDoItemsCollection();
    }
}
