using ImageServiceApp.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using ImageServiceApp;


namespace ImageServiceApp.ViewModel
{
    class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType.Name != "Brush")
            {
                throw new InvalidOperationException("Type must be of message enum type");
            }
            MessageTypeEnum type = (MessageTypeEnum)value;
            switch (type)
            {
                case MessageTypeEnum.FAIL:
                    return "Red";
                case MessageTypeEnum.WARNING:
                    return "Yellow";
                case MessageTypeEnum.INFO:
                    return "YellowGreen";
                default:
                    return "Transparent";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
