using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GZKL.Client.UI.ViewsModels
{
     public class ViewModelLocator
    {
        /// <summary>
        /// 嘿巴扎嘿
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<UserViewModel>();
            SimpleIoc.Default.Register<RoleViewModel>();
            SimpleIoc.Default.Register<ConfigViewModel>();
            SimpleIoc.Default.Register<PermissionViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();

            SimpleIoc.Default.Register<OrgViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<BattchViewModel>();
            SimpleIoc.Default.Register<BackupViewModel>();
            SimpleIoc.Default.Register<ExportViewModel>();
            SimpleIoc.Default.Register<InterfaceViewModel>();
            SimpleIoc.Default.Register<ClearViewModel>();
            SimpleIoc.Default.Register<AutoCollectViewModel>();
            SimpleIoc.Default.Register<ParameterViewModel>();
        }

        #region 实例化
        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() =>
           Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();

        #region 系统管理
        public UserViewModel User => ServiceLocator.Current.GetInstance<UserViewModel>();
        public RoleViewModel Role => ServiceLocator.Current.GetInstance<RoleViewModel>();
        public ConfigViewModel Config => ServiceLocator.Current.GetInstance<ConfigViewModel>();
        public PermissionViewModel Permission => ServiceLocator.Current.GetInstance<PermissionViewModel>();
        public MenuViewModel Menu => ServiceLocator.Current.GetInstance<MenuViewModel>();

        #endregion

        #region 采集管理

        public OrgViewModel Org => ServiceLocator.Current.GetInstance<OrgViewModel>();
        public RegisterViewModel Register => ServiceLocator.Current.GetInstance<RegisterViewModel>();
        public BattchViewModel Battch => ServiceLocator.Current.GetInstance<BattchViewModel>();
        public BackupViewModel Backup => ServiceLocator.Current.GetInstance<BackupViewModel>();
        public ExportViewModel Export => ServiceLocator.Current.GetInstance<ExportViewModel>();

        public InterfaceViewModel Interface => ServiceLocator.Current.GetInstance<InterfaceViewModel>();
        public ClearViewModel Clear => ServiceLocator.Current.GetInstance<ClearViewModel>();
        public AutoCollectViewModel AutoCollect => ServiceLocator.Current.GetInstance<AutoCollectViewModel>();

        public ParameterViewModel Parameter => ServiceLocator.Current.GetInstance<ParameterViewModel>();

        #endregion

        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
