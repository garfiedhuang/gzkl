using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;

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

    }
}
