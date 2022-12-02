using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GZKL.Client.UI.Common;

namespace GZKL.Client.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static Mutex AppMutex;

        /// <summary>
        /// 构造函数
        /// </summary>
        public App()
        {
            this.InitLog();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            AppMutex = new Mutex(true, "GZKL.Client.UI", out var createdNew);

            if (!createdNew)
            {
                var current = Process.GetCurrentProcess();

                foreach (var process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        Win32Helper.SetForegroundWindow(process.MainWindowHandle);
                        break;
                    }
                }
                Shutdown();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            this.CloseLog();

            base.OnExit(e);
        }

        private void InitLog()
        {
            //启动日志组件
            var directoryInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            var configFile = Path.Combine(directoryInfo.Parent?.FullName ?? string.Empty, "NLog.config");
            LogHelper.Startup(configFile);
        }

        private void CloseLog()
        {
            //关闭日志组件
            LogHelper.Shutdown();
        }
    }
}
