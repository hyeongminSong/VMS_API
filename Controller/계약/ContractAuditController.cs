using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class ContractAuditController
    {
        private readonly ContractAudit _contractAudit = new ContractAudit();
        private readonly SalesApprove _salesApprove = new SalesApprove();
        private readonly OwnerApprove _ownerApprove = new OwnerApprove();

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
            //계약 심사 요청
            public async Task<JObject> CreateContractAuditReceiver(int vendorID, Models.ContractAudit contractAudit)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(contractAudit, settings);
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
            //계약 심사 수정
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

        private class SalesApprove
        {
            //세일즈 심사 승인 요청
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
        private class OwnerApprove
        {
            //사장님 승인 요청
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
        public async Task<JObject> RequestSalesApprove(int vendorID, int contractID)
        {
            ContractAuditSalesApprove contractSalesSalesApprove = new ContractAuditSalesApprove()
            {
                contract_status = "sales_requested"
            };

            JObject RequestSalesApproveObj = await _salesApprove.RequestSalesApproveReceiver(vendorID, contractID, contractSalesSalesApprove);
            return RequestSalesApproveObj;
            /*JToken targetItem = RequestSalesApproveObj;                            

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }

        public async Task<JObject> RequestOwnerApprove(int vendorID, int contractID)
        {
            ContractAuditOwnerRequest contractAuditOwnerRequest = new ContractAuditOwnerRequest()
            {
                contract_status = "sales_requested"
            };
            JObject RequestOwnerApproveObj = await _ownerApprove.RequestOwnerApproveReceiver(vendorID, contractID, contractAuditOwnerRequest);
            return RequestOwnerApproveObj;
            /*JToken targetItem = RequestOwnerApproveObj;

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }

        public async Task<JObject> CreateContractAudit(int vendorID, int managerId, int contractId, string open_date)
        {
            Models.ContractAudit contractAudit = new Models.ContractAudit()
            {
                vendor = vendorID,
                inflow_type = "internal",
                contract_type = "new_grand_open",
                contract_date = DateTime.Today.ToString("yyyy-MM-dd"),
                contract_manager = managerId,
                is_requested_first_onboarding = true,
                is_requested_template_menu = true,
                commission_contract_set = new Contract[] {
                    new Contract{
                        id = contractId
                    }
                },
                zero_commission_contract_set= new Contract[] {},
                additional_fee_set= new Contract[] { },
                open_date = open_date
            };
            JObject CreateContractAuditObj = await _contractAudit.CreateContractAuditReceiver(vendorID, contractAudit);
            return CreateContractAuditObj;
            /*JToken targetItem = CreateContractAuditObj;

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }

        public async Task<JObject> UpdateContractAudit(int vendorID, int contractID, string open_date)
        {
            ContractAuditPatch contractAuditPatch = new ContractAuditPatch()
            {
                vendor = vendorID,
                open_date = open_date
            };
            JObject UpdateContractAuditObj = await _contractAudit.UpdateContractAuditReceiver(vendorID, contractID, contractAuditPatch);
            return UpdateContractAuditObj;
            /*JToken targetItem = UpdateContractAuditObj;

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
