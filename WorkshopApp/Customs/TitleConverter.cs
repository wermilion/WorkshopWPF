using System.Globalization;
using System;
using System.Windows.Data;

namespace WorkshopApp.Customs
{
    public class TitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string prefix = "Ремонтная мастерская";
            string pageTitle = value as string;
            if (!string.IsNullOrEmpty(pageTitle))
            {
                return $"{prefix} | {pageTitle}";
            }
            return prefix;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
