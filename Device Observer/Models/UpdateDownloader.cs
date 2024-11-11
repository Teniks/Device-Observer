using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Device_Observer.Models
{
    internal class UpdateDownloader
    {
        public async Task DownloadUpdate(ReleaseInfo releaseInfo, string downloadPath)
        {
            // Получаем ссылку на скачиваемый файл
            var downloadUrl = releaseInfo.Assets.FirstOrDefault(asset => asset.Name.EndsWith(".zip"))?.BrowserDownloadUrl;

            if (!string.IsNullOrEmpty(downloadUrl))
            {
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(downloadUrl, Path.Combine(downloadPath, releaseInfo.TagName + ".zip"));
                }
            }
        }
    }
}
