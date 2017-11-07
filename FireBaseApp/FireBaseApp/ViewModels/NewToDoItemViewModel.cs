using FireBaseApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FireBaseApp.ViewModels
{
    public class NewToDoItemViewModel
    {
        public ToDoItem NewToDoItem { get; set; }
        int i = 0;
        public ICommand CreateToDoItemCommand { get; set; }
        DataProvider dataProvider;

        public NewToDoItemViewModel()
        {
            dataProvider = new DataProvider(DependencyService.Get<IFireBaseDb>());
            CreateToDoItemCommand = new Command(async () => await Save());
            NewToDoItem = new ToDoItem();

        }

        async Task Save()
        {
            dataProvider.CreateToDoItem(NewToDoItem);     
        }

    }
}
