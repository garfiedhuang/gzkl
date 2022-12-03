using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.CollectMgt.Clear;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using Newtonsoft.Json;

namespace GZKL.Client.UI.ViewsModels
{
    public class ClearViewModel : BaseSearchViewModel<ClearModel>
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClearViewModel()
        {
            this.Query();
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        private DateTime startClearTime = DateTime.Now.AddYears(-1);
        public DateTime StartClearTime { get { return startClearTime; } set { startClearTime = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime endClearTime = DateTime.Now;
        public DateTime EndClearTime { get { return endClearTime; } set { endClearTime = value; RaisePropertyChanged(); } }

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
                var sql = new StringBuilder(@"SELECT row_number()over(order by m.update_dt desc )as row_num,m.* FROM [dbo].[sys_db_clear] m WHERE m.is_deleted=0");

                SqlParameter[] parameters = null;


                sql.Append($" AND m.create_dt BETWEEN @startClearTime AND @endClearTime");
                var parameters1 = new SqlParameter()
                {
                    ParameterName = "@startClearTime",
                    DbType = DbType.DateTime,
                    Value = StartClearTime
                };
                var parameters2 = new SqlParameter()
                {
                    ParameterName = "@endClearTime",
                    DbType = DbType.DateTime,
                    Value = EndClearTime.AddDays(1)
                };
                parameters = new SqlParameter[] { parameters1, parameters2 };

                TModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TModels.Add(new ClearModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                ClearNo = dataRow["clear_no"].ToString(),
                                ClearTime = GetClearTime(dataRow["clear_time"].ToString()),
                                Conditions = dataRow["conditions"].ToString(),
                                Contents = dataRow["contents"].ToString(),
                                Status = dataRow["status"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                DateTime? GetClearTime(string time)
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
            this.StartClearTime = DateTime.Now.AddYears(-1);
            this.EndClearTime = DateTime.Now;

            this.Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        public override void Add()
        {
            try
            {
                ClearModel model = new ClearModel();
                ClearQueryModel queryModel = new ClearQueryModel();
                Edit view = new Edit(model, queryModel);
                var r = view.ShowDialog();

                if (r.Value)
                {

                    model.ClearNo = $"CL-{DateTime.Now.ToString("yyyyMMddHHmmss")}".ToUpper();
                    model.Conditions = JsonConvert.SerializeObject(queryModel);
                    model.Status = "处理中";
                    model.Contents = string.Empty;

                    var sql = @"INSERT INTO [dbo].[sys_db_clear]
           ([clear_no]
           ,[conditions]
           ,[contents]
           ,[status]
           ,[remark]
           ,[clear_time]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id]) OUTPUT Inserted.[id]
     VALUES
           (@clear_no
           ,@conditions
           ,@contents
           ,@status
           ,@remark
           ,@create_dt
           ,1
           ,0
           ,@create_dt
           ,@user_id
           ,@create_dt
           ,@user_id)";

                    var parameters = new SqlParameter[] {
                    new SqlParameter("@clear_no", model.ClearNo),
                    new SqlParameter("@conditions", model.Conditions),
                    new SqlParameter("@contents", model.Contents),
                    new SqlParameter("@status", model.Status),
                    new SqlParameter("@remark", model.Remark),
                    new SqlParameter("@create_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id)
                };

                    var result = Convert.ToInt64(SQLHelper.ExecuteScalar(sql, parameters)); //result为新增后的主键ID

                    if (result > 0)//记录插入成功
                    {
                        Task.Run(() =>
                        {

                            //ToDo：执行数据库清理操作 by garfield 20221203


                        });
                    }

                    this.Query();
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
