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
using GZKL.Client.UI.Views.CollectMgt.Parameter;
using GalaSoft.MvvmLight.Messaging;
using System.Reflection;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Media.Media3D;

namespace GZKL.Client.UI.ViewsModels
{
    public class ParameterViewModel : ViewModelBase
    {
        #region Construct and property

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterViewModel()
        {
            BackupCommand = new RelayCommand(this.Backup);
            SelectCommand = new RelayCommand(this.Select);

            GetDropdownListData();

            var computerInfo = SessionInfo.Instance.ComputerInfo;
            GetParameterInfo($"{computerInfo.HostName}-{computerInfo.CPU}");
        }

        /// <summary>
        /// 当前电脑所有参数配置
        /// </summary>
        private List<ConfigModel> CurrentParameters { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        private ParameterModel model;
        public ParameterModel Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        private List<KeyValuePair<string, string>> serialPortData = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// 串行口下拉框列表
        /// </summary>
        public List<KeyValuePair<string, string>> SerialPortData
        {
            get { return serialPortData; }
            set
            {
                serialPortData = value; RaisePropertyChanged(() => SerialPortData);
            }
        }

        private List<KeyValuePair<string, string>> testerData = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// 试验机下拉框列表
        /// </summary>s
        public List<KeyValuePair<string, string>> TesterData
        {
            get { return testerData; }
            set
            {
                testerData = value; RaisePropertyChanged(() => TesterData);
            }
        }

        private List<KeyValuePair<string, string>> sensorRangeData = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// 传感器量程下拉框列表
        /// </summary>
        public List<KeyValuePair<string, string>> SensorRangeData
        {
            get { return sensorRangeData; }
            set
            {
                sensorRangeData = value; RaisePropertyChanged(() => SensorRangeData);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// 备份
        /// </summary>
        public RelayCommand BackupCommand { get; set; }

        /// <summary>
        /// 选择
        /// </summary>
        public RelayCommand SelectCommand { get; set; }

        /// <summary>
        /// 采集类型
        /// </summary>
        public RelayCommand<string> CollectTypeCheckedCommand { get; set; }

        /// <summary>
        /// 无锡建议小数位
        /// </summary>
        public RelayCommand<string> DecimalDigitsTypeCheckedCommand { get; set; }

        #endregion


        #region Command implement

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="collectType"></param>
        /// <param name="decimalDigitsType"></param>
        public void Save(string collectType,string decimalDigitsType)
        {
            try
            {
                string sql = @"BEGIN
     DECLARE @iRowCount INT;
	 SELECT @iRowCount= COUNT(1) FROM [dbo].[sys_config] WHERE [category]=@category AND [value]=@value AND [is_deleted]=0;

	 IF @iRowCount=0
	    BEGIN
	        INSERT INTO [dbo].[sys_config]
             ([category],[value],[text],[remark],[is_enabled],[is_deleted]
             ,[create_dt],[create_user_id],[update_dt],[update_user_id])
            VALUES
             (@category,@value,@text,@remark
            ,1,0,GETDATE(),@userId,GETDATE(),@userId);
		END
	  ELSE
	    BEGIN
		    UPDATE [dbo].[sys_config] SET [text]=@text,[update_dt]=GETDATE(),[update_user_id]=@userId WHERE [category]=@category AND [value]=@value AND [is_deleted]=0;
		END
END";

                //获取当前Model所有属性
                var properties = typeof(ParameterModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                var computerInfo = SessionInfo.Instance.ComputerInfo;
                var fullName = $"{computerInfo.HostName}-{computerInfo.CPU}";
                var userInfo = SessionInfo.Instance.Session;

                var category = string.Empty;
                var value = string.Empty;
                var text = string.Empty;
                var remark = string.Empty;
                var effectRowCount = 0;//影响行数

                foreach (var property in properties)
                {
                    value = ((DescriptionAttribute)Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute))).Description;// 属性值
                    text = property.GetValue(Model, null).ToString();  //值

                    if ("量程1,量程2,量程3,最大量程,试验机型号,量程系数".Contains(value))
                    {
                        //ChannelParams-{HostName}-{CPU}-{No}
                        category = $"ChannelParams-{fullName}-{Model.Tester}";
                        remark = "通道参数";
                    }
                    else
                    {
                        //CommonParams-{HostName}-{CPU}
                        category = $"CommonParams-{fullName}";
                        remark = "公用参数";
                    }

                    //特殊处理
                    switch (value)
                    {
                        case "绘图间隔":
                            text = "3";
                            break;
                        case "补偿有效":
                            text = "False";
                            break;
                        case "采集类型":
                            if (!string.IsNullOrEmpty(collectType))
                            {
                                text= collectType;//$"{rbCollectType?.Tag}#{rbCollectType.Content}";
                            }
                            break;
                        case "TYE小数位":
                            if (!string.IsNullOrEmpty(decimalDigitsType))
                            {
                                text = decimalDigitsType;//$"{rbDecimalDigitType?.Tag}#{rbDecimalDigitType.Content}";
                            }
                            break;
                        default:
                            break;
                    }

                    var parameters = new SqlParameter[] {
                        new SqlParameter("@category", category),
                        new SqlParameter("@value", value),
                        new SqlParameter("@text", text),
                        new SqlParameter("@remark", remark),
                        new SqlParameter("@userId", userInfo.Id)
                    };

                    effectRowCount += SQLHelper.ExecuteNonQuery(sql, parameters);
                }

                if (effectRowCount > 0)
                {
                    var res = MessageBox.Show($"参数设置成功", "提示信息");
                }
                else
                {
                    throw new Exception($"参数设置失败，请与管理员联系");
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        public void Select()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion

        #region Privates

        /// <summary>
        /// 获取下拉框列表集合
        /// </summary>
        /// <returns></returns>
        private void GetDropdownListData()
        {
            try
            {
                var sql = new StringBuilder(@"SELECT [id],[category],[value],[text],[remark]
                ,[is_enabled],[is_deleted],[create_dt],[create_user_id],[update_dt],[update_user_id]
                FROM [dbo].[sys_config] WHERE [category] IN('SerialPort','Tester','SensorRange') AND [is_deleted]=0 ORDER BY [category] DESC");

                using (var data = SQLHelper.GetDataTable(sql.ToString()))
                {
                    SerialPortData.Clear();
                    TesterData.Clear();
                    SensorRangeData.Clear();

                    if (data != null && data.Rows.Count > 0)
                    {
                        var value = string.Empty;
                        foreach (DataRow dr in data.Rows)
                        {
                            value = dr["category"].ToString();

                            switch (value)
                            {
                                case "SerialPort":
                                    SerialPortData.Add(new KeyValuePair<string, string>(dr["value"].ToString(), dr["text"].ToString()));
                                    break;
                                case "Tester":
                                    TesterData.Add(new KeyValuePair<string, string>(dr["value"].ToString(), dr["text"].ToString()));
                                    break;
                                case "SensorRange":
                                    SensorRangeData.Add(new KeyValuePair<string, string>(dr["value"].ToString(), dr["text"].ToString()));
                                    break;
                                default:
                                    break;
                            }
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
        /// 获取参数配置信息
        /// </summary>
        /// <param name="fullName"></param>
        internal void GetParameterInfo(string fullName)
        {
            try
            {
                //初始化模型
                if (Model == null)
                {
                    Model = new ParameterModel();
                }

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

                using (var data = SQLHelper.GetDataTable(sql.ToString(), parameters))
                {
                    if (CurrentParameters == null)
                    {
                        CurrentParameters = new List<ConfigModel>();
                    }
                    else
                    {
                        CurrentParameters.Clear();
                    }

                    if (data != null && data.Rows.Count > 0)
                    {
                        var value = string.Empty;
                        foreach (DataRow dataRow in data.Rows)
                        {
                            CurrentParameters.Add(new ConfigModel()
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

                var commParams = CurrentParameters.Where(w => w.Category == category1)?.ToList();

                if (commParams != null && commParams.Count > 0)
                {
                    //公用参数
                    Model.SerialPort = commParams.FirstOrDefault(s => s.Value == "串行口")?.Text;
                    Model.Tester = commParams.FirstOrDefault(s => s.Value == "通道号")?.Text;//与[试验机]下拉框映射
                    Model.ExitMinValue = commParams.FirstOrDefault(s => s.Value == "自动结束最小值")?.Text;
                    Model.FailureJudgment = commParams.FirstOrDefault(s => s.Value == "破坏判断")?.Text;
                    Model.CurrentRangeNo = commParams.FirstOrDefault(s => s.Value == "当前量程号")?.Text??"2";
                    Model.DrawnRange = commParams.FirstOrDefault(s => s.Value == "绘图范围")?.Text;
                    Model.DrawnInterval = commParams.FirstOrDefault(s => s.Value == "绘图间隔")?.Text??"3";//没有维护输入
                    Model.AdjustedFactor = commParams.FirstOrDefault(s => s.Value == "调整系数")?.Text;
                    Model.TwoChannel = Convert.ToBoolean(commParams.FirstOrDefault(s => s.Value == "双通道")?.Text);
                    Model.AutoSwitch = Convert.ToBoolean(commParams.FirstOrDefault(s => s.Value == "自动切换")?.Text);
                    Model.AutoSwitchRatio = commParams.FirstOrDefault(s => s.Value == "切换比例")?.Text;
                    Model.CompensationEffect = Convert.ToBoolean(commParams.FirstOrDefault(s => s.Value == "补偿有效")?.Text);//没有维护输入
                    Model.SaveData = Convert.ToBoolean(commParams.FirstOrDefault(s => s.Value == "是否保存数据")?.Text);
                    Model.SaveGraph = Convert.ToBoolean(commParams.FirstOrDefault(s => s.Value == "是否保存图片")?.Text);
                    Model.SavePath = commParams.FirstOrDefault(s => s.Value == "保存路径")?.Text;

                    var collectType = commParams.FirstOrDefault(s => s.Value == "采集类型")?.Text ?? "";//格式：T001#三和采集SSY
                    if (collectType.Split('#').Length == 2)
                    {
                        Model.CollectType = collectType.Split('#')[0];
                    }
                    else
                    {
                        Model.CollectType = collectType;
                    }

                    var wuxiSuggestedDecimalDigit = commParams.FirstOrDefault(s => s.Value == "TYE小数位")?.Text ?? "";
                    if (wuxiSuggestedDecimalDigit.Split('#').Length == 2)
                    {
                        Model.WuxiSuggestedDecimalDigit = wuxiSuggestedDecimalDigit.Split('#')[0];
                    }
                    else
                    {
                        Model.WuxiSuggestedDecimalDigit = wuxiSuggestedDecimalDigit;
                    }

                    //通道参数
                    var channelParams = CurrentParameters.Where(w => w.Category == $"{category2}{Model.Tester}")?.ToList();
                    if (channelParams != null && channelParams.Count > 0)
                    {
                        Model.FirstGear = channelParams.FirstOrDefault(s => s.Value == "量程1")?.Text;
                        Model.SecondGear = channelParams.FirstOrDefault(s => s.Value == "量程2")?.Text;
                        Model.ThirdGear = channelParams.FirstOrDefault(s => s.Value == "量程3")?.Text;
                        Model.SensorRange = channelParams.FirstOrDefault(s => s.Value == "最大量程")?.Text;
                        Model.TesterName = channelParams.FirstOrDefault(s => s.Value == "试验机型号")?.Text;
                        Model.RangeFactor = channelParams.FirstOrDefault(s => s.Value == "量程系数")?.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息");
            }
        }

        #endregion
    }
}
