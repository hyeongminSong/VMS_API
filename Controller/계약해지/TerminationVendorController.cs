using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class TerminationVendorController
    {
        private readonly Termination _termination = new Termination();
        private static HttpClient _httpClient;
        public TerminationVendorController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        public class Termination
        {
            //계약 사유 목록
            public async Task<JObject> GetTerminationReasonIDReceiver()
            {
                var Endpoint = string.Format("/termination-reason/");
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
            //벤더별 계약 해지 벌크 생성
            public async Task<JArray> CreateTerminationVendorReceiver(BulkCreateTerminationVendor bulkCreateTerminationVendor)
            {
                string jsonString = JsonConvert.SerializeObject(bulkCreateTerminationVendor);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/vendor-termination/bulk_create/";
                using (var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Endpoint)
                {
                    Content = jsonContent
                })
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

        public async Task<JObject> GetTerminationReasonID()
        {
            JObject GetTerminationReasonObj = await _termination.GetTerminationReasonIDReceiver();
            return GetTerminationReasonObj;
        }

        public async Task<JArray> CreateTerminationVendor(BulkCreateTerminationVendor bulkCreateTerminationVendor)
        {
            JArray CreateTerminationVendorObj = await _termination.CreateTerminationVendorReceiver(bulkCreateTerminationVendor);
            return CreateTerminationVendorObj;
        }
    }
}
