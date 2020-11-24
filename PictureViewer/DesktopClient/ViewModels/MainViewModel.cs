using DesktopClient.Enums;
using DesktopClient.Proxies;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    class MainViewModel : ObservableObject, IDisposable
    {
        private PictureViewerState _state;

        public PictureViewerState State { 
            get => _state; 
            set
            {
                if(value != _state)
                {
                    _state = value;
                    RaisePropertyChanged();
                }
            } 
        }

        public NavBarViewModel NavBarViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public PicturesViewModel PicturesViewModel { get; set; }
        public SinglePictureViewModel SinglePictureViewModel { get; set; }
        public UploadViewModel UploadViewModel { get; set; }
        public UpdateViewModel UpdateViewModel { get; set; }

        public bool IsLoginVisible { get => State == PictureViewerState.Login; }
        public bool IsRegisterVisible { get => State == PictureViewerState.Register; }
        public bool IsPicturesVisible { get => State == PictureViewerState.Pictures; }
        public bool IsSinglePictureVisible { get => State == PictureViewerState.SinglePicture; }
        public bool IsUploadVisible { get => State == PictureViewerState.Upload; }
        public bool IsUpdateVisible { get => State == PictureViewerState.Update; }
        public bool IsNavBarVisible { get => State == PictureViewerState.SinglePicture || State == PictureViewerState.Pictures; }

        public MainViewModel()
        {
            NavBarViewModel = new NavBarViewModel();
            LoginViewModel = new LoginViewModel();
            RegisterViewModel = new RegisterViewModel();
            PicturesViewModel = new PicturesViewModel();
            SinglePictureViewModel = new SinglePictureViewModel();
            UploadViewModel = new UploadViewModel();
            UpdateViewModel = new UpdateViewModel();
            State = PictureViewerState.Login;

            EventProxy.Instance.ViewChanged += OnViewChange;
        }

        private void OnViewChange(object sender, EventArgs e)
        {
            var viewState = (PictureViewerState)sender;
            State = viewState;
            RaisePropertyChanged("IsLoginVisible");
            RaisePropertyChanged("IsRegisterVisible");
            RaisePropertyChanged("IsPicturesVisible");
            RaisePropertyChanged("IsSinglePictureVisible");
            RaisePropertyChanged("IsNavBarVisible");
            RaisePropertyChanged("IsUploadVisible");
            RaisePropertyChanged("IsUpdateVisible");
        }

        public void Dispose()
        {
            EventProxy.Instance.ViewChanged -= OnViewChange;
        }
    }
}
