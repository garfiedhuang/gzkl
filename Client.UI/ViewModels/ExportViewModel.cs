using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GZKL.Client.UI.Models;
using System.Windows;
using HandyControl.Data;
using System.Data.SqlClient;
using GZKL.Client.UI.Common;
using System.Data;
using System.Windows.Controls;
using MessageBox = HandyControl.Controls.MessageBox;
using GZKL.Client.UI.Views.CollectMgt.Export;
using GalaSoft.MvvmLight.Messaging;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace GZKL.Client.UI.ViewsModels
{
    public class ExportViewModel : BaseSearchViewModel<ExportModel>
    {
        #region Construct and property

        /// <summary>
        /// 查询类型 TD-按检测日期查询，TN-按检测编号查询
        /// </summary>
        private string queryType="TD";
        public string QueryType { get { return queryType; } set { queryType = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 检测开始日期
        /// </summary>
        private string startTestDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        public string StartTestDate { get { return startTestDate; } set { startTestDate = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 检测结束日期
        /// </summary>
        private string endTestDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string EndTestDate { get { return endTestDate; } set { endTestDate = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 开始检测编号
        /// </summary>
        private string startTestNo;
        public string StartTestNo { get { return startTestNo; } set { startTestNo = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 结束检测编号
        /// </summary>
        private string endTestNo;
        public string EndTestNo { get { return endTestNo; } set { endTestNo = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExportViewModel()
        {

        }

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
                var sql = new StringBuilder(@"SELECT row_number()over(order by m.update_dt desc )as row_num,
m.id,m.org_no,m.test_no,m.sample_no,m.test_type_no,m.test_item_no,m.deadline,
d.* 
FROM [dbo].[biz_execute_test] m INNER JOIN [dbo].biz_execute_test_detail d ON m.id=d.test_id
WHERE m.is_deleted=0 AND d.is_deleted=0");

                SqlParameter[] parameters = null;

                if (QueryType == "TD")
                {
                    sql.Append($" AND m.create_dt BETWEEN @startTestDate AND @endTestDate");
                    parameters = new SqlParameter[2] { new SqlParameter("@startTestDate", StartTestDate), new SqlParameter("@endTestDate", EndTestDate) };
                }
                else if (QueryType == "TN")
                {
                    sql.Append($" AND m.test_no >=@startTestNo AND m.test_no<=@endTestNo");
                    parameters = new SqlParameter[2] { new SqlParameter("@startTestNo", StartTestDate), new SqlParameter("@endTestNo", EndTestDate) };
                }

                TModels.Clear();//清空前端分页数据

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data != null && data.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in data.Rows)
                        {
                            TModels.Add(new ExportModel()
                            {
                                Id = Convert.ToInt64(dataRow["id"]),
                                RowNum = Convert.ToInt64(dataRow["row_num"]),
                                OrgNo = dataRow["category"].ToString(),
                                TestNo = dataRow["value"].ToString(),
                                SampleNo = dataRow["text"].ToString(),
                                TestItemNo = dataRow["remark"].ToString(),
                                IsEnabled = Convert.ToInt32(dataRow["is_enabled"]),
                                CreateDt = Convert.ToDateTime(dataRow["create_dt"]),
                                UpdateDt = Convert.ToDateTime(dataRow["update_dt"]),
                            });
                        }
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
            this.QueryType = string.Empty;
            this.StartTestDate= DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.EndTestDate = DateTime.Now.ToString("yyyy-MM-dd");
            this.StartTestNo = string.Empty;
            this.EndTestNo = string.Empty;

            this.Query();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public override void Edit(int id)
        {
            try
            {

                /*
                var selected = GridData.Where(w => w.IsSelected == true).ToList();

                if (selected.Count != 1)
                {
                    MessageBox.Show($"请选择一条记录进行编辑", "提示信息");
                    return;
                }

                id = (int)selected.First().Id;

                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark],[is_enabled],[is_deleted],[create_dt]
                ,[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_Export] WHERE [is_deleted]=0 AND [id]=@id");

                var parameters = new SqlParameter[1] { new SqlParameter("@id", id) };
                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (data == null || data.Rows.Count == 0)
                    {
                        MessageBox.Show($"数据库不存在 主键ID={id} 的记录", "提示信息");
                        return;
                    }

                    var dataRow = data.Rows[0];
                    var model = new ExportModel()
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
                            sql.Append(@"UPDATE [dbo].[sys_Export]
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
                            new SqlParameter("@user_id", SessionInfo.Instance.Session.Id),
                            new SqlParameter("@id", id)
                        };

                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);

                            this.Query();
                        }
                    }
                }
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            try
            {
                var selected = GridData.Where(w => w.IsSelected == true).ToList();

                if (selected.Count == 0)
                {
                    MessageBox.Show($"请至少选择一条记录进行删除", "提示信息");
                    return;
                }

                if (selected != null)
                {
                    var r = MessageBox.Show($"确定要删除【{string.Join(",", selected.Select(s => $"{s.OrgNo}|{s.TestNo}|{s.SampleNo}"))}】吗？", "提示", MessageBoxButton.YesNo);
                    if (r == MessageBoxResult.Yes)
                    {
                        foreach (var dr in selected)
                        {
                            //var sql = new StringBuilder(@"DELETE FROM [dbo].[sys_Export] WHERE [id] IN(@id)");
                            var sql = new StringBuilder(@"UPDATE [dbo].[sys_Export] SET [is_deleted]=1 WHERE [id]=@id");

                            var parameters = new SqlParameter[1] { new SqlParameter("@id", dr.Id) };
                            var result = SQLHelper.ExecuteNonQuery(sql.ToString(), parameters);
                        }

                        this.Query();
                    }
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
