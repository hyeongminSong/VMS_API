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
    public class CompanyController
    {
        private static HttpClient _httpClient;
        public CompanyController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        public class Company
        {
            public async Task<JObject> GetCompanyIDReceiver(string companyNumber)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
        {
            { "company_number", companyNumber },
            // Add more parameters as needed
        };
                var Endpoint = "/companies/";

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

        public class PrincipalCompanies
        {

        }

        public async Task<int> GetCompanyID(string companyNumber)
        {
            JObject GetVendorAddressObj = await new Company().GetCompanyIDReceiver(companyNumber);
            JToken targetItem = GetVendorAddressObj["results"].First();

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
