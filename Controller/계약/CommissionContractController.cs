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
    public class CommissionContractController
    {
        private readonly HttpClient _httpClient;
        public CommissionContractController(Config config)
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
        /*private async Task<JArray> GetVendorBillingEntitiesReceiver(int vendorID)
        {
            var VendorBillingEntitiesEndpoint = string.Format("/vendor/{0}/billing-entities/", vendorID);
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), VendorBillingEntitiesEndpoint))
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

        private async Task<JObject> GetFranchisesIDReceiver(bool is_group, string franchisesName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "is_group", is_group.ToString() },
            { "name", franchisesName }
            // Add more parameters as needed
        };
            var Endpoint = "/franchises/"; // Ticket Download EndPoint
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
        public async Task<int> GetFranchisesID(bool is_group, string franchisesName)
        {
            JObject GetFranchisesIDObj = await GetFranchisesIDReceiver(is_group, franchisesName);
            JToken targetItem = GetFranchisesIDObj["results"].
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }
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
        }*/
        private class VendorBillingEntities
        {
            private readonly HttpClient _httpClient;
            public VendorBillingEntities(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            private async Task<JArray> GetVendorBillingEntitiesReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/billing-entities/", vendorID);
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
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON parsing error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
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
        private class FranchisesID
        {
            private readonly HttpClient _httpClient;
            public FranchisesID(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }
            private async Task<JObject> GetFranchisesIDReceiver(bool is_group, string franchisesName)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "is_group", is_group.ToString() },
            { "name", franchisesName }
            // Add more parameters as needed
        };
                var Endpoint = "/franchises/"; // Ticket Download EndPoint
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
            public async Task<int> GetFranchisesID(bool is_group, string franchisesName)
            {
                JObject GetFranchisesIDObj = await GetFranchisesIDReceiver(is_group, franchisesName);
                JToken targetItem = GetFranchisesIDObj["results"].
                                FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));

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
        private class CommissionID
        {
            private readonly HttpClient _httpClient;
            public CommissionID(HttpClient httpClient)
            {
                _httpClient = httpClient;
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


        private async Task<JObject> AddCommissionContractReceiver(CommissionContract commissionContract)
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
        public async Task<int> AddCommissionContract(int vendor_id, string billing_entity_type,
            bool is_group, string franchises_name,
            bool is_alliance, string commission_name, string order_type,
            string franchiseType, string start_date)
        {
            /*            int billingTypeID = await GetVendorBillingEntities(vendor_id, billing_entity_type);
                        int franchisesID = await GetFranchisesID(is_group, franchises_name);
                        int commissionID = await GetCommissionID(is_alliance, order_type, franchisesID, commission_name);*/

            int billingTypeID = new VendorBillingEntities(_httpClient).GetVendorBillingEntities(vendor_id, billing_entity_type).Result;
            int franchisesID = new FranchisesID(_httpClient).GetFranchisesID(is_group, franchises_name).Result;
            int commissionID = new CommissionID(_httpClient).GetCommissionID(is_alliance, order_type, franchisesID, commission_name).Result;

            CommissionContract contract = new CommissionContract()
            {
                vendor = vendor_id,
                commission = commissionID,
                billing_entity_info = billingTypeID,
                is_alliance = is_alliance,
                franchise_type = franchiseType,
                commission_start_date = start_date
            };

            JObject GetCommissionIDObj = AddCommissionContractReceiver(contract).Result;
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
