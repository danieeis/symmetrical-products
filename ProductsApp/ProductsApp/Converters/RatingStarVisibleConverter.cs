using System;
using System.Globalization;
using ProductsApp.Models;
using Xamarin.Forms;

namespace ProductsApp.Converters
{
    public class RatingStarVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Rating rating)
            {
                return rating.Rate >= 4.0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

