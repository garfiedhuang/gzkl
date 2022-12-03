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
using Newtonsoft.Json;
using GZKL.Client.UI.Views.CollectMgt.Backup;
using System.Threading.Tasks;

namespace GZKL.Client.UI.ViewsModels
{
    public class BackupViewModel :BaseSearchViewModel<BackupModel>
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public BackupViewModel()
        {
            this.Query();
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private DateTime startTime = DateTime.Now.AddMonths(-6);
        public DateTime StartTime { get { return startTime; } set { startTime = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime endTime = DateTime.Now;
        public DateTime EndTime { get { return endTime; } set { endTime = value; RaisePropertyChanged(); } }

        #endregion

        #region Command


        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public override void Query()
        {
            try
            {
                var sql = new StringBuilder(@"SELECT row_number()over(order by m.update_dt desc )as row_num,m.* FROM [dbo].[sys_db_backup] m WHERE m.is_deleted=0");

                SqlParameter[] parameters = null;


                sql.Append($" AND m.create_dt BETWEEN @startTime AND @endTime");
                var parameters1 = new SqlParameter()
                {
                    ParameterName = "@startTime",
                    DbType = DbType.DateTime,
                    Value = StartTime
                };
                var parameters2 = new SqlParameter()
                {
                    ParameterName = "@endTime",
                    DbType = DbType.DateTime,
                    Value = EndTime.AddDays(1)
                };
                parameters = new SqlParameter[] { parameters1, parameters2 };

                TModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TModels.Add(new BackupModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                BackupNo = dataRow["backup_no"].ToString(),
                                BackupTime = GetTime(dataRow["backup_time"].ToString()),
                                SavePath = dataRow["save_path"].ToString(),
                                FileName = dataRow["file_name"].ToString(),
                                Status = dataRow["status"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                DateTime? GetTime(string time)
                {
                    if (!string.IsNullOrEmpty(time))
                    {
                        return Convert.ToDateTime(time);
                    }
                    else
                    {
                        return null;
                    }
                }

                //数据分页
                Paging(-1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            this.StartTime = DateTime.Now.AddMonths(-6);
            this.EndTime = DateTime.Now;

            this.Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        public override void Add()
        {
            try
            {
                BackupModel model = new BackupModel();
                Edit view = new Edit(model);
                var r = view.ShowDialog();

                if (r.Value)
                {

                    var computerInfo = SessionInfo.Instance.ComputerInfo;
                    var fileName = $"{computerInfo.HostName}_gzkldb_{DateTime.Now.ToString("yyyyMMddHHmmss")}.bak";
                    var savePath = $"{model.SavePath}\\{fileName}";

                    model.SavePath = savePath;
                    model.FileName = fileName;

                    model.BackupNo = $"BK-{computerInfo.HostName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}".ToUpper();
                    model.Status = "处理中";

                    var sql = @"INSERT INTO [dbo].[sys_db_backup]
           ([backup_no]
           ,[save_path]
           ,[file_name]
           ,[status]
           ,[remark]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id]) OUTPUT Inserted.[id]
     VALUES
           (@backup_no
           ,@save_path
           ,@file_name
           ,@status
           ,@remark
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                    new SqlParameter("@backup_no", model.BackupNo),
                    new SqlParameter("@save_path", model.SavePath),
                    new SqlParameter("@file_name", model.FileName),
                    new SqlParameter("@status", model.Status),
                    new SqlParameter("@remark", model.Remark),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id)
                };

                    var result = Convert.ToInt64(SQLHelper.ExecuteScalar(sql, parameters)); //result为新增后的主键ID

                    if (result > 0)//记录插入成功
                    {
                        Task.Run(async () =>
                        {
                            await Task.Delay(10000);
                            BackupDataBase(result, model);
                        });
                    }

                    this.Query();
                    HandyControl.Controls.Growl.Info("数据库备份任务创建成功！");
                }
            }
            catch (Exception ex)
            {
                HandyControl.Controls.Growl.Error(ex?.Message);
            }
        }

        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void BackupDataBase(long id,BackupModel model)
        {
            var message = "处理成功";
            var ds = new DataSet("myDs");
            var remark = "ok";

            try
            {
                var sql = new StringBuilder(@"BACKUP DATABASE gzkldb TO DISK = @savePath WITH NOFORMAT, NOINIT, NAME = N'Full Backup of gzkldb', SKIP, NOREWIND, NOUNLOAD");

                var sqlParameters = new SqlParameter[] { new SqlParameter("@savePath", model.SavePath) };

                var result = SQLHelper.ExecuteNonQuery(sql.ToString(), sqlParameters);

            }
            catch (Exception ex)
            {
                message = "处理失败";
                remark = ex?.Message;
                HandyControl.Controls.Growl.Warning(ex?.Message);
                LogHelper.Error(ex?.Message);
            }

            try
            {
                //更新执行状态
                var sql = @"UPDATE sys_db_backup SET backup_time=getdate(),[status]=@status,update_dt=getdate(),update_user_id=@userId,remark=@remark WHERE id=@id";
                var parameters = new SqlParameter[] {
                new SqlParameter("@contents", JsonConvert.SerializeObject(ds)),
                new SqlParameter("@status", message),
                new SqlParameter("@userId", SessionInfo.Instance.UserInfo.Id),
                new SqlParameter("@remark", remark),
                new SqlParameter("@id", id) };
                _ = SQLHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.Growl.Warning(ex?.Message);
                LogHelper.Error(ex?.Message);
            }
            finally
            {
                HandyControl.Controls.Growl.Info("数据库备份执行完毕！");
            }
        }

        #endregion

        #region Privates



        #endregion
    }
}
