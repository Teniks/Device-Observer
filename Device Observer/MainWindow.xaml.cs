using System;
using System.Windows;
using Device_Observer.Models;
using Device_Observer.Views;

namespace Device_Observer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UpdateChecker _updateChecker;
        private UpdateDownloader _updateDownloader;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация классов для проверки и скачивания обновлений
            _updateChecker = new UpdateChecker(new GitHubClient());
            _updateDownloader = new UpdateDownloader();

            CheckUpdates();

            MainFrame.Navigate(new AuthorizationView());
        }

        private async void CheckUpdates()
        {
            Console.WriteLine("CheckUpdates запущен");
            if (await _updateChecker.CheckForUpdates())
            {
                Console.WriteLine("if (await _updateChecker.CheckForUpdates()) пройден");
                // Покажите диалоговое окно с информацией о доступном обновлении
                // Запросите у пользователя подтверждение скачивания
                // Если пользователь подтвердил скачивание
                if (CustomMessageBox.Show("Доступно обновление программы!\nЖелаете загрузить?", true)) // Замените на проверку выбора пользователя
                {
                    Console.WriteLine("if (CustomMessageBox.Show(\"Доступно обновление программы!\\nЖелаете загрузить?\", true)) пройден");
                    ReleaseInfo latestRelease = (new GitHubClient()).GetLatestRelease().Result;
                    // Скачайте обновление
                    await _updateDownloader.DownloadUpdate(latestRelease, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

                    // Покажите сообщение об успешном скачивании
                    // ...
                    CustomMessageBox.Show("Успешно ");
                    // Предоставьте пользователю инструкцию по установке обновления
                    // ...
                }
            }
        }
    }
}
