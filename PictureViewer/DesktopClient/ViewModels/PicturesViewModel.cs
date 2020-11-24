using Core.Models.Output;
using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopClient.ViewModels
{
    class PicturesViewModel : ObservableObject, IDisposable
    {
        public List<PictureItemViewModel> Pictures { get; set; }


        public PicturesViewModel()
        {
            Pictures = new List<PictureItemViewModel>();
            EventProxy.Instance.ViewChanged += InitImages;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        private async void InitImages(object sender, EventArgs e)
        {
            var viewState = (PictureViewerState)sender;
            if(viewState == PictureViewerState.Pictures)
            {
                Pictures = (await RestBackendService.Instance.GetPictures()).Select(p => new PictureItemViewModel()
                {
                    Picture = p
                }).ToList();
                RaisePropertyChanged("Pictures");
            }
        }

        private ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
