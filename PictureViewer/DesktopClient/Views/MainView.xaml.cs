using DesktopClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient.Views
{

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private MainViewModel _mainViewModel = new MainViewModel();

        public MainView()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
            navBarView.DataContext = _mainViewModel.NavBarViewModel;
            loginView.DataContext = _mainViewModel.LoginViewModel;
            registerView.DataContext = _mainViewModel.RegisterViewModel;
            picturesView.DataContext = _mainViewModel.PicturesViewModel;
            singlePictureView.DataContext = _mainViewModel.SinglePictureViewModel;
            uploadView.DataContext = _mainViewModel.UploadViewModel;
            updateView.DataContext = _mainViewModel.UpdateViewModel;
        }

    }
}
