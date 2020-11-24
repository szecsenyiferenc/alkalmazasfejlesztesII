using Core.Models.Output;
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
    class SinglePictureViewModel : ObservableObject, IDisposable
    {
        public PictureOutputDto Picture { get; set; }

        public RelayCommand BackClick { get; set; }
        public RelayCommand UpdateClick { get; set; }
        public RelayCommand DeleteClick { get; set; }

        public bool IsDeleteVisible { get => LoginService.Instance.CurrentUser?.Username == Picture?.Uploader; }


        public SinglePictureViewModel()
        {

            BackClick = new RelayCommand(Back);
            UpdateClick = new RelayCommand(Update);
            DeleteClick = new RelayCommand(Delete);

            EventProxy.Instance.ViewChanged += OnViewChange;
        }

        private void OnViewChange(object sender, EventArgs e)
        {
            Picture = SelectedPictureService.Instance.SelectedPicture;
            RaisePropertyChanged("Picture");
            RaisePropertyChanged("IsDeleteVisible");
        }

        private void Back()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Pictures);
        }

        private void Update()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Update);
        }

        private async void Delete()
        {
            try
            {
                await RestBackendService.Instance.DeletePicture(Picture);
                EventProxy.Instance.RaiseViewChange(PictureViewerState.Pictures);
            }
            catch
            {

            }

        }

        public void Dispose()
        {
            EventProxy.Instance.ViewChanged -= OnViewChange;
        }
    }
}
