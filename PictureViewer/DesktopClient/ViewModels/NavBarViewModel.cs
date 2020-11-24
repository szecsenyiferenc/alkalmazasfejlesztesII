using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopClient.ViewModels
{
    class NavBarViewModel : ObservableObject, IDisposable
    {
        public string Username { get => LoginService.Instance.CurrentUser?.Username; }

        public RelayCommand SignOutClick { get; set; }
        public RelayCommand UploadClick { get; set; }
        public RelayCommand DeleteClick { get; set; }

        public NavBarViewModel()
        {
            SignOutClick = new RelayCommand(SignOut);
            UploadClick = new RelayCommand(Upload);
            DeleteClick = new RelayCommand(Delete);
            EventProxy.Instance.ViewChanged += UpdateView;
        }

        private void SignOut()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Login);
        }

        private void Upload()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Upload);
        }

        private async void Delete()
        {
            try
            {
                await RestBackendService.Instance.DeleteCustomer();
                EventProxy.Instance.RaiseViewChange(PictureViewerState.Login);
            }
            catch
            {

            }

        }

        private void UpdateView(object sender, EventArgs e)
        {
            RaisePropertyChanged("Username");
        }

        public void Dispose()
        {
            EventProxy.Instance.ViewChanged -= UpdateView;
        }
    }
}
