using Core.Models.Input;
using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DesktopClient.ViewModels
{
    class RegisterViewModel : ObservableObject
    {
        private bool errorHappened;

        public string Username { get; set; }
        public string Password { get; set; }

        public RelayCommand RegisterClick { get; set; }
        public RelayCommand BackClick { get; set; }

        public bool ErrorHappened { get => errorHappened; set { errorHappened = value; RaisePropertyChanged(); } }


        public RegisterViewModel()
        {
            RegisterClick = new RelayCommand(Register);
            BackClick = new RelayCommand(Back);
        }

        private async void Register()
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    MessageBox.Show("No Username added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("No Password added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await RestBackendService.Instance.Register(new CustomerInputDto(Username, Password));
                EventProxy.Instance.RaiseViewChange(PictureViewerState.Login);
            }
            catch
            {
                ErrorHappened = true;
                MessageBox.Show("Wrong username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Back()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Login);
        }
    }
}
