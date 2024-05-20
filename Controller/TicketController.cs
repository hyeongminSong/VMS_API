using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using ConsoleApp1.Models;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using ConsoleApp1.Models.TASK;

namespace ConsoleApp1
{
    public class TicketController
    {
        private readonly Ticket _ticket = new Ticket();
        private readonly Organization _organization = new Organization();

        private static HttpClient _httpClient;

        public TicketController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private class Ticket
        {
            public async Task<JObject> GetTicketTypesReceiver(string type_depth1, string type_depth2)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "type_depth1", type_depth1 },
                    { "type_depth2", type_depth2 },
                    // Add more parameters as needed
                };
                var Endpoint = "/ticket-types/"; //EndPoint

                string queryString = QueryStringBuilder.BuildQueryString(parameters);
                Endpoint += queryString;

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, Endpoint))
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
            }
            public async Task<JObject> GetTicketInfomationReceiver(int ticketID)
            {
                var Endpoint = string.Format("/tickets/{0}/", ticketID.ToString()); //EndPoint

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, Endpoint))
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
            }

            public async Task<JObject> CreateTicketReceiver(Models.TASK.Ticket ticket)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(ticket, settings);

                //Json 가공
                JObject jsonObject = JObject.Parse(jsonString);
                jsonObject["ticket_type"] = ticket.ticket_type.id;
                jsonObject["target_vendor"] = ticket.target_vendor.id;
                jsonObject["target_company"] = ticket.target_company.id;
                if (jsonObject["target_user"].HasValues)
                {
                    jsonObject["target_user"] = ticket.target_user.id;
                }
                jsonObject["assigned_organization"] = ticket.assigned_organization.id;
                jsonString = jsonObject.ToString();

                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/tickets/"; //EndPoint

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, Endpoint)
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

            public async Task<JObject> UpdateTicketReceiver(int ticketID, Models.TASK.Ticket ticket)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                string jsonString = JsonConvert.SerializeObject(ticket, settings);

                //Json 가공
                JObject jsonObject = JObject.Parse(jsonString);
                jsonObject["ticket_type"] = ticket.ticket_type.id;
                jsonString = jsonObject.ToString();

                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/tickets/{0}/", ticketID);

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
            public async Task<JObject> SearchTicketReceiver(int vendor_id, string status, string type_depth1, string type_depth2)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "vendor_id", vendor_id.ToString() },
                    { "status", status},
                    { "type_depth1", type_depth1 },
                    { "type_depth2", type_depth2 },
                    // Add more parameters as needed
                };
                var Endpoint = "/tickets/"; //EndPoint

                string queryString = QueryStringBuilder.BuildQueryString(parameters);
                Endpoint += queryString;

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, Endpoint))
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
            }

            public async Task<JObject> UpdateTicketAssigneeReceiver(int ticketID, TicketAssignee assignee)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(assignee, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/tickets/{0}/assignee-update/", ticketID);

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
            public async Task GetTaskCSVFileReceiver(string FilePath, string TiekctType)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "status", "ready,in_progress" },
                    { "type_depth1", TiekctType },
                    { "assigned_organization_name", "CXI 본부>컨텐츠관리실>" },
                    // Add more parameters as needed
                };
                var Endpoint = "/tickets/download/"; // Ticket Download EndPoint

                string queryString = QueryStringBuilder.BuildQueryString(parameters);
                Endpoint += queryString;

                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, Endpoint))
                {
                    using (var response = await _httpClient.SendAsync(requestMessage))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var contentStream = await response.Content.ReadAsStreamAsync())
                            {
                                using (var fileStream = File.Create(FilePath))
                                {
                                    await contentStream.CopyToAsync(fileStream);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Response status: {response.StatusCode}");
                        }
                    }
                }
            }
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

        public async Task<JObject> GetTicketType(string type_depth1, string type_depth2)
        {
            JObject getTicketTypeObj = await _ticket.GetTicketTypesReceiver(type_depth1, type_depth2);
            return getTicketTypeObj;
        }
        public async Task<JObject> GetTicketInfomation(int ticketID)
        {
            JObject getTicketInfomationObj = await _ticket.GetTicketInfomationReceiver(ticketID);
            return getTicketInfomationObj;
        }
        public async Task<JObject> CreateTicket(Models.TASK.Ticket ticket)
        {
            JObject createTicketObj = await _ticket.CreateTicketReceiver(ticket);
            return createTicketObj;
        }
        public async Task<JObject> UpdateTicket(int ticketID, Models.TASK.Ticket ticket)
        {
            JObject updateTicketObj = await _ticket.UpdateTicketReceiver(ticketID, ticket);
            return updateTicketObj;
        }
        public async Task<JObject> SearchTicket(int vendor_id, string status, string type_depth1, string type_depth2)
        {
            JObject searchTicketObj = await _ticket.SearchTicketReceiver(vendor_id, status, type_depth1, type_depth2);
            return searchTicketObj;
        }
        public async Task<JObject> GetOrganization(string depth1, string depth2, string depth3)
        {
            JObject GetOrganizationObj = await _organization.GetOrganizationReceiver(depth1, depth2, depth3);
            return GetOrganizationObj;
        }
        public async Task<JObject> UpdateTicketAssignee(int ticketID, TicketAssignee assignee)
        {
            JObject updateTicketAssigneeObj = await _ticket.UpdateTicketAssigneeReceiver(ticketID, assignee);
            return updateTicketAssigneeObj;
        }
        public async Task GetTaskCSVFile(string filePath, string TicketType)
        {
            await _ticket.GetTaskCSVFileReceiver(filePath, "request_info_update");
        }
    }
}
