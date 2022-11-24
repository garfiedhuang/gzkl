using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.ViewsModels;
using MS.WindowsAPICodePack.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Factories
{
    public abstract class ICollectFactory
    {
        /// <summary>
        /// 采集数据枚举
        /// </summary>
        CollectDataEnum DataEnum { get; }

        /// <summary>
        /// 查询数据(第三方设备Access数据库)
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract void QueryDeviceData(AutoCollectViewModel viewModel);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="viewModel"></param>
        public abstract void ImportData(AutoCollectViewModel viewModel);

        /// <summary>
        /// 新增导入记录
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        internal virtual void AddImportDetail(InterfaceImportDetailInfo data)
        {
            var sql = @"SELECT [test_no] FROM [dbo].[biz_interface_import_detail] WHERE [interface_id]=@interfaceId AND [test_item_id]=@testItemId AND [test_item_no]=@testItemNo AND [sample_no]=@sampleNo";

            var parameters = new SqlParameter[4] {
                new SqlParameter("@interfaceId", data.InterfaceId),
                new SqlParameter("@testItemId", data.InterfaceTestItemId),//接口检测项ID
                new SqlParameter("@testItemNo", data.SystemTestItemNo),//系统检测项编号
                new SqlParameter("@sampleNo", data.SampleNo)
            };

            var dbTestNo = SQLHelper.ExecuteScalar(sql, parameters).ToString();

            if (!string.IsNullOrEmpty(dbTestNo) && dbTestNo != data.TestNo)
            {
                throw new Exception($"本样品[{data.SampleNo}]数据已导入，且与上次的检测编号不一致，不能再次导入！");
            }

            //写入数据库
            sql = @"INSERT INTO [dbo].[biz_interface_import_detail]
           ([interface_id]
           ,[test_item_no]
           ,[test_item_id]
           ,[test_no]
           ,[sample_no]
           ,[remark]
           ,[is_enabled]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@interfaceId
           ,@testItemNo
           ,@testItemId
           ,@testNo
           ,@sampleNo
           ,@remark,
           ,1
           ,0
           ,@createDt
           ,@userId
           ,@createDt
           ,@userId)";

            parameters = new SqlParameter[8] {
                new SqlParameter("@interfaceId", data.InterfaceId),
                new SqlParameter("@testItemId", data.InterfaceTestItemId),//接口检测项ID
                new SqlParameter("@testItemNo", data.SystemTestItemNo),//系统检测项编号
                new SqlParameter("@testNo", data.TestNo),
                new SqlParameter("@sampleNo", data.SampleNo),
                new SqlParameter("@remark", data.Remark),
                new SqlParameter("@createDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@userId", SessionInfo.Instance.Session.Id)
            };

            var result = SQLHelper.ExecuteNonQuery(sql, parameters);

            if (result == 0)
            {
                throw new Exception($"本样品[{data.SampleNo}]数据，写入[biz_interface_import_detail]表失败！");
            }
        }

        /// <summary>
        /// 新增检测记录
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        internal virtual long AddTest(ExecuteTestInfo data)
        {
            //写入数据库
            var sql = @"INSERT INTO [dbo].[biz_execute_test]
           ([org_no]
           ,[test_no]
           ,[sample_no]
           ,[test_type_no]
           ,[test_item_no]
           ,[deadline]
           ,[test_time]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id]) OUTPUT [biz_execute_test].[id]
     VALUES
           (@orgNo
           ,@testNo
           ,@sampleNo
           ,@testTypeNo
           ,@testItemNo
           ,@testTime
           ,@deadline
           ,0
           ,@createDt
           ,@userId
           ,@createDt
           ,@userId)";

            var parameters = new SqlParameter[9] {
                new SqlParameter("@orgNo", data.OrgNo),
                new SqlParameter("@testNo", data.TestNo),
                new SqlParameter("@sampleNo", data.SampleNo),
                new SqlParameter("@testTypeNo",data.TestTypeNo),//接口检测类型编号
                new SqlParameter("@testItemNo", data.TestItemNo),//系统检测项编号
                new SqlParameter("@testTime", data.TestNo),
                new SqlParameter("@deadline", data.Deadline),
                new SqlParameter("@createDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@userId", SessionInfo.Instance.Session.Id)
            };

            var result = Convert.ToInt64(SQLHelper.ExecuteScalar(sql, parameters)); //result为新增后的主键ID

            if (result < 1)
            {
                throw new Exception($"本样品[{data.SampleNo}]数据，写入[biz_execute_test]表失败！");
            }

            return result;
        }

        /// <summary>
        /// 新增检测明细记录
        /// </summary>
        /// <param name="sampleNo"></param>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        internal virtual void AddTestDetail(string sampleNo, ExecuteTestDetailInfo data)
        {
            //写入数据库
            var sql = @"INSERT INTO [dbo].[biz_execute_test_detail]
           ([test_id]
           ,[experiment_no]
           ,[play_time]
           ,[press_unit_name]
           ,[file_name]
           ,[sample_shape]
           ,[area]
           ,[gauge_length]
           ,[up_yield_dot]
           ,[down_yield_dot]
           ,[max_dot]
           ,[sample_width]
           ,[sample_thick]
           ,[sample_dia]
           ,[sample_min_dia]
           ,[sample_out_dia]
           ,[sample_inner_dia]
           ,[load_unit_name]
           ,[encription]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id]) OUTPUT [biz_execute_test_detail].[id]
     VALUES
           (@testId
           ,@experimentNo
           ,@playTime
           ,@pressUnitName
           ,@fileName
           ,@sampleShape
           ,@area
           ,@gaugeLength
           ,@upYieldDot
           ,@downYieldDot
           ,@maxDot
           ,@sampleWidth
           ,@sampleThick
           ,@sampleDia
           ,@sampleMinDia
           ,@sampleOutDia
           ,@sampleInnerDia
           ,@loadUnitName
           ,@encription
           ,0
           ,@createDt
           ,@userId
           ,@createDt,
           ,@userId)";

            var parameters = new SqlParameter[21] {
                        new SqlParameter("@testId", data.TestId),
                        new SqlParameter("@experimentNo", data.ExperimentNo),
                        new SqlParameter("@playTime", data.PlayTime),
                        new SqlParameter("@pressUnitName", data.PressUnitName),
                        new SqlParameter("@fileName", data.FileName),//系统检测项编号
                        new SqlParameter("@sampleShape", data.SampleShape),
                        new SqlParameter("@area", data.Area),
                        new SqlParameter("@gaugeLength", data.GaugeLength),
                        new SqlParameter("@upYieldDot", data.UpYieldDot),
                        new SqlParameter("@downYieldDot", data.DownYieldDot),
                        new SqlParameter("@maxDot", data.MaxDot),
                        new SqlParameter("@sampleWidth", data.SampleWidth),
                        new SqlParameter("@sampleThick", data.SampleThick),
                        new SqlParameter("@sampleDia", data.SampleDia),
                        new SqlParameter("@sampleMinDia", data.SampleMinDia),
                        new SqlParameter("@sampleOutDia", data.SampleOutDia),
                        new SqlParameter("@sampleInnerDia", data.SampleInnerDia),
                        new SqlParameter("@loadUnitName", data.LoadUnitName),
                        new SqlParameter("@encription", data.Encryption),
                        new SqlParameter("@createDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        new SqlParameter("@userId", SessionInfo.Instance.Session.Id)
                };

            var result = Convert.ToInt64(SQLHelper.ExecuteScalar(sql, parameters)); //result为新增后的主键ID

            if (result < 1)
            {
                throw new Exception($"本样品[{sampleNo}]数据，写入[biz_execute_test_detail]表失败！");
            }

            //更新加密字段encription
            sql = @"UPDATE [dbo].[biz_execute_test_detail] SET [encription]=@encription WHERE [id]=@id";
            parameters = new SqlParameter[2] {
                        new SqlParameter("@id", result),
                        new SqlParameter("@encription",EncryptMaxDot(result,data.MaxDot))
            };
            
            result = SQLHelper.ExecuteNonQuery(sql, parameters);
            if (result < 1)
            {
                throw new Exception($"本样品[{sampleNo}]数据，更新[biz_execute_test_detail]表加密字段失败！");
            }

        }

        /// <summary>
        /// 新增原始数据记录
        /// </summary>
        /// <param name="sampleNo"></param>
        /// <param name="viewModel"></param>
        /// <exception cref="Exception"></exception>
        internal virtual void AddOriginalData(string sampleNo, ExecuteOriginalDataInfo data)
        {
            //写入数据库
            var sql = @"INSERT INTO [dbo].[biz_original_data]
           ([test_id]
           ,[experiment_no]
           ,[play_time]
           ,[load_value]
           ,[position_value]
           ,[extend_value]
           ,[big_deform_value]
           ,[deform_switch]
           ,[ctrl_step]
           ,[extend_device1]
           ,[extend_device2]
           ,[extend_device3]
           ,[extend_device4]
           ,[extend_device5]
           ,[extend_device6]
           ,[posi_speed]
           ,[stress_speed]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@testId
           ,@experimentNo
           ,@playTime
           ,@loadValue
           ,@positionValue
           ,@extendValue
           ,@bigDeformValue
           ,@deformSwitch
           ,@ctrlStep
           ,@extendDevice1
           ,@extendDevice2
           ,@extendDevice3
           ,@extendDevice4
           ,@extendDevice5
           ,@extendDevice6
           ,@posiSpeed
           ,@stressSpeed
           ,0
           ,@createDt
           ,@userId
           ,@createDt
           ,@userId)";

            var parameters = new SqlParameter[19] {
                                    new SqlParameter("@testId", data.TestId),
                                    new SqlParameter("@experimentNo", data.ExperimentNo),
                                    new SqlParameter("@playTime", data.PlayTime),
                                    new SqlParameter("@loadValue", data.LoadValue),
                                    new SqlParameter("@positionValue", data.PositionValue),
                                    new SqlParameter("@extendValue", data.ExtendValue),
                                    new SqlParameter("@bigDeformValue", data.BigDeformValue),
                                    new SqlParameter("@deformSwitch", data.DeformSwitch),
                                    new SqlParameter("@ctrlStep", data.CtrlStep),
                                    new SqlParameter("@extendDevice1", data.ExtendDevice1),
                                    new SqlParameter("@extendDevice2", data.ExtendDevice2),
                                    new SqlParameter("@extendDevice3", data.ExtendDevice3),
                                    new SqlParameter("@extendDevice4", data.ExtendDevice4),
                                    new SqlParameter("@extendDevice5", data.ExtendDevice5),
                                    new SqlParameter("@extendDevice6", data.ExtendDevice6),
                                    new SqlParameter("@posiSpeed", data.PosiSpeed),
                                    new SqlParameter("@stressSpeed", data.StressSpeed),
                                    new SqlParameter("@createDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                    new SqlParameter("@userId", SessionInfo.Instance.Session.Id)
                };

            var result = SQLHelper.ExecuteNonQuery(sql, parameters);

            if (result < 1)
            {
                throw new Exception($"本样品[{sampleNo}]数据，写入[biz_original_data]表失败！");
            }

            /* old logic 
             * 
            var result = 0;

            foreach (DataRow dr in viewModel.Model.UnfinishTestDetailData?.Rows)
            {
                //写入数据库
                var sql = @"INSERT INTO [dbo].[biz_original_data]
           ([test_id]
           ,[experiment_no]
           ,[play_time]
           ,[load_value]
           ,[position_value]
           ,[extend_value]
           ,[big_deform_value]
           ,[deform_switch]
           ,[ctrl_step]
           ,[extend_device1]
           ,[extend_device2]
           ,[extend_device3]
           ,[extend_device4]
           ,[extend_device5]
           ,[extend_device6]
           ,[posi_speed]
           ,[stress_speed]
           ,[is_deleted]
           ,[create_dt]
           ,[create_user_id]
           ,[update_dt]
           ,[update_user_id])
     VALUES
           (@testId
           ,@experimentNo
           ,@playTime
           ,@loadValue
           ,@positionValue
           ,@extendValue
           ,@bigDeformValue
           ,@deformSwitch
           ,@ctrlStep
           ,@extendDevice1
           ,@extendDevice2
           ,@extendDevice3
           ,@extendDevice4
           ,@extendDevice5
           ,@extendDevice6
           ,@posiSpeed
           ,@stressSpeed
           ,0
           ,@createDt
           ,@userId
           ,@createDt
           ,@userId)";

                var experimentNo = viewModel.Model.UnfinishTestData.Rows[0]["experiment_no"].ToString();
                var playTime = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var loadValue = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var positionValue = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendValue = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var bigDeformValue = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var deformSwitch = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var ctrlStep = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice1 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice2 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice3 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice4 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice5 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var extendDevice6 = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var posiSpeed = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();
                var stressSpeed = viewModel.Model.UnfinishTestData.Rows[0]["Dates"].ToString();

                var parameters = new SqlParameter[19] {
                                    new SqlParameter("@testId", testId),
                                    new SqlParameter("@experimentNo", experimentNo),
                                    new SqlParameter("@playTime", playTime),
                                    new SqlParameter("@loadValue", loadValue),
                                    new SqlParameter("@positionValue", positionValue),
                                    new SqlParameter("@extendValue", extendValue),
                                    new SqlParameter("@bigDeformValue", bigDeformValue),
                                    new SqlParameter("@deformSwitch", deformSwitch),
                                    new SqlParameter("@ctrlStep", ctrlStep),
                                    new SqlParameter("@extendDevice1", extendDevice1),
                                    new SqlParameter("@extendDevice2", extendDevice2),
                                    new SqlParameter("@extendDevice3", extendDevice3),
                                    new SqlParameter("@extendDevice4", extendDevice4),
                                    new SqlParameter("@extendDevice5", extendDevice5),
                                    new SqlParameter("@extendDevice6", extendDevice6),
                                    new SqlParameter("@posiSpeed", posiSpeed),
                                    new SqlParameter("@stressSpeed", stressSpeed),
                                    new SqlParameter("@createDt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                    new SqlParameter("@userId", SessionInfo.Instance.Session.Id)
                };

                result += SQLHelper.ExecuteNonQuery(sql, parameters);
            }

            if (result != viewModel.Model.UnfinishTestDetailData?.Rows.Count)
            {
                throw new Exception($"本样品[{viewModel.Model.QuerySampleNo}]数据，写入[biz_original_data]表失败！");
            }

            */
        }

        /// <summary>
        /// 格式转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal virtual string DataAmpt(double value)
        {
            var result = "0";
            var tempValue = Math.Abs(value);
            if (tempValue >= 100)
            {
                result = Math.Round(value, 1).ToString();
            }
            else if (tempValue >= 10 && tempValue < 100)
            {
                result = Math.Round(value, 2).ToString();
            }
            else if (tempValue < 10)
            {
                result = Math.Round(value, 3).ToString();
            }

            return result;
        }

        /// <summary>
        /// 加密MaxDox
        /// 加密字符格式：ontall_id_max_dot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="maxDot"></param>
        /// <returns></returns>
        internal string EncryptMaxDot(long id,string maxDot)
        {
            var plaintext = $"ontall_{id}_{maxDot}";
            var ciphertext = SecurityHelper.SHA512Encrypt(plaintext);

            return ciphertext;
        }

    }
}
