using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class GetCommissionIDController
    {
        private readonly HttpClient _httpClient;

        public GetCommissionIDController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private async Task<JArray> GetCommissionIDReceiver(bool is_alliance, string order_type, int franchisesID, string commissionName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            {"is_alliance", is_alliance.ToString() },
                { "order_type", order_type },
                { "franchise_id", franchisesID.ToString() },
                { "type", order_type },
                { "name", commissionName }
            // Add more parameters as needed
        };
            var Endpoint = "/commission/";
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
                            return JArray.Parse(contentString);

                        }
                        else
                        {
                            Console.WriteLine($"Response status: {response.StatusCode}");
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

        // 계약서 ID 추출
        public async Task<int> GetCommissionID(bool is_alliance, string order_type, int franchisesID, string commissionName)
        {
            JArray GetCommissionIDObj = await GetCommissionIDReceiver(is_alliance, order_type, franchisesID, commissionName);
            JToken targetItem = GetCommissionIDObj.
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(commissionName));

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
        }
    }
}
