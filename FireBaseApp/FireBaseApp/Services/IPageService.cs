using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FireBaseApp.Services
{
    public interface IPageService
    {
        Task PushPageAsync(Page page);
        Task PushModalPageAsync(Page page);
        Task PopModalPageAsync();
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
    }
}
