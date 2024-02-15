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
        private readonly CommissionID _commissionID = new CommissionID();
        private readonly CommissionContract _commissionContract = new CommissionContract();
        private static HttpClient _httpClient;
        public CommissionController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private class CommissionID
        {
            //이용료정보 조회
            public async Task<JArray> GetCommissionIDReceiver(bool is_alliance, string order_type, int franchisesID, string commissionName)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    {"is_alliance", is_alliance.ToString() },
                    { "order_type", order_type },
                    { "franchise_id", franchisesID.ToString() },
                    { "type", order_type },
                    { "name", commissionName }
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

        private class CommissionContract
        {
            //이용료계약정보 등록
            public async Task<JObject> AddCommissionContractReceiver(Models.CommissionContract commissionContract)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(commissionContract, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/commission-contract/";

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
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
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


        public async Task<JArray> GetCommissionID(bool is_alliance, string order_type, int franchisesID, string commissionName)
        {
            JArray GetCommissionIDObj = await _commissionID.GetCommissionIDReceiver(is_alliance, order_type, franchisesID, commissionName);
            return GetCommissionIDObj;
            /*JToken targetItem = GetCommissionIDObj.
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(commissionName));

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }

        public async Task<JObject> AddCommissionContract(Models.CommissionContract commissionContractObj)
        {
            JObject GetCommissionIDObj = await _commissionContract.AddCommissionContractReceiver(commissionContractObj);
            return GetCommissionIDObj;
        }
    }
}
