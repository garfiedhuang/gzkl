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
        private DateTime startTestDate = DateTime.Now.AddDays(-7);
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
            SelectCommand = new RelayCommand<string>(this.Select);
            ExportCommand = new RelayCommand<string>(this.Export);
        }

        #endregion

        #region Command

        /// <summary>
        /// 选择
        /// </summary>
        public RelayCommand<string> SelectCommand { get; set; }

        /// <summary>
        /// 导出
        /// </summary>
        public RelayCommand<string> ExportCommand { get; set; }

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
                                TestPreceptName = dataRow["test_precept_name"].ToString(),
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
                                DeformSensorName = dataRow["deform_sensor_name"].ToString(),
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

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="obj"></param>
        public void Select(string obj)
        {
            if (obj== "CheckAll")
            {//全选
                foreach (var item in GridData)
                {
                    if (!item.IsSelected) item.IsSelected = true;
                }
            }
            else
            { //反选
                foreach (var item in GridData)
                {
                    item.IsSelected = !item.IsSelected;
                }
            }
        }

        /// <summary>
        /// 导出/全部导出
        /// </summary>
        /// <param name="obj"></param>
        public void Export(string obj)
        {
            MessageBox.Show("小伙钓鱼去了，暂时没有时间写代码！");

            /* ==> 导出逻辑
             procedure TfrmDataEpt.Button5Click(Sender: TObject);
var
  constr: string;
  filename: string;
  testMain_id: string;
begin
  if dxMemData1.RecordCount=0 then
  begin
    showmessage('没有要导出的数据！');
    exit;
  end;

  if rd1.ItemIndex=0 then
    filename := formatdatetime('yyyymmdd-hhnnss',now())+'.mdb'
  else
    filename := edtmin.Text+'~'+edtMax.Text+'.mdb';

  if FileExists(CurrentDir+'Export\'+filename) then
    deletefile(CurrentDir+'Export\'+filename);

  frmMain.adoconn.Close;
  CopyFile(Pchar(CurrentDir+'DB\Press1.mdb'),Pchar(CurrentDir+'Export\'+filename),false);

  adoconn.Close;
  CreateDSN(CurrentDir+'Export\'+filename, 'AutoAcsDBout', 'AutoAcs');
  constr:='FILE NAME='+CurrentDir+'DBEPTLink.udl';

  adoconn.Close;
  adoconn.ConnectionString:=constr;

  adoconn.Close;
  adoconn.ConnectionString:=constr;
  try
    adoconn.Connected :=true;
  except
    showmessage('无法连接数据库，请检测数据库是否存在！');
    EncrypMDB(CurrentDir+'Export\'+filename);
    exit;
  end;

  testMain_id:='0,';
  dxMemData1.DisableControls;
  dxMemData1.first;
  while not dxMemData1.Eof do
  begin
    if dxMemData1.FieldByName('flag').AsBoolean then
    begin
      if Pos(','+dxMemData1.fieldbyname('testMain_id').AsString+',', testMain_id)=0 then
       testMain_id :=testMain_id+dxMemData1.fieldbyname('testMain_id').AsString+',';
    end;
    dxMemData1.Next;
  end;
  testMain_id:=testMain_id+'0';
  dxMemData1.EnableControls;
  //清除不需要的数据
  acddel.CommandText:='delete from testno where testMain_id not in ('+testMain_id+')';
  acddel.Execute;

  acddel.CommandText:='delete from OriginalData where testMain_id not in ('+testMain_id+')';
  acddel.Execute;

  acddel.CommandText:='delete from testMain where id  not in ('+testMain_id+')';
  acddel.Execute;

  showmessage('数据已成功导出到'+CurrentDir+'Export\'+filename);
  adoconn.Close;
  dxMemData1.Close;

  CompactDatabase(CurrentDir+'Export\'+filename,'AutoAcs');
end;
             
             */



            /* ==> 全部导出逻辑
             procedure TfrmDataEpt.Button1Click(Sender: TObject);
var
  constr: string;
  filename: string;
begin
  if dxMemData1.RecordCount=0 then
  begin
    showmessage('没有要导出的数据！');
    exit;
  end;

  if rd1.ItemIndex=0 then
    filename := formatdatetime('yyyymmdd-hhnnss',now())+'.mdb'
  else
    filename := edtmin.Text+'~'+edtMax.Text+'.mdb';

  if FileExists(CurrentDir+'Export\'+filename) then
    deletefile(CurrentDir+'Export\'+filename);

  frmMain.adoconn.Close;
  CopyFile(Pchar(CurrentDir+'DB\Press1.mdb'),Pchar(CurrentDir+'Export\'+filename),false);

  adoconn.Close;
  CreateDSN(CurrentDir+'Export\'+filename, 'AutoAcsDBout', 'AutoAcs');
  constr:='FILE NAME='+CurrentDir+'DBEPTLink.udl';

  adoconn.Close;
  adoconn.ConnectionString:=constr;
  try
    adoconn.Connected :=true;
  except
    showmessage('无法连接数据库，请检测数据库是否存在！');
    EncrypMDB(CurrentDir+'Export\'+filename);
    exit;
  end;
  //清除不需要的数据

  if rd1.ItemIndex=0 then
  begin
    acddel.CommandText:='delete from testno where testMain_id not in ( select id from testMain where dates>=#'+testdate.Text+'# and dates<=#'+testdate1.Text+'#)';
    acddel.Execute;

    acddel.CommandText:='delete from OriginalData where testMain_id not in ( select id from testMain where dates>=#'+testdate.Text+'# and dates<=#'+testdate1.Text+'#)';
    acddel.Execute;

    acddel.CommandText:='delete from testMain where dates<#'+testdate.Text+'#';
    acddel.Execute;
    acddel.CommandText:='delete from testMain where dates>#'+testdate1.Text+'#';
    acddel.Execute;
  end
  else
  begin
    acddel.CommandText:='delete from testno where testMain_id not in ( select id from testMain where TestNo>='''+trim(edtmin.Text)+''' and TestNo<='''+Trim(edtmax.Text)+''')';
    acddel.Execute;

    acddel.CommandText:='delete from OriginalData where testMain_id not in ( select id from testMain where TestNo>='''+trim(edtmin.Text)+''' and TestNo<='''+Trim(edtmax.Text)+''')';
    acddel.Execute;

    acddel.CommandText:='delete from testMain where TestNo<'''+trim(edtmin.Text)+'''';
    acddel.Execute;
    acddel.CommandText:='delete from testMain where TestNo>'''+Trim(edtmax.Text)+'''';
    acddel.Execute;
  end;

  showmessage('数据已成功导出到'+CurrentDir+'Export\'+filename);
  adoconn.Close;
  dxMemData1.Close;
  
  CompactDatabase(CurrentDir+'Export\'+filename,'AutoAcs');
end;
             */
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
