﻿using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

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
                }
            }
            public async Task<JObject> CreateVendorReceiver(Models.Vendor CreateVendorObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(CreateVendorObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/vendor/";

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
                }
            }
            public async Task<JObject> UpdateVendorReceiver(int vendorID, Object UpdateVendorObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(UpdateVendorObj, settings);
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
        public async Task<JObject> CreateVendor(Models.Vendor vendorObject)
        {
            JObject GetVendorAddressObj = await new Vendor().CreateVendorReceiver(vendorObject);
            return GetVendorAddressObj;

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
        public async Task<AddressInfo> GetVendorAddressInfo(string addressKeyword)
        {
            //return await new VendorAddress().GetVendorAddressReceiver(addressKeyword);
            string[] addressToken = new GetAddressItem().GetAddressToken(addressKeyword);
            JObject GetVendorAddressObj = await new VendorAddress().GetVendorAddressReceiver(addressKeyword);
            JToken targetItem = GetVendorAddressObj["items"].Count() == 1
                ? GetVendorAddressObj["items"].First() :
                GetVendorAddressObj["items"]
                .FirstOrDefault(item => addressToken.All(token => item["road"].Values().
                Select(tokenItem => Regex.Replace((string)tokenItem, @"[^\w\d]", "")).Contains(token))
                );

            if (targetItem != null)
            {
                AddressInfo addressInfo = new AddressInfo
                {
                    lat = (double)targetItem["point"]["lat"],
                    lon = (double)targetItem["point"]["lng"],
                    zip_code = (string)targetItem["zipcode"],
                    sido = (string)targetItem["road"]["sido"],
                    sigugun = (string)targetItem["road"]["sigugun"],
                    admin_dongmyun = (string)targetItem["admin"]["dongmyun"],
                    law_dongmyun = (string)targetItem["law"]["dongmyun"],
                    road_dongmyun = (string)targetItem["road"]["dongmyun"],
                    ri = (string)targetItem["road"]["ri"],
                    admin_detailed_address = (string)targetItem["admin"]["detail"],
                    law_detailed_address = (string)targetItem["law"]["detail"],
                    road_detailed_address = (string)targetItem["road"]["detail"],
                    custom_detailed_address = ""
                };
                return addressInfo;
            }
            else
            {
                return null;
            }
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
