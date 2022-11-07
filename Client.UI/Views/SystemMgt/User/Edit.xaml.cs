using GalaSoft.MvvmLight.Messaging;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.SystemMgt.User
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        private readonly long _id;
        public Edit(UserModel userModel)
        {
            InitializeComponent();

            _id = userModel.Id;

            var sexData = new List<KeyValuePair<int, string>>();
            sexData.Add(new KeyValuePair<int, string>(0, "0-未知"));
            sexData.Add(new KeyValuePair<int, string>(1, "1-男"));
            sexData.Add(new KeyValuePair<int, string>(2, "2-女"));

            var isEnabledData = new List<KeyValuePair<int, string>>();
            isEnabledData.Add(new KeyValuePair<int, string>(0, "0-否"));
            isEnabledData.Add(new KeyValuePair<int, string>(1, "1-是"));

            this.DataContext = new { Model = userModel, SexData = sexData, IsEnabledData = isEnabledData };

            if (userModel.Id != 0)
            {
                this.txtName.IsReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.txtName.IsError = true;
                this.txtName.ErrorStr = "不能为空";
                return;
            }

            string sql = "";
            SqlParameter[] parameters = null;
            int rowCount = 0;

            if (_id == 0)
            {//新增
                sql = "SELECT COUNT(1) FROM [dbo].[sys_user] WHERE [name]=@name AND [is_deleted]=0";
                parameters = new SqlParameter[] { new SqlParameter("@name", this.txtName.Text) };
            }
            else
            { //修改
                sql = "SELECT COUNT(1) FROM [dbo].[sys_user] WHERE [name]=@name AND [is_deleted]=0 AND [id]<>@id";
                parameters = new SqlParameter[] { new SqlParameter("@name", this.txtName.Text), new SqlParameter("@id", _id) };
            }
            rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

            if (rowCount > 0)
            {
                MessageBox.Show($"数据库中已存在【{this.txtName.Text}】记录", "提示信息");
                return;
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
