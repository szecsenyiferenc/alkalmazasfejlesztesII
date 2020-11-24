using DesktopClient.Proxies;
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
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void pwBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EventProxy.Instance.ViewChanged += RemovePassword;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            (DataContext as RegisterViewModel).Password = passwordBox.Password;
        }

        private void RemovePassword(object sender, EventArgs e)
        {
            pwBox.Password = null;
            tBox.Text = null;
        }

        public void Dispose()
        {
            EventProxy.Instance.ViewChanged -= RemovePassword;
        }
    }
}
