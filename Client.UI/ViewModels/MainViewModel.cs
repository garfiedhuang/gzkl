using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GZKL.Client.UI.API;
using GZKL.Client.UI.Models;
using GZKL.Client.UI.Common;
using MessageBox = HandyControl.Controls.MessageBox;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.IO;

namespace GZKL.Client.UI.ViewsModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="loginSuccessModel"></param>
        public MainViewModel(LoginSuccessModel loginSuccessModel)
        {
            ModuleGroups = new ObservableCollection<ModuleGroupModel>();
            TabModels = new ObservableCollection<MenuTabModel>();

            ChangeContentCommand = new RelayCommand<object>(NavChanged);
            ExpandMenuCommand = new RelayCommand(() =>
            {
                for (int i = 0; i < ModuleGroups.Count; i++)
                {
                    var item = ModuleGroups[i];
                    item.ContractionTemplate = !item.ContractionTemplate;
                }
                Messenger.Default.Send("", "ExpandMenu");
            });

            //Ĭ��չ����ҳ
            NavChanged("Home");

            //��ʼ������
            InitData(loginSuccessModel);
        }

        #region =====data

        /// <summary>
        /// ҳǩ����
        /// </summary>
        private int tabIndex = 0;
        public int TabIndex
        {
            get { return tabIndex; }
            set { tabIndex = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// ģ�����
        /// </summary>
        private FrameworkElement mainContent;
        public FrameworkElement MainContent
        {
            get { return mainContent; }
            set { mainContent = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// �Ѽ���ģ��-����
        /// </summary>
        private ObservableCollection<ModuleGroupModel> moduleGroups;
        public ObservableCollection<ModuleGroupModel> ModuleGroups
        {
            get { return moduleGroups; }
            set { moduleGroups = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// �ѵ��ģ��
        /// </summary>
        private ObservableCollection<MenuTabModel> tabModels;
        public ObservableCollection<MenuTabModel> TabModels
        {
            get { return tabModels; }
            set { tabModels = value; RaisePropertyChanged(); }
        }

        public string UserName { get; set; } 
        public string RoleName { get; set; }

        #endregion

        #region ====cmd
        public RelayCommand<object> ChangeContentCommand { get; set; }
        public RelayCommand ExpandMenuCommand { get; set; }

        #endregion

        /// <summary>
        /// ҳǩ�ı��¼�
        /// </summary>
        /// <param name="obj"></param>
        private void NavChanged(object obj)
        {
            string typeName;
            string tabName;

            if (obj.ToString() == "Home")
            {
                typeName = "Home";
                tabName = "��ҳ";
            }
            else
            {
                var values = (object[])obj;
                typeName = values[0].ToString();
                tabName = values[1].ToString();
            }

            var type = Type.GetType("GZKL.Client.UI.Views." + typeName);

            if (type == null)
            {
                MessageBox.Show($"��ǰ���ܡ�{tabName}��δʵ�֣����������������ϵ", "������ʾ");
                return;
            }

            //��ȡ�չ��캯��
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var needAdd = true;

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
                var menuTabModel = new MenuTabModel
                {
                    Header = tabName,
                    Code = typeName,
                    Content = fe
                };

                TabModels.Add(menuTabModel);
                TabIndex = TabModels.Count - 1;
            }

            this.MainContent = fe;
        }

        /// <summary>
        /// ��ȡ�˵�����
        /// </summary>
        /// <param name="menuModels"></param>
        private void GetMenuData(List<MenuModel> menuModels)
        {
            var menuApi = new MenuApi();
            Task.Run(new Action(() =>
            {
                ModuleGroups.Clear();

                var menus = menuApi.GetModuleGroups(menuModels);
                foreach (var item in menus)
                {
                    ModuleGroups.Add(item);
                }
            }));
        }

        /// <summary>
        /// ��ȡ���Ժ�ע������
        /// </summary>
        private void GetPCAndRegisterData()
        {
            Task.Run(new Action(() =>
            {
                SessionInfo.Instance.ComputerInfo = ComputerInfo.GetInstance().ReadComputerInfo();

                var fullName = $"{SessionInfo.Instance.ComputerInfo.HostName}-{SessionInfo.Instance.ComputerInfo.CPU}";

                SessionInfo.Instance.RegisterInfo = RegisterInfo.GetInstance().GetRegisterInfo(fullName);
            }));
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="loginSuccessModel"></param>
        private void InitData(LoginSuccessModel loginSuccessModel)
        {
            try
            {
                SessionInfo.Instance.UserInfo = loginSuccessModel.User;

                GetMenuData(loginSuccessModel.Menus);//�첽

                GetPCAndRegisterData();//�첽

                this.UserName = loginSuccessModel?.User?.Name;
                this.RoleName = loginSuccessModel?.Role?.Name;
            }
            catch
            {

            }
        }
    }
}