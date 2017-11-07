using FireBaseApp.Models;
using FireBaseApp.Services;
using FireBaseApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FireBaseApp.ViewModels
{
    public class ToDoItemsListViewModel : BaseViewModel
    {
        private ObservableCollection<ToDoItem> toDoItemsCollection;

        public ObservableCollection<ToDoItem> ToDoItemsObservableCollection
        {
            get
            {
                return toDoItemsCollection;
            }
            set
            {
                SetValue(ref toDoItemsCollection, value);
            }
        }

        private DataProvider dataProvider;
        private IAuthProvider authProvider;
        private IPageService pageService;

        public ICommand LoadDataCommand { get; private set; }
        public ICommand DeleteToDoItemCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public ToDoItemsListViewModel(DataProvider _dataProvider, IAuthProvider _authProvider, IPageService _pageService)
        {
            dataProvider = _dataProvider;
            authProvider = _authProvider;
            pageService = _pageService;
            LoadDataCommand = new Command(async () => await LoadData());
            LogoutCommand = new Command(() => LogoutUser());
        }

        private void LogoutUser()
        {
            authProvider.LogoutUser();
            dataProvider.ClearToken();
            CheckUserAuthorization();
        }

        public async Task CheckUserAuthorization()
        {
            if (!authProvider.IsUserSignedIn())
            {
                LoginViewModel loginViewModel = new LoginViewModel(authProvider);
                loginViewModel.OnUserSignedIn += async () => await pageService.PopModalPageAsync();
                await pageService.PushModalPageAsync(new LoginPage(loginViewModel));
            }
            else
            {
                var _token = await authProvider.GetUserTokenAsync();
                dataProvider.SetToken(_token);
                LoadDataCommand.Execute(null);
            }

        }

        private async Task LoadData()
        {
            await dataProvider.LoadDataFromDatabase();
            ToDoItemsObservableCollection = new ObservableCollection<ToDoItem>(dataProvider.GetToDoItemCollection());
        }
       
        
        
        
        
        /*
                private async Task DeleteToDoItem()
                {
                    var a = "";
                    if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No"))
                    {
                        Contacts.Remove(contactViewModel);

                        var contact = await _contactStore.GetContact(contactViewModel.Id);
                        await _contactStore.DeleteContact(contact);
                    }
                }
                */

    }
}
