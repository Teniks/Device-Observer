using Device_Observer.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Device_Observer.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditView.xaml
    /// </summary>
    public partial class AddEditView : Page
    {
        string nameTable = "";
        public static object element;

        public static EventHandler GoBack;

        public void onGoBack()
        {
            if(GoBack != null)
            {
                GoBack(this, EventArgs.Empty);
            }
        }

        public AddEditView(string nameTable, object selected = null)
        {
            InitializeComponent();
            this.nameTable = nameTable;

            switch (nameTable)
            {
                case "Ответственные":
                    thirdBlock.Visibility = System.Windows.Visibility.Collapsed;
                    thirdBox.Visibility = System.Windows.Visibility.Collapsed;

                    firstBlock.Text = "ФИО";
                    secondBlock.Text = "Должность";

                    if(selected != null)
                    {
                        Users user = selected as Users;

                        firstBox.Text = user.FullNameUser;
                        secondBox.Text = user.RoleUser;
                        element = user;
                    }
                    else
                    {
                        element = new Users();
                    }
                    break;
                case "Ресурсы":
                    firstBlock.Text = "Название ресурса";
                    secondBlock.Text = "Тип ресурса";
                    thirdBlock.Text = "Описание";

                    if (selected != null)
                    {
                        Resources resource = selected as Resources;

                        firstBox.Text = resource.NameResource;
                        secondBox.Text = resource.TypeResource;
                        thirdBox.Text = resource.DescriptionResource;

                        element = resource;
                    }
                    else
                    {
                        element = new Resources();
                    }
                    break;
                case "Права доступа":
                    firstBlock.Text = "Пользователь";
                    secondBlock.Text = "Ресурс";
                    thirdBlock.Text = "Описание возможностей";

                    if (selected != null)
                    {
                        AccessRights accessRights = selected as AccessRights;

                        firstBox.Text = accessRights.IdUser.ToString();
                        secondBox.Text = accessRights.IdResource.ToString();
                        thirdBox.Text = accessRights.Permission;

                        element = accessRights;
                    }
                    else
                    {
                        element = new AccessRights();
                    }
                    break;
                case "Пользователи":
                    thirdBlock.Visibility = System.Windows.Visibility.Collapsed;
                    thirdBox.Visibility = System.Windows.Visibility.Collapsed;

                    firstBlock.Text = "Роль (User/Admin)";
                    secondBlock.Text = "Детали о пользователе";

                    if (selected != null)
                    {
                        UsersAU usersAU = selected as UsersAU;

                        firstBox.Text = usersAU.RoleUser.ToString();
                        secondBox.Text = usersAU.Details.ToString();

                        element = usersAU;
                    }
                    else
                    {
                        element = new UsersAU();
                    }
                    break;
            }
        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isValid = true;
            try
            {
                switch (nameTable)
                {
                    case "Ответственные":
                        Users user = new Users
                        {
                            FullNameUser = firstBox.Text.Trim(),
                            RoleUser = secondBox.Text.Trim()
                        };
                        element = user;

                        if (firstBox.Text.Trim() == "" && secondBox.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Вы оставили пустые значения");
                        }
                        break;
                    case "Ресурсы":
                        Resources resource = new Resources
                        {
                            NameResource = firstBox.Text.Trim(),
                            TypeResource = secondBox.Text.Trim(),
                            DescriptionResource = thirdBox.Text.Trim()
                        };
                        element = resource;


                        if (firstBox.Text.Trim() == "" && secondBox.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Вы оставили пустые значения");
                        }
                        break;
                    case "Права доступа":
                        AccessRights accessRights = null;
                        
                        try
                        {
                            accessRights = new AccessRights
                            {
                                IdUser = ApplicationContext.Instance.Users.FirstOrDefault(x => x.FullNameUser.Equals(firstBox.Text.Trim())).IdUser,
                                IdResource = ApplicationContext.Instance.Resources.FirstOrDefault(x => x.NameResource.Equals(secondBox.Text.Trim())).IdResource,
                                Permission = thirdBox.Text.Trim()
                            };
                        }
                        catch
                        {
                            isValid = false;
                            MessageBox.Show("Не удалось найти необходимую запись");
                        }

                        if (accessRights != null)
                        {
                            element = accessRights;
                        }

                        if (firstBox.Text.Trim() == "" && secondBox.Text.Trim() == "" && thirdBox.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Вы оставили пустые значения");
                        }
                        break;
                    case "Пользователи":
                        UsersAU userAU = new UsersAU
                        {
                            RoleUser = firstBox.Text.Trim()
                        };
                        element = userAU;

                        if (firstBox.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Вы оставили пустые значения");
                        }
                        break;
                }

                if (isValid)
                {
                    onGoBack();
                    NavigationService.GoBack();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось найти элементы. Вы уверены в введенных данных?");
            }
        }

        private void GoBackBtn_Click(object sender, System.Windows.RoutedEventArgs e)
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
