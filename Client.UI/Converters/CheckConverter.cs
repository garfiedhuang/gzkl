using GZKL.Client.UI.Views.CollectMgt.Parameter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GZKL.Client.UI.Converters
{
    public class CheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }
            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            bool r = checkValue.Equals(targetValue);
            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }
            if ((bool)value)
            {
                return parameter.ToString();
            }
            return null;
        }
    }
}
