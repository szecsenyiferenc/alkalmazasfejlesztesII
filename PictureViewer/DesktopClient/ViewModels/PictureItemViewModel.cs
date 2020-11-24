using Core.Models.Output;
using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopClient.ViewModels
{
    class PictureItemViewModel
    {
        public PictureOutputDto Picture { get; set; }

        public RelayCommand<PictureOutputDto> PictureClick { get; set; }

        public PictureItemViewModel()
        {
            PictureClick = new RelayCommand<PictureOutputDto>(SelectPicture);
        }

        private void SelectPicture(PictureOutputDto sender)
        {
            SelectedPictureService.Instance.SelectedPicture = sender;
            EventProxy.Instance.RaiseViewChange(PictureViewerState.SinglePicture);
        }

    }
}
