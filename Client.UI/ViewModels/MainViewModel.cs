using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using GZKL.Client.UI.API;
using GZKL.Client.UI.Models;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.ViewsModels
{
    public class MainViewModel : ViewModelBase
    {
        MenuApi api = new MenuApi();
        
        public MainViewModel()
        {
            ModuleGroups = new ObservableCollection<ModuleGroupModel>();
            TabModels = new ObservableCollection<MenuTabModel>();
            tabIndex = 0;
            ChangeContentCommand = new RelayCommand<object>(NavChanged);
            ExpandMenuCommand = new RelayCommand(()=> {
                for (int i = 0; i < ModuleGroups.Count; i++)
                {
                    var item = ModuleGroups[i];
                    item.ContractionTemplate = !item.ContractionTemplate;
                }
                Messenger.Default.Send("", "ExpandMenu");
            });
            GetMenu();

            NavChanged("Home");
        }
        #region =====data
        private ObservableCollection<ModuleGroupModel> moduleGroups;
        private ObservableCollection<MenuTabModel> tabModels;
        private int tabIndex;
        public int TabIndex
        {
            get { return tabIndex; }
            set { tabIndex = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 模块加载
        /// </summary>
        private FrameworkElement mainContent;
        public FrameworkElement MainContent
        {
            get { return mainContent; }
            set { mainContent = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<UserModel> GridModelList { get; set; }
        /// <summary>
        /// 已加载模块-分组
        /// </summary>
        public ObservableCollection<ModuleGroupModel> ModuleGroups
        {
            get { return moduleGroups; }
            set { moduleGroups = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 已点击模块
        /// </summary>
        public ObservableCollection<MenuTabModel> TabModels
        {
            get { return tabModels; }
            set { tabModels = value; RaisePropertyChanged(); }
        }
        #endregion

        #region ====cmd
        public RelayCommand<object> ChangeContentCommand { get; set; }
        public RelayCommand ExpandMenuCommand { get; set; }

        #endregion

        private void NavChanged(object o)
        {
            string typeName;
            string tabName;
            if (o.ToString()=="Home")
            {
                typeName = "Home";
                tabName = "首页";
            }
            else
            {
                var values = (object[])o;
                typeName = values[0].ToString();
                tabName = values[1].ToString();
            }
           
            Type type = Type.GetType("GZKL.Client.UI.Views." + typeName);

            if (type == null)
            {
                MessageBox.Show($"当前功能【{tabName}】未实现，请与系统管理员联系", "操作提示");
                return;
            }

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            bool needAdd = true;
            for (int i = 0; i < TabModels.Count; i++)
            {
                if (TabModels[i].Code == typeName)
                {
                    TabIndex = i;
                    needAdd = false;
                    break;
                }
            }

            var fe = (FrameworkElement)constructor.Invoke(null);
            if (needAdd)
            {
                MenuTabModel menuTabModel = new MenuTabModel();
                menuTabModel.Header = tabName;
                menuTabModel.Code= typeName;

                //menuTabModel.Content = (FrameworkElement)constructor.Invoke(null);//Todo:暂无用处 mark by garfield 20221109
                menuTabModel.Content= fe;
                TabModels.Add(menuTabModel);
                TabIndex = TabModels.Count - 1;
            }
            this.MainContent = fe;
        }
        private void GetMenu()
        {
            MenuApi mApi = new MenuApi();
            Task.Run(new Action(()=> {

                ModuleGroups.Clear();

                var menus = mApi.GetModuleGroups();
                foreach (var item in menus)
                {                  
                    ModuleGroups.Add(item);
                }
            }));
        }
    }
}