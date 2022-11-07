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

namespace GZKL.Client.UI.Views.SystemMgt.Role
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

        public Edit(RoleModel roleModel)
        {
            InitializeComponent();

            _id = roleModel.Id;

            this.DataContext = new { Model = roleModel };

            if (roleModel.Id != 0)
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
                sql = "SELECT COUNT(1) FROM [dbo].[sys_role] WHERE [name]=@name AND [is_deleted]=0";
                parameters = new SqlParameter[] { new SqlParameter("@name", this.txtName.Text) };
            }
            else
            { //修改
                sql = "SELECT COUNT(1) FROM [dbo].[sys_role] WHERE [name]=@name AND [is_deleted]=0 AND [id]<>@id";
                parameters = new SqlParameter[] { new SqlParameter("@name", this.txtName.Text), new SqlParameter("@id", _id) };
            }
            rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql.ToString(), parameters) ?? "0");

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
