using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Device_Observer.Models
{
    public class GitHubClient
    {
        private const string GitHubApiUrl = "https://api.github.com/repos/{your_repo_owner}/{your_repo_name}/releases/latest";
        private const string token = "";
        private const string productInformation = "";

        public async Task <ReleaseInfo> GetLatestRelease()
        {
            using (var client = new HttpClient())
            {
                // Bearer or token
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
                string enURL = GitHubApiUrl.Replace("{your_repo_owner}", "Teniks").Replace("{your_repo_name}", "Device-Observer");
                
                Console.WriteLine(enURL);
                var response = await client.GetAsync(enURL);

                Console.WriteLine(response.StatusCode);
                Console.WriteLine(client.DefaultRequestHeaders);
                Console.WriteLine(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("if (response.IsSuccessStatusCode) пройден");

                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReleaseInfo>(json);
                }
                else
                {

                    Console.WriteLine("if (response.IsSuccessStatusCode) else");
                    // Обработка ошибок
                    return null;
                }
            }
        }
    }

    public class ReleaseInfo
    {
        public string TagName { get; set; }
        public string Body { get; set; }
        public string HtmlURL { get; set; }
        public Asset[] Assets { get; set; }
    }

    // Класс для представления информации об ассете
    public class Asset
    {
        public string Name { get; set; }
        public string BrowserDownloadUrl { get; set; }
    }
}

