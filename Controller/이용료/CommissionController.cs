using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class CommissionController
    {
        private readonly Commission _commission = new Commission();
        private readonly CommissionContract _commissionContract = new CommissionContract();
        private readonly ZeroCommission _zeroCommission = new ZeroCommission();
        private readonly ZeroCommissionContract _zeroCommissionContract = new ZeroCommissionContract();

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

        //이용료 정보
        private class Commission
        {
            //이용료정보 조회
            public async Task<JArray> GetCommissionReceiver(string order_type, int? franchisesID, string commissionName)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "order_type", order_type },
                    { "franchise_id", franchisesID },
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
        //이용료계약정보
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

        //월정액 이용료정보
        private class ZeroCommission
        {
            //이용료계약정보 등록
            public async Task<JArray> GetZeroCommissionReceiver()
            {
                var Endpoint = "/zero-commission/";

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
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
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
            }
        }
        //월정액 이용료계약정보
        private class ZeroCommissionContract
        {
            //이용료계약정보 등록
            public async Task<JObject> AddZeroCommissionContractReceiver(Models.ZeroCommissionContract zeroCommissionContract)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(zeroCommissionContract, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/zero-commission-contract/";

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


        public async Task<JArray> GetCommission(string order_type, int? franchisesID, string commissionName)
        {
            JArray getCommissionObj = await _commission.GetCommissionReceiver(order_type, franchisesID, commissionName);
            return getCommissionObj;
        }

        public async Task<JObject> AddCommissionContract(Models.CommissionContract commissionContract)
        {
            JObject addCommissionContractObj = await _commissionContract.AddCommissionContractReceiver(commissionContract);
            return addCommissionContractObj;
        }

        public async Task<JArray> GetZeroCommission()
        {
            JArray getZeroCommissionArr = await _zeroCommission.GetZeroCommissionReceiver();
            return getZeroCommissionArr;
        }

        public async Task<JObject> AddZeroCommissionContract(Models.ZeroCommissionContract zeroCommissionContract)
        {
            JObject addZeroCommissionContractObj = await _zeroCommissionContract.AddZeroCommissionContractReceiver(zeroCommissionContract);
            return addZeroCommissionContractObj;
        }
    }
}