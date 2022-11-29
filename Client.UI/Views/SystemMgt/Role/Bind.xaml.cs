using GZKL.Client.UI.Common;
using GZKL.Client.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = HandyControl.Controls.MessageBox;

namespace GZKL.Client.UI.Views.SystemMgt.Role
{
    /// <summary>
    /// Bind.xaml 的交互逻辑
    /// </summary>
    public partial class Bind : Window
    {

        private List<MenuModel> MenuModels;
        private List<MenuDataModel> MenuDataModels;
        public Bind(RoleModel roleModel, List<MenuModel> menuModels, List<MenuDataModel> menuDataModels)
        {
            InitializeComponent();

            this.MenuModels = menuModels;
            this.MenuDataModels = menuDataModels;

            this.DataContext = new { Model = roleModel, MenuDataModel = menuDataModels };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            CheckBox chkBox = (CheckBox)sender;
            var index = Convert.ToInt64(chkBox.Tag);

            //var menuDataModels = this.tvMenu.Items.Cast<MenuDataModel>().ToList();
            var menuDataModels = this.MenuDataModels;

            var selectedNode = FindNode(menuDataModels, index);

            //子节点
            if (selectedNode?.DataList?.Count > 0)
            {
                IsCheckedBox(selectedNode.DataList, Convert.ToBoolean(chkBox.IsChecked));
            }

            //父节点
            var parentNodes = FindParentNode(index);
            if (parentNodes?.Count > 0)
            {
                IsParentCheckedBox(this.MenuDataModels,parentNodes);
            }
        }

        private List<long> FindParentNode(long index)
        {
            var result = new List<long>();

            var parentNode = MenuModels.FirstOrDefault(w => w.Id == index);
            if (parentNode != null)
            {
                result.Add(parentNode.ParentId);
                if (parentNode.ParentId != 0)
                {
                    result.AddRange(FindParentNode(parentNode.ParentId));
                }
            }

            return result;
        }

        /// <summary>
        /// 更改父节点状态
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parentIds"></param>
        private void IsParentCheckedBox(List<MenuDataModel> nodes, List<long> parentIds)
        {
            foreach (MenuDataModel item in nodes)
            {
                if (parentIds.Contains(item.Index))
                {
                    parentIds.Remove(item.Index);
                    item.IsSelected = true;

                    if (parentIds.Count > 0 && item.DataList?.Count > 0)
                    {
                        IsParentCheckedBox(item.DataList.ToList(), parentIds);
                    }
                }
                continue;
            }
        }

        /// <summary>
        /// 查找子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private MenuDataModel FindNode(List<MenuDataModel> nodes, long index)
        {
            var node = new MenuDataModel();

            foreach (var item in nodes)
            {
                if (item.Index != index)
                {
                    node = FindNode(item.DataList.ToList(), index);
                }
                else
                {
                    node = item;
                }

                if (node.Index == index)
                {
                    return node;
                }
            }

            return node;
        }

        /// <summary>
        /// 更改子节点状态
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="flag"></param>
        private void IsCheckedBox(ObservableCollection<MenuDataModel> menus, bool flag)
        {
            foreach (MenuDataModel item in menus)
            {
                item.IsSelected = flag;
                if (item.DataList.Count > 0)
                {
                    IsCheckedBox(item.DataList, flag);
                }
            }
        }
    }
}
