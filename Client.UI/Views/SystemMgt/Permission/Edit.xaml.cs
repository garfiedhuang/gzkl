using GalaSoft.MvvmLight.Messaging;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.SystemMgt.Permission
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

        public Edit(PermissionModel permissionModel)
        {
            InitializeComponent();

            _id = permissionModel.Id;

            var sexData = new List<KeyValuePair<int, string>>();
            sexData.Add(new KeyValuePair<int, string>(0, "0-未知"));
            sexData.Add(new KeyValuePair<int, string>(1, "1-男"));
            sexData.Add(new KeyValuePair<int, string>(2, "2-女"));

            var isEnabledData = new List<KeyValuePair<int, string>>();
            isEnabledData.Add(new KeyValuePair<int, string>(0, "0-否"));
            isEnabledData.Add(new KeyValuePair<int, string>(1, "1-是"));

            this.DataContext = new { Model = permissionModel, UserData = GetUserData(), RoleData = GetRoleData() };

            if (permissionModel.Id != 0)
            {
                this.cmbUserId.IsReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbUserId.Text))
            {
                this.cmbUserId.IsError = true;
                this.cmbUserId.ErrorStr = "不能为空";
                return;
            }

            if (string.IsNullOrEmpty(this.cmbRoleId.Text))
            {
                this.cmbRoleId.IsError = true;
                this.cmbRoleId.ErrorStr = "不能为空";
                return;
            }

            string sql = "";
            SqlParameter[] parameters = null;
            int rowCount = 0;

            if (_id == 0)
            {//新增
                sql = "SELECT COUNT(1) FROM [dbo].[sys_user_role] WHERE [user_id]=@userId AND [role_id]=@roleId AND [is_deleted]=0";
                parameters = new SqlParameter[] {
                    new SqlParameter("@userId", cmbUserId.SelectedValue),
                    new SqlParameter("@roleId", cmbRoleId.SelectedValue)
                    };
            }
            else
            { //修改
                sql = "SELECT COUNT(1) FROM [dbo].[sys_user_role] WHERE [user_id]=@userId AND [role_id]=@roleId AND [is_deleted]=0 AND [id]<>@id";
                parameters = new SqlParameter[] {
                    new SqlParameter("@userId", cmbUserId.SelectedValue),
                    new SqlParameter("@roleId", cmbRoleId.SelectedValue),
                    new SqlParameter("@id", _id)
                    };
            }
            rowCount = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, parameters) ?? "0");

            if (rowCount > 0)
            {
                MessageBox.Show($"数据库中已存在【{cmbUserId.Text}|{cmbRoleId.Text}】记录", "提示信息");
                return;
            }

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private List<KeyValuePair<int, string>> GetUserData()
        {
            var result = new List<KeyValuePair<int, string>>();
            var sql = @"SELECT[id],[name] FROM[dbo].[sys_user] WHERE[is_deleted] = 0 ORDER BY[name]";

            using (var data = SQLHelper.GetDataTable(sql))
            {
                if (data == null || data.Rows.Count == 0)
                {
                    return result;
                }
                foreach (DataRow dr in data.Rows)
                {
                    result.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["id"]??"0"), dr["name"].ToString()));
                }
            }

            return result;

        }

        private List<KeyValuePair<int, string>> GetRoleData()
        {
            var result = new List<KeyValuePair<int, string>>();
            var sql = @"SELECT [id],[name] FROM [dbo].[sys_role] WHERE [is_deleted]=0 ORDER BY [name]";

            using (var data = SQLHelper.GetDataTable(sql))
            {
                if (data == null || data.Rows.Count == 0)
                {
                    return result;
                }
                foreach (DataRow dr in data.Rows)
                {
                    result.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["id"] ?? "0"), dr["name"].ToString()));
                }
            }

            return result;
        }
    }
}
