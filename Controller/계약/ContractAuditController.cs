using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class ContractAuditController
    {
        private static HttpClient _httpClient;
        public ContractAuditController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private class ContractAudit
        {
            public async Task<JObject> CreateContractAuditReceiver(int vendorID, Models.ContractAudit contractAudit)
            {
                string jsonString = JsonConvert.SerializeObject(contractAudit);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/contract-audit/", vendorID);
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
            public async Task<JObject> UpdateContractAuditReceiver(int vendorID, int contractID, ContractAuditPatch contractAuditPatch)
            {
                string jsonString = JsonConvert.SerializeObject(contractAuditPatch);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/contract-audit/{1}/", vendorID, contractID);
                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
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
        }

        private class SalesApprove
        {
            public async Task<JObject> RequestSalesApproveReceiver(int vendorID, int contractID, ContractAuditSalesApprove contractSalesSalesApprove)
            {
                string jsonString = JsonConvert.SerializeObject(contractSalesSalesApprove);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/contract-audit/{1}/sales-approve/", vendorID, contractID);
                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
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
        }
        private class OwnerApprove
        {
            public async Task<JObject> RequestOwnerApproveReceiver(int vendorID, int contractID, ContractAuditOwnerRequest contractAuditOwnerRequest)
            {
                string jsonString = JsonConvert.SerializeObject(contractAuditOwnerRequest);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/contract-audit/{1}/request-owner-approve/", vendorID, contractID);
                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
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
            
        }
        public async Task<int> RequestSalesApprove(int vendorID, int contractID)
        {
            ContractAuditSalesApprove contractSalesSalesApprove = new ContractAuditSalesApprove()
            {
                contract_status = "sales_requested"
            };

            JObject RequestSalesApproveObj = await new SalesApprove().RequestSalesApproveReceiver(vendorID, contractID, contractSalesSalesApprove);
            JToken targetItem = RequestSalesApproveObj;                            

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> RequestOwnerApprove(int vendorID, int contractID)
        {
            ContractAuditOwnerRequest contractAuditOwnerRequest = new ContractAuditOwnerRequest()
            {
                contract_status = "sales_requested"
            };
            JObject RequestOwnerApproveObj = await new OwnerApprove().RequestOwnerApproveReceiver(vendorID, contractID, contractAuditOwnerRequest);
            JToken targetItem = RequestOwnerApproveObj;

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> CreateContractAudit(int vendorID, string open_date)
        {
            Models.ContractAudit contractAudit = new Models.ContractAudit()
            {
                vendor = vendorID,
                inflow_type = "internal",
                contract_type = "new_grand_open",
                contract_date = DateTime.Today.ToString("yyyy-MM-dd"),
                contract_manager = 588,
                is_requested_first_onboarding = true,
                is_requested_template_menu = true,
                commission_contract_set = new Contract[] {
                    new Contract{
                        id = 11255
                    }
                },
                zero_commission_contract_set= new Contract[] {},
                additional_fee_set= new Contract[] { },
                open_date = open_date
            };
            JObject CreateContractAuditObj = await new ContractAudit().CreateContractAuditReceiver(vendorID, contractAudit);
            JToken targetItem = CreateContractAuditObj;

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> UpdateContractAudit(int vendorID, int contractID, int v, string open_date)
        {
            ContractAuditPatch contractAuditPatch = new ContractAuditPatch()
            {
                vendor = vendorID,
                open_date = open_date
            };
            JObject UpdateContractAuditObj = await new ContractAudit().UpdateContractAuditReceiver(vendorID, contractID, contractAuditPatch);
            JToken targetItem = UpdateContractAuditObj;

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
