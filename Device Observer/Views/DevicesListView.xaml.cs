using Device_Observer.Models;
using Device_Observer.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Device_Observer.Views
{
    /// <summary>
    /// Логика взаимодействия для DevicesListView.xaml
    /// </summary>
    public partial class DevicesListView : Page
    {
        UsersVM usersVM = new UsersVM();
        ResourcesVM resourcesVM = new ResourcesVM();
        AccessRightsVM accessRightsVM = new AccessRightsVM();

        AccessLogsVM accessLogsVM;
        ListOfChangesVM listOfChangesVM;

        UsersAUVM usersAUVM;

        string nameSelectedTable;

        public DevicesListView()
        {
            InitializeComponent();

            SearchBox.TextChanged += OnTextChanged;
            TableNamesList.SelectionChanged += ListSelectionChanged;
            TableNamesList.Items.Clear();
            TableNamesList.ItemsSource = new List<string> { "Ответственные", "Ресурсы", "Права доступа", "Количество типов ресурсов" };

            if (AUContext.Role != null && AUContext.Role.Equals("Admin"))
            {
                accessLogsVM = new AccessLogsVM();
                listOfChangesVM = new ListOfChangesVM();
                usersAUVM = new UsersAUVM();

                TableNamesList.ItemsSource = new List<string> { "Ответственные", "Ресурсы", "Права доступа", "Список изменений", "Логи доступа", "Пользователи", "Количество типов ресурсов" };

                BackupPanel.Visibility = Visibility.Visible;
                BackupBtn.Click += delegate (object sender, RoutedEventArgs e)
                {
                    string connectionStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Vtx\\source\\repos\\Device Observer\\Device Observer\\data\\db_local.mdf\";Integrated Security=True";


                    // Создаем диалог сохранения
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    // Настраиваем диалог
                    saveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.AddExtension = true;

                    // Показываем диалог и обрабатываем результат
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        try
                        {
                            BackupManager.BackupDatabase(connectionStr, "C:\\MyDatabaseBackup.bak");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                };

                RestoreBtn.Click += delegate (object sender, RoutedEventArgs e)
                {
                    string connectionStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Vtx\\source\\repos\\Device Observer\\Device Observer\\data\\db_local.mdf\";Integrated Security=True";

                    // Создаем диалог сохранения
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    // Настраиваем диалог
                    saveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = ".xlsx";
                    saveFileDialog.AddExtension = true;

                    // Показываем диалог и обрабатываем результат
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        try
                        {
                            BackupManager.RestoreDatabase(connectionStr, "C:\\MyDatabaseBackup.bak");
                        }catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                };
            }

            IsEnabledBtns(false);
        }

        public void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nameSelectedTable = TableNamesList.SelectedItem.ToString();
            IsEnabledBtns(false);
            ShowTable();
        }

        public void ShowTable()
        {
            DataTable.AutoGenerateColumns = false;
            DataTable.Columns.Clear();
            switch (nameSelectedTable)
            {
                case "Ответственные":
                    DataTable.ItemsSource = usersVM.users?.ToBindingList();


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullNameUser") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Роль/Должность", Binding = new Binding("RoleUser") });

                    IsEnabledBtns();
                    break;
                case "Ресурсы":
                    DataTable.ItemsSource = resourcesVM.resources?.ToBindingList();


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("NameResource") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Тип ресурса", Binding = new Binding("TypeResource") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Описание", Binding = new Binding("DescriptionResource") });

                    IsEnabledBtns();
                    break;
                case "Права доступа":
                    DataTable.ItemsSource = accessRightsVM.accessRights.Select(x => 
                    new { x.IdRight,
                        usersVM.users.FirstOrDefault(y => x.IdUser == y.IdUser)?.FullNameUser,
                        resourcesVM.resources.FirstOrDefault(z => x.IdResource == z.IdResource).NameResource,
                        x.Permission
                    });


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Пользователь", Binding = new Binding("FullNameUser") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Ресурс", Binding = new Binding("NameResource") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Возможности", Binding = new Binding("Permission") });

                    IsEnabledBtns();
                    break;
                case "Список изменений":
                    DataTable.ItemsSource = listOfChangesVM.listOfChanges?.ToBindingList();


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Дата", Binding = new Binding("DateChanges") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Детали", Binding = new Binding("Details") });
                    break;
                case "Логи доступа":
                    DataTable.ItemsSource = accessLogsVM.accessLogs?.ToBindingList();


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Время выдачи", Binding = new Binding("TimeStampLog") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Детали", Binding = new Binding("Details") });
                    break;
                case "Пользователи":
                    DataTable.ItemsSource = usersAUVM.users?.ToBindingList();

                    
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Детали", Binding = new Binding("Details") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Роль", Binding = new Binding("RoleUser") });

                    if(AUContext.Role.Equals("Admin"))
                        IsEnabledBtns();
                    break;
                case "Количество типов ресурсов":
                    DataTable.ItemsSource = null;
                    DataTable.Items.Clear();
                    DataTable.ItemsSource = resourcesVM.resources.GroupBy(g => g.TypeResource).Select(s => new 
                    {
                        Type = s.Key, 
                        Count = s.Count()  
                    }).ToList();


                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Тип", Binding = new Binding("Type") });
                    DataTable.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("Count") });
                    break;
            }
        }

        public void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable.ItemsSource = null;
            DataTable.Items.Clear();

            switch (nameSelectedTable)
            {
                case "Ответственные":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = usersVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = usersVM.FilterByString(SearchBox.Text);
                    break;
                case "Ресурсы":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = resourcesVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = resourcesVM.FilterByString(SearchBox.Text);
                    break;
                case "Права доступа":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = accessRightsVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = accessRightsVM.FilterByString(SearchBox.Text);
                    break;
                case "Список изменений":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = listOfChangesVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = listOfChangesVM.FilterByString(SearchBox.Text);
                    break;
                case "Логи доступа":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = accessLogsVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = accessLogsVM.FilterByString(SearchBox.Text);
                    break;
                case "Пользователи":
                    if (!IsFilter.IsChecked.Value)
                        DataTable.ItemsSource = usersAUVM.SearchByString(SearchBox.Text);
                    else
                        DataTable.ItemsSource = usersAUVM.FilterByString(SearchBox.Text);
                    break;
            }
        }

        public void UpdateTable()
        {
            DataTable.ItemsSource = null;
            DataTable.Items.Clear();
            switch (nameSelectedTable)
            {
                case "Ответственные":
                    usersVM.UpdateCollection();
                    break;
                case "Ресурсы":
                    resourcesVM.UpdateCollection();
                    break;
                case "Права доступа":
                    accessRightsVM.UpdateCollection();
                    break;
                case "Список изменений":
                    listOfChangesVM.UpdateCollection();
                    break;
                case "Логи доступа":
                    accessLogsVM.UpdateCollection();
                    break;
                case "Пользователи":
                    usersAUVM.UpdateCollection();
                    break;
            }
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (nameSelectedTable.Equals("Пользователи"))
            {
                NavigationService.Navigate(new RegistrationView());
            }
            else
            {
                NavigationService.Navigate(new AddEditView(nameSelectedTable));
                AddEditView.GoBack += SaveAdd;
            }
        }

        private void SaveAdd(object sender, EventArgs e)
        {
            switch (nameSelectedTable)
            {
                case "Ответственные":
                    Users user = AddEditView.element as Users;
                    if (user != null)
                    {
                        usersVM.AddCommand.Execute(user);
                    }
                    break;
                case "Ресурсы":
                    Resources resource = AddEditView.element as Resources;
                    if (resource != null)
                    {
                        resourcesVM.AddCommand.Execute(resource);
                    }
                    break;
                case "Права доступа":
                    AccessRights accessRights = AddEditView.element as AccessRights;
                    if (accessRights != null)
                    {
                        accessRightsVM.AddCommand.Execute(accessRights);
                    }
                    break;
            }
            UpdateTable();
            ShowTable();
            AddEditView.GoBack -= SaveAdd;
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(nameSelectedTable != null && DataTable.SelectedItem != null)
            {
                switch (nameSelectedTable)
                {
                    case "Ответственные":
                        Users user = DataTable.SelectedItem as Users;
                        usersVM.RemoveCommand.Execute(user);
                        break;
                    case "Ресурсы":
                        Resources resources = DataTable.SelectedItem as Resources;
                        resourcesVM.RemoveCommand.Execute(resources);
                        break;
                    case "Права доступа":
                        AccessRights accessRights = DataTable.SelectedItem as AccessRights;
                        accessRightsVM.RemoveCommand.Execute(accessRights);
                        break;
                    case "Список изменений":
                        ListOfChanges listOfChanges = DataTable.SelectedItem as ListOfChanges;
                        listOfChangesVM.RemoveCommand.Execute(listOfChanges);
                        break;
                    case "Логи доступа":
                        AccessLogs accessLogs = DataTable.SelectedItem as AccessLogs;
                        accessLogsVM.RemoveCommand.Execute(accessLogs);
                        break;
                    case "Пользователи":
                        UsersAU userAU = DataTable.SelectedItem as UsersAU;
                        usersAUVM.RemoveCommand.Execute(userAU);
                        break;
                }

                ApplicationContext.Instance.SaveChanges();
                UpdateTable();
                ShowTable();
            }
        }

        private void UpdateBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditView(nameSelectedTable, DataTable.SelectedItem));
            AddEditView.GoBack += SaveUpdate;
        }

        private void SaveUpdate(object sender, EventArgs e)
        {
            switch (nameSelectedTable)
            {
                case "Ответственные":
                    Users user = AddEditView.element as Users;
                    usersVM.UpdateCommand.Execute(user);
                    break;
                case "Ресурсы":
                    Resources resource = AddEditView.element as Resources;
                    resourcesVM.UpdateCommand.Execute(resource);
                    break;
                case "Права доступа":
                    AccessRights accessRights = AddEditView.element as AccessRights;
                    resourcesVM.UpdateCommand.Execute(accessRights);
                    break;
                case "Пользователи":
                    UsersAU userAU = AddEditView.element as UsersAU;
                    usersAUVM.UpdateCommand.Execute(userAU);
                    break;
            }

            UpdateTable();
            ShowTable();
            AddEditView.GoBack -= SaveUpdate;
        }

        public void IsEnabledBtns(bool isEnabled = true)
        {
            AddBtn.IsEnabled = isEnabled;
            RemoveBtn.IsEnabled = isEnabled;
            UpdateBtn.IsEnabled = isEnabled;
        }

        private void GoBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoBack();
            }
            catch
            {

            }
        }

        private void OptionsBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // Создаем диалог сохранения
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                // Настраиваем диалог
                saveFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = ".xlsx";
                saveFileDialog.AddExtension = true;

                // Показываем диалог и обрабатываем результат
                if (saveFileDialog.ShowDialog() == true)
                {
                    Export.ExportToExcel(DataTable, saveFileDialog.FileName);
                }
                //Сохраняем данные в CSV-файл
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
