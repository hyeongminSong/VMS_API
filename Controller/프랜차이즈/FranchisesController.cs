using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class FranchisesController
    {
        private readonly FranchisesID _franchisesID = new FranchisesID();
        private static HttpClient _httpClient;
        public FranchisesController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }
        private class FranchisesID
        {
            public async Task<JObject> GetFranchisesIDReceiver(bool is_group, string franchisesName)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "is_group", is_group.ToString() },
            { "name", franchisesName }
            // Add more parameters as needed
        };
                var Endpoint = "/franchises/";
                string queryString = QueryStringBuilder.BuildQueryString(parameters);
                Endpoint += queryString;

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);

                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }
        public async Task<JObject> GetFranchisesID(bool is_group, string franchisesName)
        {
            JObject GetFranchisesIDObj = await _franchisesID.GetFranchisesIDReceiver(is_group, franchisesName);
            return GetFranchisesIDObj;
            /*JToken targetItem = GetFranchisesIDObj["results"].
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }
    }
}
