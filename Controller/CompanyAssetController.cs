using ConsoleApp1;
using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    public class CompanyAssetController
    {
        private readonly Organization _organization = new Organization();
        private readonly Staff _staff = new Staff();

        private static HttpClient _httpClient;
        public CompanyAssetController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }
        private class Organization
        {
            public async Task<JObject> GetOrganizationReceiver(string depth1, string depth2, string depth3)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "depth1", depth1 },
                    { "depth2", depth2 },
                    { "depth3", depth3 }
                };
                var Endpoint = "/organization/"; // Ticket Download EndPoint

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

        private class Staff
        {
            public async Task<JObject> GetStaffReceiver(string staffName)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", staffName }
                };
                var Endpoint = "/staff/";

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
            public async Task<JObject> GetStaffReceiver(string staffName, string organization)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", staffName },
                    { "organization", organization }
                };
                var Endpoint = "/staff/";

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

        public async Task<JObject> GetOrganization(string depth1, string depth2, string depth3)
        {
            JObject GetOrganizationObj = await _organization.GetOrganizationReceiver(depth1, depth2, depth3);
            return GetOrganizationObj;
        }

        public async Task<JObject> GetStaff(string staffName)
        {
            JObject GetStaffObj = await _staff.GetStaffReceiver(staffName);
            return GetStaffObj;
        }
        public async Task<JObject> GetStaff(string staffName, string depth1, string depth2, string depth3)
        {
            string organization = string.Join(">", depth1, depth2, depth3);
            JObject GetStaffObj = await _staff.GetStaffReceiver(staffName, organization);
            return GetStaffObj;
        }
    }
}
