using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GZKL.Client.UI.Common
{

    /*
     * ODBC:
     * 
     ODBC是Open DataBase Connectivity的缩写，中文意思是“开放式数据库连接标准”。
     ODBC是微软公司为Windows操作系统推出的一套访问各种数据库的统一接口技术。
     ODBC类似于一种软件驱动程序，通过这种驱动程序提供应用程序与数据库之间的连接标准。
     
    *
    *DSN:
    *
     Data Source Name (DSN)的PDO命名惯例为：PDO驱动程序的名称，后面为一个冒号，再后面是可选的驱动程序连接数据库变量信息，如主机名、端口和数据库名。
     比如： 
     */
    public static class DsnHelper
    {
        private const string ODBC_INI_REG_PATH = "SOFTWARE\\ODBC\\ODBC.INI\\";
        private const string ODBCINST_INI_REG_PATH = "SOFTWARE\\ODBC\\ODBCINST.INI\\";

        /// <summary>
        /// Creates a new DSN entry with the specified values. If the DSN exists, the values are updated.
        /// </summary>
        /// <param name="dsnName">Name of the DSN for use by client applications</param>
        /// <param name="password">database password</param>
        /// <param name="database">Name of the datbase to connect to</param>
        public static void CreateDSN(string dsnName, string password, string database)
        {
            //CreateDSN(CurrentDir+'db\Press1.mdb', 'AutoAcsDB', 'AutoAcs');

            //dsnName    -> dsn 名称 -> AutoAcsDB
            //description -> 数据源描述 -> 自动接口采集数据源
            //driverName -> 驱动名称 -> Microsoft Access Driver (*.mdb)

            //database   -> 数据库路径(绝对路径)  -> F:\gzkl\gzkl-source\db\Press1.mdb

            // <param name="description">Description of the DSN that appears in the ODBC control panel applet</param>
            // <param name="driverName">Name of the driver to use</param>
            // <param name="trustedConnection">True to use NT authentication, false to require applications to supply username/password in the connection string</param>

            string description = "采集软件数据源，请勿删除";
            string driverName = "Microsoft Access Driver (*.mdb)";


            // Lookup driver path from driver name
            var driverKey = Registry.LocalMachine.CreateSubKey(ODBCINST_INI_REG_PATH + driverName);
            if (driverKey == null)
            {
                //SOFTWARE\\ODBC\\ODBC.INI\\AutoAcsDB
                throw new Exception(string.Format("ODBC Registry key for driver '{0}' does not exist", driverName));
            }
            string driverPath = driverKey.GetValue("Driver").ToString();

            // Add value to odbc data sources
            var datasourcesKey = Registry.LocalMachine.CreateSubKey(ODBC_INI_REG_PATH + "ODBC Data Sources");
            if (datasourcesKey == null)
            {
                throw new Exception("ODBC Registry key for datasources does not exist");
            }
            datasourcesKey.SetValue(dsnName, driverName);

            // Create new key in odbc.ini with dsn name and add values
            var dsnKey = Registry.LocalMachine.CreateSubKey(ODBC_INI_REG_PATH + dsnName);
            if (dsnKey == null)
            {
                throw new Exception("ODBC Registry key for DSN was not created");
            }

            dsnKey.SetValue("DBQ", database);
            dsnKey.SetValue("Description", description);
            dsnKey.SetValue("Driver", driverPath);//C:\windows\SYSTEM32\odbcjt32.dll
            dsnKey.SetValue("LastUser", Environment.UserName);
            dsnKey.SetValue("DriverId", 25);
            dsnKey.SetValue("FIL", "Ms Access;");//驱动程序表示
            dsnKey.SetValue("SafeTransaction", 0);//Filter依据
            dsnKey.SetValue("PWD", password);

            dsnKey.Close();

            /*
                        //找到或创建Software\ODBC\ODBC.INI\MyAccess\Engines\Jet
                        //写入DSN数据库引擎配置信息
                        if OpenKey('Software\ODBC\ODBC.INI\'+sName+'\Engines\Jet',True) then

                        begin

                          WriteString('ImplicitCommitSync', 'Yes');
                          WriteInteger('MaxBufferSize', 2048);//缓冲区大小
                          WriteInteger('PageTimeout', 10);//页超时
                          WriteInteger('Threads', 3);//支持的线程数目
                          WriteString('UserCommitSync', 'Yes');
                        end;
                        CloseKey;
            */
        }

        /// <summary>
        /// Removes a DSN entry
        /// </summary>
        /// <param name="dsnName">Name of the DSN to remove.</param>
        public static void RemoveDSN(string dsnName)
        {
            // Remove DSN key
            Registry.LocalMachine.DeleteSubKeyTree(ODBC_INI_REG_PATH + dsnName);

            // Remove DSN name from values list in ODBC Data Sources key
            var datasourcesKey = Registry.LocalMachine.CreateSubKey(ODBC_INI_REG_PATH + "ODBC Data Sources");
            if (datasourcesKey == null)
            {
                throw new Exception("ODBC Registry key for datasources does not exist");
            }
            datasourcesKey.DeleteValue(dsnName);
        }

        ///<summary>
        /// Checks the registry to see if a DSN exists with the specified name
        /// 32bit机器
        ///</summary>
        ///<param name="dsnName"></param>
        ///<returns></returns>
        public static bool DSNExists(string dsnName)
        {
            var driversKey = Registry.LocalMachine.CreateSubKey(ODBCINST_INI_REG_PATH + "ODBC Drivers");
            if (driversKey == null)
            {
                throw new Exception("ODBC Registry key for drivers does not exist");
            }

            return driversKey.GetValue(dsnName) != null;
        }

        /// <summary>
        /// 64bit机器
        /// </summary>
        /// <param name="dsnName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool DSNExists2(string dsnName)
        {
            var sourcesKey = Registry.LocalMachine.CreateSubKey(ODBC_INI_REG_PATH + "ODBC Data Sources");
            if (sourcesKey == null)
            {
                throw new Exception("ODBC Registry key for sources does not exist");
            }

            return sourcesKey.GetValue(dsnName) != null;
        }

        ///<summary>
        /// Returns an array of driver names installed on the system
        ///</summary>
        ///<returns></returns>
        public static string[] GetInstalledDrivers()
        {
            var driversKey = Registry.LocalMachine.CreateSubKey(ODBCINST_INI_REG_PATH + "ODBC Drivers");
            if (driversKey == null) throw new Exception("ODBC Registry key for drivers does not exist");

            var driverNames = driversKey.GetValueNames();

            var ret = new List<string>();

            foreach (var driverName in driverNames)
            {
                if (driverName != "(Default)")
                {
                    ret.Add(driverName);
                }
            }

            return ret.ToArray();
        }
    }
}
