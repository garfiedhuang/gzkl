using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
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

namespace GZKL.Client.UI.Views.CollectMgt.AutoCollect
{
    /// <summary>
    /// Selector.xaml 的交互逻辑
    /// </summary>
    public partial class Selector : Window
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        private readonly long _id;

        public Selector(AutoCollectModel model)
        {
            InitializeComponent();

            //_id = SelectorModel.Id;

            //var isEnabledData = new List<KeyValuePair<int, string>>();
            //isEnabledData.Add(new KeyValuePair<int, string>(0, "0-否"));
            //isEnabledData.Add(new KeyValuePair<int, string>(1, "1-是"));

            //this.DataContext = new { Model = SelectorModel, IsEnabledData = isEnabledData };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(this.txtSelectorName.Text))
            //{
            //    this.txtSelectorName.IsError = true;
            //    this.txtSelectorName.ErrorStr = "不能为空";
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

            string sql = "";
            SqlParameter[] parameters = null;
            int rowCount = 0;

            //if (_id == 0)
            //{//新增
            //    sql = "SELECT COUNT(1) FROM [dbo].[base_Selector] WHERE ([Selector_name]=@Selector_name OR [access_db_path]=@accessDbPath OR [access_db_name]=@accessDbName) AND [is_deleted]=0";
            //    parameters = new SqlParameter[] { new SqlParameter("@Selector_name", txtSelectorName.Text), new SqlParameter("@accessDbPath", txtAccessDbPath.Text), new SqlParameter("@accessDbName", txtAccessDbName.Text) };
            //}
            //else
            //{ //修改
            //    sql = "SELECT COUNT(1) FROM [dbo].[base_Selector] WHERE ([Selector_name]=@Selector_name OR [access_db_path]=@accessDbPath OR [access_db_name]=@accessDbName) AND [is_deleted]=0 AND [id]<>@id";
            //    parameters = new SqlParameter[] { new SqlParameter("@Selector_name", txtSelectorName.Text), new SqlParameter("@accessDbPath", txtAccessDbPath.Text), new SqlParameter("@accessDbName", txtAccessDbName.Text), new SqlParameter("@id", _id) };
            //}
            //rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

            //if (rowCount > 0)
            //{
            //    MessageBox.Show($"数据库中已存在【{txtSelectorName.Text}|{txtAccessDbName.Text}】记录", "提示信息");
            //    return;
            //}

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
