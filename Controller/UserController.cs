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
    public class UserController
    {
        private readonly User _user = new User();

        private static HttpClient _httpClient;
        public UserController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }
        private class User
        {
            public async Task<JObject> GetUserReceiver(string oneID, string phoneNumber, string userName)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "username", oneID },
                    { "phone", phoneNumber },
                    { "name", userName }
                };
                var Endpoint = "/users/"; 

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

        public async Task<JObject> GetUser(string oneID, string phoneNumber, string userName)
        {
            JObject GetOrganizationObj = await _user.GetUserReceiver(oneID, phoneNumber, userName);
            return GetOrganizationObj;
        }
    }
}
