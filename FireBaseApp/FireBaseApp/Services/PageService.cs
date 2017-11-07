using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireBaseApp.Services
{
    public class PageService : IPageService
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PopModalPageAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task PushModalPageAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }


        public async Task PushPageAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
