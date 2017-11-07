using FireBaseApp.Models;
using FireBaseApp.Services;
using FireBaseApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FireBaseApp.Views
{
    public class LoginViewModel : BaseViewModel
    {
        IAuthProvider authProvider;
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public event Action OnUserSignedIn;
        public event Action OnUserSignedUp;

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetValue(ref email, value);
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetValue(ref password, value);
            }
        }

        public LoginViewModel(IAuthProvider _authProvider)
        {
            authProvider = _authProvider;
            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(async () => await Register());
        }

        private async Task Login()
        {
            await authProvider.LoginUser(Email, Password);
            if (authProvider.IsUserSignedIn())
            {
                OnUserSignedIn?.Invoke();
            }
        }

        private async Task Register()
        {
            await authProvider.SignUpUser(Email, Password);
        }

    }
}
