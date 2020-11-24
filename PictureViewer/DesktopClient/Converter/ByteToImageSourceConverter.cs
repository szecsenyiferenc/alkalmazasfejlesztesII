using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopClient.Converter
{
    public sealed class ByteToImageSourceConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                byte[] rawImage = value as byte[];

                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(rawImage);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = biImg as ImageSource;

                return imgSrc;
            } catch
            {
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
