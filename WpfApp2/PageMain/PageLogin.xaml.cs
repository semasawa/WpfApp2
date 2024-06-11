using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp2.AppData;
using WpfApp2.PageAdmin;

namespace WpfApp2.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userObj = AppConnect.model.User.FirstOrDefault(x => x.Login == TxbLogin.Text && x.Password == PsbPassword.Text);
            if (userObj == null)
            {
                MessageBox.Show("Данные пользователя не найдены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                //AppFrame.FrameMain.Navigate(new PageMenuAdmin());
            }
            try
            {
                switch (userObj.RoleID)
                {
                    case 1: MessageBox.Show("Добро пожаловать, администратор!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.FrameMain.Navigate(new PageMenuAdmin());
                        break;
                    case 2:
                        MessageBox.Show("Добро пожаловать, пользователь!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                        //AppFrame.FrameMain.Navigate(new PageMenuAdmin());
                        break;
                    default:
                        MessageBox.Show("Ошибка авторизации!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        //AppFrame.FrameMain.Navigate(new PageMenuAdmin());
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
