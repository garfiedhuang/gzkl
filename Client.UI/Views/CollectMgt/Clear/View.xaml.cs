using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.CollectMgt.Clear
{
    /// <summary>
    /// ViewCurve.xaml 的交互逻辑
    /// </summary>
    public partial class View : UserControl
    {
        public View(ClearModel clearModel)
        {
            InitializeComponent();

            clearModel.Conditions = ConvertJsonString(clearModel.Conditions);
            clearModel.Contents = ConvertJsonString(clearModel.Contents);

            this.DataContext = new { Model = clearModel };
        }

        private string ConvertJsonString(string str)
        {
            try
            {
                //格式化json字符串
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }

                return str;
            }
            catch
            {

            }

            return str;
        }
    }
}
