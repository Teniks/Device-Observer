using Device_Observer.data;
using System;
using System.Threading.Tasks;

namespace Device_Observer.Models
{
    public class UpdateChecker
    {
        private GitHubClient _gitHubClient;

        public UpdateChecker(GitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<bool> CheckForUpdates()
        {
            Console.WriteLine("CheckForUpdates запущен");
            // Получаем информацию о последней версии
            var latestRelease = await _gitHubClient.GetLatestRelease();

            // Проверяем, доступна ли новая версия
            if (latestRelease != null )
            {
                Console.WriteLine("if (latestRelease != null ) пройден");
                if (latestRelease.TagName != data.Version.VERSION_TAG)
                {
                    Console.WriteLine("if (latestRelease.TagName != data.Version.VERSION_TAG) запущен");
                    // Обработка наличия обновлений, например, показать диалоговое окно
                    return true;
                }
            }
            return false;
        }
    }
}
