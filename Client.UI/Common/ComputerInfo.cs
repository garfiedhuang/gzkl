using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net;

namespace GZKL.Client.UI.Common
{
    public class ComputerInfo
    {
        //private PerformanceCounter cpuCounter;
        //private ManagementClass memCounter;

        private static ComputerInfo instance;

        /// <summary>
        /// cpu
        /// </summary>
        public string CPU { get; set; }

        /// <summary>
        /// Mac
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 硬盘ID
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 主机名称
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// PC类型
        /// </summary>
        public string SystemType { get; set; }

        /// <summary>
        /// 内存大小
        /// </summary>
        public string TotalPhysicalMemory { get; set; }

        /// <summary>
        /// 电脑制造商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 电脑型号
        /// </summary>
        public string SystemFamily { get; set; }

        public static ComputerInfo GetInstance()
        {
            lock ("instance")
            {
                if (instance == null)
                {
                    instance = new ComputerInfo();

                    //instance.cpuCounter = new PerformanceCounter();
                    //instance.cpuCounter.CategoryName = "Processor";
                    //instance.cpuCounter.CounterName = "% Processor Time";
                    //instance.cpuCounter.InstanceName = "_Total";
                    //instance.cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                    //instance.memCounter = new ManagementClass();
                }

                return instance;
            }
        }

        public ComputerInfo ReadComputerInfo()
        {
            var computerInfo = new ComputerInfo();

            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //CPU序列号
                    //Console.WriteLine(mo.Properties["ProcessorId"].Value.ToString());

                    computerInfo.CPU = mo.Properties["ProcessorId"].Value.ToString();

                    //若想获取所有属性，可迭代ManagementObject.Properties，以下同理;
                    //foreach (PropertyData pd in mo.Properties)
                    //{
                    //    Console.WriteLine(pd.Name + "----" + pd.Value);
                    //}
                }
/*
                mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //MAC地址
                        //Console.WriteLine(mo["MacAddress"].ToString());
                        computerInfo.MacAddress = mo["MacAddress"].ToString();

                        //ip地址
                        //Console.WriteLine(((Array)mo.Properties["IpAddress"].Value).GetValue(0).ToString());
                        computerInfo.IpAddress = ((Array)mo.Properties["IpAddress"].Value).GetValue(0).ToString();
                    }
                }

                mc = new ManagementClass("Win32_DiskDrive");
                moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //硬盘ID
                    //Console.WriteLine(mo.Properties["Model"].Value.ToString());
                    computerInfo.Model = mo.Properties["Model"].Value?.ToString();
                }
*/

                mc = new ManagementClass("Win32_ComputerSystem");
                moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //系统名称
                    //Console.WriteLine(mo["Name"].ToString());
                    computerInfo.HostName = mo["Name"].ToString();

                    //登录用户名
                    //Console.WriteLine(mo["UserName"].ToString());
                    computerInfo.UserName = mo["UserName"].ToString();

                    //pc类型
                    //Console.WriteLine(mo["SystemType"].ToString());
                    computerInfo.SystemType = mo["SystemType"].ToString();

                    //内存
                    //Console.WriteLine(mo["TotalPhysicalMemory"].ToString());
                    computerInfo.TotalPhysicalMemory = mo["TotalPhysicalMemory"].ToString();

                    //电脑制造商
                    //Console.WriteLine(mo["Manufacturer"].ToString());
                    computerInfo.Manufacturer = mo["Manufacturer"].ToString();

                    //电脑型号
                    //Console.WriteLine(mo["SystemFamily"].ToString());
                    computerInfo.SystemFamily = mo["SystemFamily"].ToString();
                }

                mc?.Dispose();
                moc?.Dispose();
            }
            catch { }

            return computerInfo;
        }

        /*
        public double GetMemInfo()
        {
            memCounter.Path = new ManagementPath("Win32_PhysicalMemory");
            ManagementObjectCollection moc = memCounter.GetInstances();
            double available = 0, capacity = 0;

            foreach (ManagementObject mo1 in moc)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            moc.Dispose();

            memCounter.Path = new ManagementPath("Win32_PerfFormattedData_PerfOS_Memory");
            moc = memCounter.GetInstances();
            foreach (ManagementObject mo2 in moc)
            {
                available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));
            }
            moc.Dispose();

            return (capacity - available) / capacity * 100;
        }

        public double GetCPUInfo()
        {
            return this.cpuCounter.NextValue();
        }
        */

        public void Dispose()
        {
            //this.memCounter?.Dispose();
            //this.cpuCounter?.Dispose();
        }
    }
}
