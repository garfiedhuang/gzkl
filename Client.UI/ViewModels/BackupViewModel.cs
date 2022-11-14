using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GZKL.Client.UI.Models;
using System.Data.SqlClient;
using GZKL.Client.UI.Common;
using System.Data;
using MessageBox = HandyControl.Controls.MessageBox;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GZKL.Client.UI.ViewsModels
{
    public class BackupViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public BackupViewModel()
        {
            SelectCommand = new RelayCommand(this.Select);
            BackupCommand = new RelayCommand(this.Backup);

            _computerInfo = SessionInfo.Instance.ComputerInfo;
        }

        private ComputerInfo _computerInfo;

        /// <summary>
        /// 数据集合
        /// </summary>
        private BackupModel model;
        public BackupModel Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        private List<KeyValuePair<string, string>> serialPortData = new List<KeyValuePair<string, string>>();

        #endregion

        #region Command

        /// <summary>
        /// 选择
        /// </summary>
        public RelayCommand SelectCommand { get; set; }

        /// <summary>
        /// 备份
        /// </summary>
        public RelayCommand BackupCommand { get; set; }

        #endregion


        #region Command implement

        /// <summary>
        /// 选择
        /// </summary>
        public void Select()
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog("请选择一个文件夹");
                dialog.IsFolderPicker = true; //选择文件还是文件夹（true:选择文件夹，false:选择文件）
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Model.SavePath = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 备份
        /// </summary>
        public void Backup()
        {
            try
            {
                if (string.IsNullOrEmpty(Model.SavePath))
                {
                    throw new Exception("保存路径不能为空，请选择后再备份");
                }

                var fullName = string.Empty;
                var backupName = string.Empty;
                var userInfo = SessionInfo.Instance.Session;

                fullName = $"{_computerInfo.HostName}-{_computerInfo.CPU}";
                backupName = $"{fullName}_gzkldb_{DateTime.Now.ToString("yyyyMMddHHmmss")}.bak";

                var savePath = $"{Model.SavePath}/{backupName}";

                // 声明备份集名称变量
                //declare @name varchar(250)
                //--变量赋值，形如DBname_20210818．bak
                //set @name = 'D:\bak\bakup\DBname_' + convert(varchar(50), getdate(), 112) + '.bak'

                //--DBname为对应备份数据库的名称
                //BACKUP DATABASE DBname TO DISK = @name
                //WITH NOFORMAT, NOINIT, NAME = N'Full Backup of DBname', SKIP, NOREWIND, NOUNLOAD


                //执行备份
                var sql = new StringBuilder(@"BACKUP DATABASE gzkldb TO DISK = @savePath WITH NOFORMAT, NOINIT, NAME = N'Full Backup of gzkldb', SKIP, NOREWIND, NOUNLOAD");

                var sqlParameters = new SqlParameter[] {new SqlParameter("@savePath", savePath)};

                var result = SQLHelper.ExecuteNonQuery(sql.ToString(), sqlParameters);


                if (result > 0)
                {
                    //记录备份日志
                    sql.Clear();
                    sql.Append(@"INSERT INTO [dbo].[sys_db_backup]
                                       ([backup_no],[content],[remark],[is_enabled]
                                       ,[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
                                 VALUES
                                       (@backupNo,@content,@remark,1
                                       ,0,GETDATE(),@userId,GETDATE(),@userId)");

                    var backupNo = $"BK-{fullName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}".ToUpper();

                    sqlParameters = new SqlParameter[] {
                    new SqlParameter("@backupNo", backupNo),
                    new SqlParameter("@content", savePath),
                    new SqlParameter("@remark", "数据库备份"),
                    new SqlParameter("@userId", userInfo.Id),
                    };

                    result = SQLHelper.ExecuteNonQuery(sql.ToString(), sqlParameters);
                    MessageBox.Show($"数据库备份成功，{Environment.NewLine}备份码：{backupNo}{Environment.NewLine}备份地址：{savePath}", "提示信息");

                }
                else
                {
                    throw new Exception("当前无可用的备份数据，备份失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion

        #region Privates



        #endregion
    }
}
