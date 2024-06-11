using Device_Observer.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Device_Observer.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Page
    {
        AuthorizationVM authorizationVM;
        public RegistrationView()
        {
            InitializeComponent();
            authorizationVM = new AuthorizationVM();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (authorizationVM.Registration(LoginBox.Text, PasswordBox.Text, DetailsBox.Text) && authorizationVM.Role != null)
            {
                MessageBox.Show("Успешно зарегистрирован!");
                try
                {
                    NavigationService.GoBack();
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("Регистрация не удалась!");
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch
            {

            }
        }
    }
}
