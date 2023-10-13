using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class CommissionController
    {
        private static HttpClient _httpClient;
        public CommissionController(Config config)
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                MaxConnectionsPerServer = 20 // 특정 도메인에 대한 최대 동시 커넥션 수를 20으로 설정
            })
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }
        
        private class CommissionID
        {
            public async Task<JArray> GetCommissionIDReceiver(bool is_alliance, string order_type, int franchisesID, string commissionName)
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
            
        }

        private class CommissionContract
        {
            public async Task<JObject> AddCommissionContractReceiver(Models.CommissionContract commissionContract)
            {
                var Endpoint = "/commission-contract/";

                string jsonString = JsonConvert.SerializeObject(commissionContract);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Endpoint)
                {
                    Content = jsonContent
                })
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);

                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                throw new Exception("fail");
                            }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"HTTP request error: {ex.Message}");
                        throw ex;
                    }
            }
        }
        

        public async Task<int> GetCommissionID(bool is_alliance, string order_type, int franchisesID, string commissionName)
        {
            JArray GetCommissionIDObj = await new CommissionID().GetCommissionIDReceiver(is_alliance, order_type, franchisesID, commissionName);
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

        public async Task<int> AddCommissionContract(int vendor_id, int billingTypeID,
            int commissionID, bool is_alliance, string franchiseType, string start_date)
        {
            Models.CommissionContract contract = new Models.CommissionContract()
            {
                vendor = vendor_id,
                commission = commissionID,
                billing_entity_info = billingTypeID,
                is_alliance = is_alliance,
                franchise_type = franchiseType,
                commission_start_date = start_date
            };

            JObject GetCommissionIDObj = await new CommissionContract().AddCommissionContractReceiver(contract);
            if (GetCommissionIDObj != null)
            {
                return (int)GetCommissionIDObj["id"];
            }
            else
            {
                return 0;
            }
        }
    }
}
