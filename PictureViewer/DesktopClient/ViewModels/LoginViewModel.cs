using Core.Models;
using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesktopClient.ViewModels
{
    class LoginViewModel : ObservableObject
    {
        private bool errorHappened;

        public string Username { get; set; }
        public string Password { get; set; }
        public bool ErrorHappened { get => errorHappened; set { errorHappened = value; RaisePropertyChanged(); } }

        public RelayCommand SignInClick { get; set; }
        public RelayCommand RegisterClick { get; set; }

        public LoginViewModel()
        {
            SignInClick = new RelayCommand(SignIn);
            RegisterClick = new RelayCommand(Register);
            ErrorHappened = false;
        }

        private async void SignIn()
        {
            try
            {
                var loginResult = await LoginService.Instance.CheckLogin(new LoginData(Username, Password));
                if (loginResult)
                {
                    ErrorHappened = false;
                    Username = null;
                    Password = null;
                    EventProxy.Instance.RaiseViewChange(PictureViewerState.Pictures);
                }
            }
            catch
            {
                ErrorHappened = true;
            }

        }

        private void Register()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Register);
        }
    }
}
