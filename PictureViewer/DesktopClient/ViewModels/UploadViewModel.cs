using Core.Models.Input;
using DesktopClient.Enums;
using DesktopClient.Proxies;
using DesktopClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient.ViewModels
{
    class UploadViewModel : ObservableObject
    {
        private string title;
        private string description;
        private string path;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged();
            }
        }

        public string Path
        {
            get => path;
            set
            {
                path = value;
                RaisePropertyChanged();
                RaisePropertyChanged("CanBeUploaded");
            }
        }

        public bool CanBeUploaded { get => Path != null; }

        public RelayCommand SelectClick { get; set; }
        public RelayCommand UploadClick { get; set; }
        public RelayCommand BackClick { get; set; }

        public UploadViewModel()
        {
            SelectClick = new RelayCommand(Select);
            UploadClick = new RelayCommand(Upload);
            BackClick = new RelayCommand(Back);
        }

        private void Select()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                Path = dlg.FileName;
            }
        }

        private async void Upload()
        {
            try
            {
                if (string.IsNullOrEmpty(Title))
                {
                    MessageBox.Show("No Title added", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(Path))
                {
                    MessageBox.Show("No file selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var bytes = await File.ReadAllBytesAsync(Path);

                await RestBackendService.Instance.UploadPicture(new PictureInputDto(Title, Description, bytes));

                EventProxy.Instance.RaiseViewChange(PictureViewerState.Pictures);
            }
            catch(Exception e)
            {

            }

        }

        private void Back()
        {
            EventProxy.Instance.RaiseViewChange(PictureViewerState.Login);
        }

    }
}
