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
using Firebase;
using Firebase.Auth;
using FireBaseApp.Droid.Auth;
using Xamarin.Forms;
using FireBaseApp.Models;
using System.Threading.Tasks;
using System.Diagnostics;

[assembly: Dependency(typeof(FirebaseAuthentication))]
namespace FireBaseApp.Droid.Auth
{
    public class FirebaseAuthentication : IAuthProvider
    {
        FirebaseApp firebaseApp;
        FirebaseAuth firebaseAuth;

        public event Action OnUserSignedIn;
        public event Action OnUserSignedOut;

        public FirebaseAuthentication()
        {
            if (firebaseApp == null)
            {
                firebaseApp = FirebaseApp.GetApps(Android.App.Application.Context)[0];
            }
            firebaseAuth = FirebaseAuth.GetInstance(firebaseApp);
            firebaseAuth.AuthState += AuthStateChanged;
        }

        public void AuthStateChanged(object sender, FirebaseAuth.AuthStateEventArgs e)
        {
            var user = e.Auth.CurrentUser;
            if (user != null)
            {
                OnUserSignedIn?.Invoke();
            }
            else
            {
                OnUserSignedOut?.Invoke();
            }
        }


        public async Task<string> GetUserTokenAsync()
        {
            var token = await firebaseAuth.CurrentUser.GetTokenAsync(false);
            return token.Token;
        }

        public bool IsUserSignedIn()
        {
            var user = firebaseAuth.CurrentUser;
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LoginUser(string email, string password)
        {
            try
            {
                await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async Task SignUpUser(string email, string password)
        {
            try
            {
                await firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void ChangePassword(string newPassword)
        {
            FirebaseUser user = firebaseAuth.CurrentUser;
            user.UpdatePassword(newPassword);
        }

        public void LogoutUser()
        {
            firebaseAuth.SignOut();
            if(firebaseAuth.CurrentUser == null)
            {
               
            }
        }

        public void ResetPassword(string email)
        {
            firebaseAuth.SendPasswordResetEmail(email);
        }
    }
}