using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    internal class OnboardingTaskDownloadController
    { 
        private readonly HttpClient _httpClient;

        public OnboardingTaskDownloadController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        public async Task<string> GetTaskCSVFile(string outFilePath)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "status", "ready,in_progress,pending" },
            { "type_depth1", "onboarding_yogiyo" },
            { "assigned_organization_name", "CXI 본부>컨텐츠관리실>" },
            // Add more parameters as needed
        };
            var vendordownloadEndpoint = "/tickets/download/"; // Ticket Download EndPoint

            string queryString = QueryStringBuilder.BuildQueryString(parameters);
            vendordownloadEndpoint += queryString;

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, vendordownloadEndpoint))
            {
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        using (var fileStream = File.Create(outFilePath))
                        {
                            await contentStream.CopyToAsync(fileStream);
                        }
                        return outFilePath;

                    }
                    else
                    {
                        Console.WriteLine($"Response status: {response.StatusCode}");
                        return null;
                    }
                }
            }
        }
    }
}
