using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace GZKL.Client.UI.Views.SystemMgt.Menu
{
    /// <summary>
    /// Edit.xaml 的交互逻辑
    /// </summary>
    public partial class Edit : Window
    {

        public Edit(MenuModel MenuModel)
        {
            InitializeComponent();

            var isEnabledData = new List<KeyValuePair<int, string>>();
            isEnabledData.Add(new KeyValuePair<int, string>(0, "0.否"));
            isEnabledData.Add(new KeyValuePair<int, string>(1, "1.是"));

            var menuTypeData = new List<KeyValuePair<int, string>>();
            menuTypeData.Add(new KeyValuePair<int, string>(1, "1.根菜单"));
            menuTypeData.Add(new KeyValuePair<int, string>(2, "2.一级菜单"));
            menuTypeData.Add(new KeyValuePair<int, string>(3, "3.二级菜单"));
            menuTypeData.Add(new KeyValuePair<int, string>(4, "4.三级菜单"));

            this.DataContext = new { Model = MenuModel, IsEnabledData = isEnabledData,MenuTypeData=menuTypeData };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtMenuName.Text))
            //{
            //    this.txtMenuName.IsError = true;
            //    this.txtMenuName.ErrorStr = "不能为空";
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.txtAccessDbPath.Text))
            //{
            //    this.txtAccessDbPath.IsError = true;
            //    this.txtAccessDbPath.ErrorStr = "不能为空";
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.txtAccessDbName.Text))
            //{
            //    this.txtAccessDbName.IsError = true;
            //    this.txtAccessDbName.ErrorStr = "不能为空";
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.cmbIsEnabled.Text))
            //{
            //    this.cmbIsEnabled.IsError = true;
            //    this.cmbIsEnabled.ErrorStr = "不能为空";
            //    return;
            //}

            //string sql = "";
            //SqlParameter[] parameters = null;
            //int rowCount = 0;

            //if (_id == 0)
            //{//新增
            //    sql = "SELECT COUNT(1) FROM [dbo].[base_Menu] WHERE ([Menu_name]=@Menu_name OR [access_db_path]=@accessDbPath OR [access_db_name]=@accessDbName) AND [is_deleted]=0";
            //    parameters = new SqlParameter[] { new SqlParameter("@Menu_name", txtMenuName.Text), new SqlParameter("@accessDbPath", txtAccessDbPath.Text), new SqlParameter("@accessDbName", txtAccessDbName.Text) };
            //}
            //else
            //{ //修改
            //    sql = "SELECT COUNT(1) FROM [dbo].[base_Menu] WHERE ([Menu_name]=@Menu_name OR [access_db_path]=@accessDbPath OR [access_db_name]=@accessDbName) AND [is_deleted]=0 AND [id]<>@id";
            //    parameters = new SqlParameter[] { new SqlParameter("@Menu_name", txtMenuName.Text), new SqlParameter("@accessDbPath", txtAccessDbPath.Text), new SqlParameter("@accessDbName", txtAccessDbName.Text), new SqlParameter("@id", _id) };
            //}
            //rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

            //if (rowCount > 0)
            //{
            //    MessageBox.Show($"数据库中已存在【{txtMenuName.Text}|{txtAccessDbName.Text}】记录", "提示信息");
            //    return;
            //}

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
