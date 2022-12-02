using GalaSoft.MvvmLight.Messaging;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.ViewsModels;
using GZKL.Client.UI.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace GZKL.Client.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(LoginSuccessModel loginSuccessModel)
        {
            InitializeComponent();

            this.DataContext = new MainViewModel(loginSuccessModel);

            Messenger.Default.Register<string>(this, "ExpandMenu", arg =>
            {
                if (this.menu.Width < 200)
                {
                    this.xpUserInfo.Visibility = Visibility.Visible;
                    AnimationHelper.CreateWidthChangedAnimation(this.menu, 60, 200, new TimeSpan(0, 0, 0, 0, 300));
                }
                else
                {
                    this.xpUserInfo.Visibility = Visibility.Collapsed;
                    AnimationHelper.CreateWidthChangedAnimation(this.menu, 200, 60, new TimeSpan(0, 0, 0, 0, 300));
                }

                //由于...
                var template = this.IC.ItemTemplateSelector;
                this.IC.ItemTemplateSelector = null;
                this.IC.ItemTemplateSelector = template;              
            });
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void MinWin_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxWin_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWin_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Grid gridtemp = (Grid) btn.Template.FindName("gridtemp",btn);
            Popup menuPop = (Popup)gridtemp.FindName("menuPop");
            menuPop.IsOpen = true;
        }
    }
}
