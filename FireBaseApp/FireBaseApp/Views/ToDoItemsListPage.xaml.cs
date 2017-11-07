using FireBaseApp.Models;
using FireBaseApp.Services;
using FireBaseApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireBaseApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoItemsListPage : ContentPage
    {
        IPageService pageService;
        IAuthProvider authProvider;

        public ToDoItemsListPage(DataProvider dataProvider, IAuthProvider _authProvider, IPageService _pageService)
        {
            pageService = _pageService;
            authProvider = _authProvider;
            BindingContext = new ToDoItemsListViewModel(dataProvider, authProvider, _pageService);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await ((ToDoItemsListViewModel)BindingContext).CheckUserAuthorization();
            base.OnAppearing();
        }
    }
}