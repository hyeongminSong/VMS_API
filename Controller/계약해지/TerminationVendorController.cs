using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Controller.CompanyController;

namespace ConsoleApp1.Controller
{
    public class TerminationVendorController
    {
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
        }

        public async Task<int> GetTerminationReasonID(string terminationReason)
        {
            JObject GetTerminationReasonObj = await new Termination().GetTerminationReasonIDReceiver();
            JToken targetItem = GetTerminationReasonObj["results"].
                FirstOrDefault(item => (string)item["reason"] == terminationReason);

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
        }

        public async Task CreateTerminationVendor(int companyID, string companyNumber, int vendorID,
            int terminationReasonID, string terminationDate)
        {
            BulkCreateTerminationVendor bulkCreateTerminationVendor = new BulkCreateTerminationVendor()
            {
                company_id = companyID,
                company_number = companyNumber,
                vendor_ids = new int[]{ vendorID },
                termination = new termination()
                {
                    reason = terminationReasonID,
                    termination_date = terminationDate
                }
            };
            await new Termination().CreateTerminationVendorReceiver(bulkCreateTerminationVendor);

        }
    }
}
