using GZKL.Client.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GZKL.Client.UI.Converters
{
    public class EnumConverter : IValueConverter
    {
        private string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            var descriptionAttr = fieldInfo
                .GetCustomAttributes(false)
                .OfType<DescriptionAttribute>()
                .Cast<DescriptionAttribute>()
                .SingleOrDefault();
            if (descriptionAttr == null)
            {
                return enumObj.ToString();
            }
            else
            {
                return descriptionAttr.Description;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum myEnum = null;

            switch (parameter?.ToString() ?? "")
            {
                case "sex":
                    myEnum = (SexEnum)value; break;
                case "isEnabled":
                    myEnum = (BoolEnum)value; break;
                case "menuType":
                    myEnum = (MenuType)value; break;
                default:
                    break;
            }

            if (myEnum != null)
            {
                return GetEnumDescription(myEnum);
            }
            else
            {
                return value;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
