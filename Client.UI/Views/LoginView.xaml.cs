using GalaSoft.MvvmLight.Messaging;
using GZKL.Client.UI.Common;
using GZKL.Client.UI.ViewsModels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GZKL.Client.UI.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "UserNameErrorToken", UserNameErrorToken);

            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);


            var viewModel = this.DataContext as LoginViewModel;
            var loginModel = viewModel.GetLoginSetting();

            if (loginModel.RememberPassword)
            {
                viewModel.UserName = loginModel.UserName;
                viewModel.Password = loginModel.Password;

                this.PassWordStr.Password= loginModel.Password;
            }

            viewModel.AutoLogin = loginModel.AutoLogin;
            viewModel.RememberPassword = loginModel.RememberPassword;

            if (loginModel.AutoLogin)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    this.txtLoginError.Foreground = Brushes.Green;
                    viewModel.LoginError = "自动登录中...";

                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromSeconds(5)
                    };
                    timer.Tick += (sender, e) =>
                    {
                        viewModel.LoginMethod(this);
                    };
                   
                    timer.Start();
                });
            }
        }

        private void UserNameErrorToken(string msg)
        {  
            this.UserNameStr.IsError = true;
            this.UserNameStr.ErrorStr = msg;
        }

        private void LoginWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWin_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as LoginViewModel;

            var psdControl = this.PassWordStr;
            var userName = this.UserNameStr.Text;
            var psdStr = psdControl.Password;

            if (string.IsNullOrEmpty(userName))
            {
                Messenger.Default.Send("登录名不能为空", "UserNameErrorToken");
                return;
            }
            if (string.IsNullOrEmpty(psdStr))
            {
                psdControl.IsError = true;
                psdControl.ErrorStr = "密码不能为空";
                return;
            }
            else
            {
                viewModel.Password = psdStr;
            }

            viewModel.LoginMethod(this);
        }
    }
}
