using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Views.CollectMgt.Export;
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
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using System.IO;
using GZKL.Client.UI.Factories.Collect;

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
        private DateTime startTestDate = DateTime.Now.AddMonths(-3);
        public DateTime StartTestDate { get { return startTestDate; } set { startTestDate = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 检测结束日期
        /// </summary>
        private DateTime endTestDate = DateTime.Now;
        public DateTime EndTestDate { get { return endTestDate; } set { endTestDate = value; RaisePropertyChanged(); } }

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
                    var parameters1 = new SqlParameter()
                    {
                        ParameterName = "@startTestDate",
                        DbType = DbType.DateTime,
                        Value = StartTestDate
                    };
                    var parameters2 = new SqlParameter()
                    {
                        ParameterName = "@endTestDate",
                        DbType = DbType.DateTime,
                        Value = EndTestDate
                    };
                    parameters = new SqlParameter[] { parameters1, parameters2 };
                }
                else if (QueryType == "TN")
                {
                    sql.Append($" AND m.test_no >=@startTestNo AND m.test_no<=@endTestNo");
                    parameters = new SqlParameter[] { new SqlParameter("@startTestNo", StartTestNo), new SqlParameter("@endTestNo", EndTestNo) };
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
                                OrgNo = dataRow["org_no"].ToString(),
                                TestNo = dataRow["test_no"].ToString(),
                                SampleNo = dataRow["sample_no"].ToString(),
                                TestTypeNo = dataRow["test_type_no"].ToString(),
                                TestItemNo = dataRow["test_item_no"].ToString(),
                                Deadline = dataRow["deadline"].ToString(),
                                ExperimentNo = dataRow["experiment_no"].ToString(),
                                PlayTime = Convert.ToDateTime(dataRow["play_time"].ToString()),
                                LoadUnitName = dataRow["load_unit_name"].ToString(),
                                FileName = dataRow["file_name"].ToString(),
                                SampleShape = dataRow["sample_shape"].ToString(),
                                Area = dataRow["area"].ToString(),
                                GaugeLength = dataRow["gauge_length"].ToString(),
                                UpYieldDot = dataRow["up_yield_dot"].ToString(),
                                DownYieldDot = dataRow["down_yield_dot"].ToString(),
                                MaxDot = dataRow["max_dot"].ToString(),
                                SampleWidth = dataRow["sample_width"].ToString(),
                                SampleThick = dataRow["sample_thick"].ToString(),
                                SampleDia = dataRow["sample_dia"].ToString(),
                                SampleMinDia = dataRow["sample_min_dia"].ToString(),
                                SampleOutDia = dataRow["sample_out_dia"].ToString(),
                                SampleInnerDia = dataRow["sample_inner_dia"].ToString(),
                                PressUnitName = dataRow["press_unit_name"].ToString(),
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
            this.QueryType = "TD";
            this.StartTestDate= DateTime.Now.AddDays(-7);
            this.EndTestDate = DateTime.Now;
            this.StartTestNo = string.Empty;
            this.EndTestNo = string.Empty;

            this.Query();
        }

        #endregion

        #region Privates

        public void Export(List<ExportModel> exportModels)
        {
            try
            {
                SaveData2AccessDb(exportModels);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.Growl.Error(ex?.Message);
                LogHelper.Error(ex?.Message);
            }

        }

        public void ExportAll()
        {
            try
            {
                SaveData2AccessDb(TModels);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.Growl.Error(ex?.Message);
                LogHelper.Error(ex?.Message);
            }
        }

        private void SaveData2AccessDb(List<ExportModel> exportModels)
        {
            var fileName = string.Empty;
            var savePath = string.Empty;

            if (QueryType == "TD")
            {
                fileName = $"{DateTime.Now:yyyyMMdd-HHmmss}.mdb";
            }
            else if (QueryType == "TN")
            {
                fileName = $"{startTestNo}~{endTestNo}.mdb";
            }

            savePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export");

            if (!System.IO.Directory.Exists(savePath))
            {
                System.IO.Directory.CreateDirectory(savePath);
            }

            //将导出的Access数据库模板文件，复制到当前目录下并重命名
            savePath = System.IO.Path.Combine(savePath, fileName);
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }

            var templateFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Press1.mdb");
            if (!File.Exists(templateFile))
            {
                throw new Exception($"Access模板文件不存在，{templateFile}");
            }

            File.Copy(templateFile, savePath);

            //查询原始数据
            var sql = @"
BEGIN
SELECT * FROM biz_execute_test WHERE is_deleted=0 AND id IN(@ids);
SELECT * FROM biz_execute_test_detail WHERE is_deleted=0 AND test_id IN(@ids);
SELECT * FROM biz_original_data WHERE is_deleted=0 AND test_id IN(@ids);
END";
            var parameters = new SqlParameter[1] { new SqlParameter("@ids", string.Join(",", exportModels.Select(s => s.Id).Distinct())) };

            var ds = SQLHelper.GetDataSet(sql, parameters);

            //写入Access模板数据库
            if (ds != null && ds.Tables.Count > 0)
            {
                var tableMasterSql = $"";
                var tableDetailSql = $"";
                var tableOriginalSql = $"";

                var dsnName = $"AutoAcsDBout";
                var pwd = "AutoAcs";
                var database = savePath;
                var path = $"DSN={dsnName}";

                if (string.IsNullOrEmpty(database) || !System.IO.File.Exists(database))
                {
                    throw new Exception($"数据库文件不存在，请检查！{database}");
                }

                DsnHelper.CreateDSN(dsnName, pwd, database);//创建DSN

                var sqls = new StringBuilder();

                for (var i = 0; i < ds.Tables.Count; i++)
                {
                    using (var dt = ds.Tables[i])
                    {
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            continue;
                        }

                        sqls.Clear();

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (i == 0)
                            {
                                sqls.Append("sql1");
                            }
                            else if (i == 1)
                            {
                                sqls.Append("sql1");
                            }
                            else if (i == 2)
                            {
                                sqls.Append("sql1");
                            }
                        }

                        //每个表提交一次数据
                        OdbcHelper.ExcuteSql(sqls.ToString(), database);
                    }
                }
            }

            //压缩数据库文件

        }

        #endregion
    }
}
