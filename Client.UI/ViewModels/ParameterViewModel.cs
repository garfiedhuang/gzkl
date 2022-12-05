using GalaSoft.MvvmLight.Command;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.CollectMgt.Parameter;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GZKL.Client.UI.ViewsModels
{
    public class ParameterViewModel : BaseSearchViewModel<ParameterModel>
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterViewModel()
        {
            //每页最大记录数
            base.DataCountPerPage = 50;

            BackupCommand = new RelayCommand(this.Backup);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// 备份
        /// </summary>
        public RelayCommand BackupCommand { get; set; }

        #endregion

        #region Command implement

        /// <summary>
        /// 查询
        /// </summary>
        public override void Query()
        {
            try
            {
                var computerInfo = SessionInfo.Instance.ComputerInfo;
                var commonParams = $"CommonParams-{computerInfo.HostName}-{computerInfo.CPU}";
                var channelParams = $"ChannelParams-{computerInfo.HostName}-{computerInfo.CPU}-%";

                var sql = new StringBuilder($@"SELECT a.* FROM [dbo].[sys_config] a WHERE a.[is_deleted]=0 AND (a.category='{commonParams}' OR a.category LIKE '{channelParams}')");

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(Search.Trim()))
                {
                    sql.Append($" AND (a.[value] LIKE @search or a.[text] LIKE @search)");
                    parameters = new SqlParameter[1] { new SqlParameter("@search", $"%{Search}%") };
                }

                sql.Append($" ORDER BY a.[category] ASC,a.[value] ASC");

                TModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        var tempData = new List<ParameterModel>();
                        foreach (DataRow dataRow in data.Rows)
                        {
                            tempData.Add(new ParameterModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                Category = dataRow["category"].ToString(),
                                Value = dataRow["value"].ToString(),
                                Text = dataRow["text"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }

                        var currentChannel = tempData.FirstOrDefault(w => w.Value == "通道号").Text;

                        //通用参数
                        TModels.AddRange(tempData.Where(w=>w.Category==commonParams));

                        //通道参数
                        TModels.AddRange(tempData.Where(w => w.Category == channelParams.Replace("%",currentChannel)));

                        var rowNum = 1;
                        TModels.ForEach(item => {
                            item.RowNum = rowNum;
                            ++rowNum;
                        });
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
            this.Search = string.Empty;
            this.Query();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public void Edit(long id)
        {
            try
            {
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new ParameterModel()
                    {
                        Id = Convert.ToInt64(dataRow["id"]),
                        Category = dataRow["category"].ToString(),
                        Value = dataRow["value"].ToString(),
                        Text = dataRow["text"].ToString(),
                        Remark = dataRow["remark"].ToString(),
                        IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                        CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                        UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                    };

                    if (model != null)
                    {
                        Edit view = new Edit(model);
                        var r = view.ShowDialog();
                        if (r.Value)
                        {
                            sql.Clear();
                            sql.Append(@"UPDATE [dbo].[sys_config]
   SET [category] = @category
      ,[value] = @value
      ,[text] = @text
      ,[remark] = @remark
      ,[is_enabled] = @is_enabled
      ,[update_dt] = @update_dt
      ,[update_user_id] = @user_id
 WHERE [id]=@id");
                            parameters = new SqlParameter[] {
                            new SqlParameter("@category", model.Category),
                            new SqlParameter("@value", model.Value),
                            new SqlParameter("@text", model.Text),
                            new SqlParameter("@remark", model.Remark),
                            new SqlParameter("@is_enabled", model.IsEnabled),
                            new SqlParameter("@update_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                            new SqlParameter("@user_id", SessionInfo.Instance.UserInfo.Id),
                            new SqlParameter("@id", id)
                        };

                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

                            this.Query();
                        }
                    }
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
                var _computerInfo = SessionInfo.Instance.ComputerInfo;
                var fullName = $"{_computerInfo.HostName}-{_computerInfo.CPU}";
                var userInfo = SessionInfo.Instance.UserInfo;

                //查询数据库并赋值
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE ([category] =@category1 OR [category] LIKE @category2) AND [is_deleted]=0");

                var category1 = $"CommonParams-{fullName}";//CommonParams-{HostName}-{CPU}
                var category2 = $"ChannelParams-{fullName}-";//ChannelParams-{HostName}-{CPU}-{No}

                var parameters = new SqlParameter[] {
                    new SqlParameter("@category1", category1),
                    new SqlParameter("@category2", $"{category2}%")
                };

                var paramsConfigs = new List<ConfigModel>();

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            paramsConfigs.Add(new ConfigModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                Category = dataRow["category"].ToString(),
                                Value = dataRow["value"].ToString(),
                                Text = dataRow["text"].ToString(),
                                Remark = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
                    }
                }

                if (paramsConfigs.Count > 0)
                {
                    sql.Clear();
                    sql.Append(@"INSERT INTO [dbo].[sys_params_backup]
                                       ([backup_no],[json_content],[remark],[is_enabled]
                                       ,[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id])
                                 VALUES
                                       (@backupNo,@contents,@remark,1
                                       ,0,GETDATE(),@userId,GETDATE(),@userId)");

                    var backupNo = $"BK-{fullName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}".ToUpper();
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(paramsConfigs);

                    parameters = new SqlParameter[] {
                    new SqlParameter("@backupNo", backupNo),
                    new SqlParameter("@contents", jsonContent),
                    new SqlParameter("@remark", "采集参数备份"),
                    new SqlParameter("@userId", userInfo.Id),
                    };

                    var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                    MessageBox.Show($"参数备份成功，备份码：{backupNo}", "提示信息");

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
