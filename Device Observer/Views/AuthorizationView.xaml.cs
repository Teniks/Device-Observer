using Device_Observer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Device_Observer.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Page
    {
        AuthorizationVM authorizationVM;
        public AuthorizationView()
        {
            InitializeComponent();
            authorizationVM = new AuthorizationVM();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(authorizationVM.Authorization(LoginBox.Text, PasswordBox.Text) && authorizationVM.Role != null)
            {
                NavigationService.Navigate(new DevicesListView());
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
        }
    }
}
