﻿using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace ConsoleApp1.Controller
{
    public class VendorController
    {
        private static HttpClient _httpClient;
        public VendorController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private class Category
        {
            public async Task<JArray> GetCategoryReceiver()
            {
                var Endpoint = "/categories/";
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
        }
        private class VendorAddress
        {
            public async Task<JObject> GetVendorAddressReceiver(string addressKeyword)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "search_keyword", addressKeyword },
            // Add more parameters as needed
        };
                var Endpoint = "/vendor-addresses/"; // Ticket Download EndPoint

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
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON parsing error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }
        private class Vendor
        {
            public async Task<JObject> GetVendorReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/", vendorID);
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
            public async Task<JObject> CreateVendorReceiver(string addressKeyword)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "search_keyword", addressKeyword },
            // Add more parameters as needed
        };
                var Endpoint = "/vendor-addresses/"; // Ticket Download EndPoint

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
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON parsing error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
            public async Task<JObject> UpdateVendorReceiver(int vendorID, Object UpdateVendorObj)
            {
                string jsonString = JsonConvert.SerializeObject(UpdateVendorObj);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/", vendorID);
                
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

        private class VendorBillingEntities
        {
            public async Task<JArray> GetVendorBillingEntitiesReceiver(int vendorID)
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
            
        }

        public async Task<category_set> GetCategoryToken(string categoryName)
        {
            JArray GetCategoryObj = await new Category().GetCategoryReceiver();

            JToken targetItem = GetCategoryObj.
                            FirstOrDefault(item => (string)item["display_name"] == categoryName);

            if (targetItem != null)
            {
                return new category_set
                {
                    id = (int)targetItem["id"],
                    name = (string)targetItem["name"],
                    category_type = (string)targetItem["category_type"]
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<JObject> GetVendor(int vendorID)
        {
            return await new Vendor().GetVendorReceiver(vendorID); ;
        }
        public async Task<int> CreateVendor(string addressKeyword)
        {
            JObject GetVendorAddressObj = await new Vendor().CreateVendorReceiver(addressKeyword);
            return 0;

            /*JToken targetItem = GetVendorAddressObj.
                            FirstOrDefault(item => (string)item["billing_entity_type"] == type);

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }
        public async Task<int> UpdateVendor(int vendorID, Object obj)
        {
            JObject UpdateVendorObj = await new Vendor().UpdateVendorReceiver(vendorID, obj);
            return 0;
        }
        public async Task<int> GetVendorAddress(string addressKeyword)
        {
            JObject GetVendorAddressObj = await new VendorAddress().GetVendorAddressReceiver(addressKeyword);
            return 0;

            /*JToken targetItem = GetVendorAddressObj.
                            FirstOrDefault(item => (string)item["billing_entity_type"] == type);

            if (targetItem != null)
            {
                return (int)targetItem["id"];
            }
            else
            {
                return 0;
            }*/
        }
        public async Task<int> GetVendorBillingEntities(int vendorID, string type)
        {
            JArray GetVendorBillingEntitiesObj = await new VendorBillingEntities().GetVendorBillingEntitiesReceiver(vendorID);

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
