using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using System.Net.Http;
using System.Security.Policy;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ConsoleApp1
{
    internal class VendorDownloadController
    { 
        private readonly HttpClient _httpClient;

        public VendorDownloadController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        public async Task<string> GetVerdorCSVFile(string outFilePath)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "contract_status", "registration_in_progress" },
            { "latest_audit_status", "sales_approved" },
            // Add more parameters as needed
        };
            var vendordownloadEndpoint = "/vendor/download/"; // Vendor Download EndPoint

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
