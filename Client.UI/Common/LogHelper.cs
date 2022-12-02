using NLog;
using System;

namespace GZKL.Client.UI.Common
{
    public class LogHelper
    {
        //NLog日志对象
        private static Logger _logger;

        /// <summary>
        /// 初始化日志组件
        /// </summary>
        /// <param name="configFile">NLog日志配置文件</param>
        public static void Startup(string configFile)
        {
            if (string.IsNullOrWhiteSpace(configFile))
            {
                throw new ArgumentException(@"NLog ConfigFile is null or whitespace");
            }

            try
            {
                _logger = LogManager.LoadConfiguration(configFile).GetCurrentClassLogger();
            }
            catch (Exception e)
            {
                Console.WriteLine($@"NLog LoadConfiguration Exception: {e.Message}.");
            }
        }

        /// <summary>
        /// 关闭日志组件
        /// </summary>
        public static void Shutdown()
        {
            _logger = null;
            LogManager.Shutdown();
        }

        public static void Trace(object value)
        {
            _logger?.Trace(value);
        }

        public static void Debug(object value)
        {
            _logger?.Debug(value);
        }

        public static void Info(object value)
        {
            _logger?.Info(value);
        }

        public static void Warn(object value)
        {
            _logger?.Warn(value);
        }

        public static void Error(object value)
        {
            _logger?.Error(value);
        }

        public static void Fatal(object value)
        {
            _logger?.Fatal(value);
        }
    }
}
