using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseApp.Models
{
    public interface IAuthProvider
    {
        Task LoginUser(string email, string password);
        Task SignUpUser(string email, string password);
        void ChangePassword(string newPassword);
        void LogoutUser();
        void ResetPassword(string email);
        event Action OnUserSignedIn;
        event Action OnUserSignedOut;
        bool IsUserSignedIn();
        Task<string> GetUserTokenAsync();
    }
}
