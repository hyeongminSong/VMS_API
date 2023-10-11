using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class GetVendorBillingEntitiesContoller
    {
        private readonly HttpClient _httpClient;
        public GetVendorBillingEntitiesContoller(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private async Task<JArray> GetVendorBillingEntitiesReceiver(int vendorID)
        {
            var Endpoint = string.Format("/vendor/{0}/billing-entities/", vendorID);
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
            {
                try
                {
                    Console.WriteLine(requestMessage);
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
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON parsing error: {ex.Message}");
                    // 예외 처리를 위한 로직 추가
                    return null;
                }

            }
        }

        // 정산주체 ID 추출
        public async Task<int> GetVendorBillingEntities(int vendorID, string type)
        {
            JArray GetVendorBillingEntitiesObj = await GetVendorBillingEntitiesReceiver(vendorID);

            JToken targetItem = GetVendorBillingEntitiesObj.
                            FirstOrDefault(item => (string)item["billing_entity_type"] == type);

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
