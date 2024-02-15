﻿using ConsoleApp1.Models;
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
    public class CompanyController
    {
        private readonly Company _company = new Company();
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
            //사업자 검색
            public async Task<JObject> GetCompanyReceiver(string companyNumber)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "company_number", companyNumber },
                    // Add more parameters as needed
                };
                var Endpoint = "/principal-companies/";

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
            //사업자 검색(ID)
            public async Task<JObject> GetCompanyFromIDReceiver(int companyID)
            {
                var Endpoint = string.Format("/principal-companies/{0}/", companyID);

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
            //사업자 정보 조회
            public async Task<JObject> GetInquiryCompanyInfoReceiver(string companyNumber)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "company_number", companyNumber },
                    // Add more parameters as needed
                };
                var Endpoint = "/principal-companies/inquiry-cmpno/";

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
            //사업자 정보 등록
            public async Task<JObject> CreatePrincipalCompanyReceiver(PrincipalCompany principalCompanyObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(principalCompanyObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = "/principal-companies/";

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
            //사업자 정보 수정
            public async Task<JObject> UpdatePrincipalCompanyReceiver(int companyID, PrincipalCompany principalCompanyObj)
            {
                var Endpoint = string.Format("/principal-companies/{0}/", companyID);

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(principalCompanyObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

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

            //사업자정보 첨부자료 조회
            public async Task<JObject> GetCompanyFilesReceiver(int companyID)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "principal_company", companyID.ToString() },
                    // Add more parameters as needed
                };

                var Endpoint = "/companyfiles/";

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

            //사업자정보 첨부자료 등록
            public async Task<JObject> CreateCompanyFilesReceiver(int companyID, int fileType, string filePath)
            {
                /*
                 * 개인
                 * 1. 사업자등록증 file_type = 3
                 * 2. 실소유자 확인 방법: 여
                 * 법인(영리법인)
                 * 1. 사업자등록증 file_type = 3 
                 * 2. 실소유자 확인 방법: 주주명부 file_type = 4
                 * 3. 인감증명서 file_type = 2
                 * 4. 등기부등본 file_type = 1
                 * 법인(비영리법인)
                 * 1. 사업자등록증 file_type = 3
                 * 2. 실소유자 확인 방법: 주주명부 file_type = 4
                 * 3. 인감증명서 file_type = 2
                 * 4. 등기부등본 file_type = 1
                 * 5. 설립목적: 자선
                 * 6. 설립목적 검증 서류: 정관 file_type = 8
                 */
                var formData = new MultipartFormDataContent
                {
                    { new StringContent(fileType.ToString()), "file_type" },
                    { new ByteArrayContent(System.IO.File.ReadAllBytes(filePath)), "file_path", System.IO.Path.GetFileName(filePath) },
                    { new StringContent(companyID.ToString()) , "principal_company" }
                };

                var Endpoint = "/companyfiles/";

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Endpoint)
                {
                    Content = formData
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
            //사업자정보 첨부자료 삭제
            public async Task<JObject> DeleteCompanyFilesReceiver(int fileID)
            {
                var Endpoint = string.Format("/companyfiles/{0}/", fileID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("DELETE"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                if (string.IsNullOrEmpty(contentString)){
                                    return new JObject();
                                }
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
            public async Task<JObject> GetOneIDOBjReceiver(string oneiD)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "username", oneiD },
                    // Add more parameters as needed
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

        public async Task<JObject> GetCompanyID(string companyNumber)
        {
            JObject GetCompanyObj = await _company.GetCompanyReceiver(companyNumber);
            return GetCompanyObj;
        }
        public async Task<JObject> GetCompanyFromIDObj(int companyID)
        {
            JObject GetCompanyFromIDObj = await _company.GetCompanyFromIDReceiver(companyID);
            return GetCompanyFromIDObj;
        }

        public async Task<JObject> CreatePrincipalCompany(PrincipalCompany principalCompanyObj)
        {
            JObject CreatePrincipalCompanyObj = await _company.CreatePrincipalCompanyReceiver(principalCompanyObj);
            return CreatePrincipalCompanyObj;
        }
        public async Task<JObject> UpdatePrincipalCompany(int companyID, PrincipalCompany principalCompanyObj)
        {
            JObject UpdatePrincipalCompanyObj = await _company.UpdatePrincipalCompanyReceiver(companyID, principalCompanyObj);
            return UpdatePrincipalCompanyObj;
        }
        public async Task<JObject> GetInquiryCompanyInfo(string companyNumber)
        {
            JObject InquiryCompanyInfoObj = await new Company().GetInquiryCompanyInfoReceiver(companyNumber);
            if (InquiryCompanyInfoObj != null)
            {
                return InquiryCompanyInfoObj;
            }
            else
            {
                return null;
            }
        }

        public async Task<JObject> GetCompanyFilesObj(int companyID)
        {
            JObject getCompanyFilesObj = await _company.GetCompanyFilesReceiver(companyID);
            return getCompanyFilesObj;
        }
        public async Task<JObject> CreateCompanyFilesObj(int companyID, int fileType, string filePath)
        {
            JObject createCompanyFilesObj = await _company.CreateCompanyFilesReceiver(companyID, fileType, filePath);
            return createCompanyFilesObj;
        }
        public async Task<JObject> DeleteCompanyFilesObj(int fileID)
        {
            JObject deleteCompanyFilesObj = await _company.DeleteCompanyFilesReceiver(fileID);
            return deleteCompanyFilesObj;
        }
        public async Task<JObject> GetOneIDObj(string oneID)
        {
            JObject getOneIDObj = await _company.GetOneIDOBjReceiver(oneID);
            return getOneIDObj;
        }

    }
}
