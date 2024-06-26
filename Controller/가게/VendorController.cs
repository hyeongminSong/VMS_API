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

namespace ConsoleApp1.Controller
{
    public class VendorController
    {
        private readonly Vendor _vendor = new Vendor();
        private readonly Address _address = new Address();
        private readonly VendorBillingEntities _vendorbillingEntities = new VendorBillingEntities();
        private readonly Category _category = new Category();
        private readonly TicketFile _ticketFile = new TicketFile();
        private readonly VendorFile _vendorFile = new VendorFile();

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
            //가게 부가 정보 - 카테고리 목록 조회
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
        private class Address
        {
            //주소 검색
            public async Task<JObject> GetAddressReceiver(string addressKeyword)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "search_keyword", addressKeyword }
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
        private class Vendor
        {
            //가게 검색(가게 상태 - 계약 완료)
            public async Task<JObject> SearchVendorFromNameReceiver(string vendorName, string companyNumber)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", vendorName },
                    { "company_number", companyNumber },
                };
                var Endpoint = "/vendor/";

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
            //벤더 이름 중복 체크
            public async Task<JObject> CheckVendorDuplicateNameReceiver(string vendorName)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", vendorName }
                };
                var Endpoint = "/vendor/check-duplicate/";

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
            //가게 상세 조회
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
            //가게 기본정보 생성
            public async Task<JObject> CreateVendorReceiver(VendorBasicInfo CreateVendorObj)
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
            //가게 기본정보 수정
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
            //벤더 첨부자료 추가
            public async Task<JObject> CreateVendorFilesReceiver(int vendorID, string imageType, string filePath)
            {
                /*
                 logo- Vendor 로고 이미지
                new_logo- New logo 이미지
                curation- Vendor 큐레이션 이미지
                background- Vendor 뒷배경 썸네일
                leaflet- 광고 전단지
                background1- Vendor 뒷배경 이미지1
                background2- Vendor 뒷배경 이미지2
                background3- Vendor 뒷배경 이미지3
                background4- Vendor 뒷배경 이미지4
                sales_registered- 영업 신고증
                delivery_district- 배달지역 참고이미지
                 */
                var formData = new MultipartFormDataContent
                {
                    { new StringContent(imageType.ToString()), "image_type" },
                    { new ByteArrayContent(System.IO.File.ReadAllBytes(filePath)), "file_path", System.IO.Path.GetFileName(filePath) },
                };

                var Endpoint = string.Format("/vendor/{0}/vendorfiles/", vendorID);

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
            //가게 서술정보 목록 조회
            public async Task<JArray> GetVendorDescriptionsInfoReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/description-infos/", vendorID);
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
            //가게 서술정보 수정
            public async Task<JObject> UpdateVendorDescriptionsInfoReceiver(int vendorID, int descriptionID, object UpdateVendorDescriptionInfoObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(UpdateVendorDescriptionInfoObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/description-infos/{1}/", vendorID, descriptionID);

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
            //운영자 목록 조회
            public async Task<JObject> GetVendorContactableEmployeesReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/contactable-employees/", vendorID);
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
            //운영자 등록(추가)
            public async Task<JObject> CreateVendorContactableEmployeesReceiver(int vendorID, object ContactableEmployeesObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(ContactableEmployeesObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/contactable-employees/", vendorID);

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
            //모바일 주문전달수단 활성화 여부 관리
            public async Task<JObject> UpdateVendorMobileRelayMethodsReceiver(int vendorID, object UpdatUpdateVendorMobileRelayMethodsObj)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(UpdatUpdateVendorMobileRelayMethodsObj, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/relay-methods/mobile/", vendorID);

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

            //주문전달수단 벌크 수정
            public async Task<JArray> UpdateGowinRelayMethodsReceiver(int vendorID, List<RelayMethod> UpdateGowinRelayMethodsObjList)
            {
                /*var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };*/
                
                string jsonString = JsonConvert.SerializeObject(UpdateGowinRelayMethodsObjList);
                //string jsonString = JsonConvert.SerializeObject(UpdateGowinRelayMethodsObjList, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/relay-methods/", vendorID);

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
                                return JArray.Parse(contentString);
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
            //연동 여부 조회
            public async Task<JObject> GetServiceActivationsReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/service-activations/", vendorID);
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
            //모바일 주문전달수단 활성화 여부 관리
            public async Task<JObject> UpdateServiceActivationsReceiver(int vendorID, int serviceActivationsID, ServiceActivation serviceActivation)
            {
                string jsonString = JsonConvert.SerializeObject(serviceActivation);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/service-activations/{1}/", vendorID, serviceActivationsID);

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
            public async Task<JObject> GetCopyVendorReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/copy-vendor/", vendorID);
                using (var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Endpoint))
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
            public async Task<JObject> GetContactableEmployeesReceiver(int vendorID)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "vendor_pk", vendorID.ToString() },
                };
                var Endpoint = string.Format("/vendor/{0}/contactable-employees/", vendorID);

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
        private class VendorBillingEntities
        {
            //정산주체
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
        private class TicketFile
        {
            public async Task<JObject> CreateTicketFileReceiver(int ticketID, string filePath)
            {
                var formData = new MultipartFormDataContent
                {
                    { new ByteArrayContent(System.IO.File.ReadAllBytes(filePath)), "file_path", System.IO.Path.GetFileName(filePath) },
                    { new StringContent(ticketID.ToString()) , "ticket" }
                };

                var Endpoint = string.Format("/tickets/{0}/ticket-files/", ticketID);

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
        }

        private class VendorFile
        {
            public async Task<JArray> GetVendorFileReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/vendorfiles/", vendorID);
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
            public async Task<JObject> CreateVendorFileReceiver(int vendorID, string imageType, string filePath)
            {
                var formData = new MultipartFormDataContent
                {
                    { new StringContent(imageType) , "image_type" },
                    { new ByteArrayContent(System.IO.File.ReadAllBytes(filePath)), "file_path", System.IO.Path.GetFileName(filePath) },
                };

                var Endpoint = string.Format("/vendor/{0}/vendorfiles/", vendorID);

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
        }

        public async Task<JArray> GetCategoryToken()
        {
            JArray GetCategoryObj = await _category.GetCategoryReceiver();
            return GetCategoryObj;
        }
        public async Task<JObject> SearchVendorFromName(string vendorName, string company_number)
        {
            return await _vendor.SearchVendorFromNameReceiver(vendorName, company_number);
        }
        public async Task<JObject> GetVendor(int vendorID)
        {
            return await _vendor.GetVendorReceiver(vendorID); ;
        }
        public async Task<JObject> CreateVendor(VendorBasicInfo vendorObject)
        {
            JObject GetVendorAddressObj = await _vendor.CreateVendorReceiver(vendorObject);
            return GetVendorAddressObj;
        }
        public async Task<JObject> UpdateVendor(int vendorID, Object obj)
        {
            JObject UpdateVendorObj = await _vendor.UpdateVendorReceiver(vendorID, obj);
            return UpdateVendorObj;
        }
        public async Task<JObject> GetAddressInfoObj(string addressKeyword)
        {
            JObject AddressInfoObj = await _address.GetAddressReceiver(addressKeyword);
            return AddressInfoObj;
        }
        public async Task<JArray> GetVendorBillingEntities(int vendorID)
        {
            JArray GetVendorBillingEntitiesObj = await _vendorbillingEntities.GetVendorBillingEntitiesReceiver(vendorID);
            return GetVendorBillingEntitiesObj;
        }
        public async Task<JObject> CreateVendorFiles(int vendorID, string imageType, string filePath)
        {
            JObject createVendorFilesObj = await _vendor.CreateVendorFilesReceiver(vendorID, imageType, filePath);
            return createVendorFilesObj;
        }

        public async Task<JArray> GetVendorDescriptionsInfo(int vendorID)
        {
            JArray getVendorDescriptionsInfoObj = await _vendor.GetVendorDescriptionsInfoReceiver(vendorID);
            return getVendorDescriptionsInfoObj;
        }

        public async Task<JObject> UpdateVendorDescriptionsInfo(int vendorID, int descriptionID, string description)
        {
            VendorDescriptionInfo vendorDescriptionInfo = new VendorDescriptionInfo()
            {
                description = description
            };
            JObject updateVendorDescriptionsInfoObj = await _vendor.UpdateVendorDescriptionsInfoReceiver(vendorID, descriptionID, vendorDescriptionInfo);
            return updateVendorDescriptionsInfoObj;
        }

        public async Task<JObject> GetVendorContactableEmployees(int vendorID)
        {
            JObject updateVendorDescriptionsInfoObj = await _vendor.GetVendorContactableEmployeesReceiver(vendorID);
            return updateVendorDescriptionsInfoObj;
        }

        public async Task<JObject> CreateVendorContactableEmployees(int vendorID, string phone)
        {
            ContactableEmployee contactableEmployee = new ContactableEmployee()
            {
                employee_type = "part_time",
                name = string.Empty,
                phone = phone,
                is_agree_recv_required_info = true
            };
            JObject createVendorDescriptionsInfoObj = await _vendor.CreateVendorContactableEmployeesReceiver(vendorID, contactableEmployee);
            return createVendorDescriptionsInfoObj;
        }

        public async Task<JObject> UpdateVendorMobileRelayMethods(int vendorID, string phone)
        {
            MobileRelay mobileRelay = new MobileRelay()
            {
                is_active = true,
                phone_number = phone,
            };
            JObject UpdateVendorMobileRelayObj = await _vendor.UpdateVendorMobileRelayMethodsReceiver(vendorID, mobileRelay);
            return UpdateVendorMobileRelayObj;
        }
        public async Task<JArray> UpdateGowinRelayMethods(int vendorID)
        {
            List<RelayMethod> relayMethodList = new List<RelayMethod>() { new RelayMethod()
            {
                method_type = "gowin",
                relaydevice = null,
                contactableemployee_set = new List<object>(),
                is_active = true,
                is_owner_using = false
            }};
            JArray UpdateGowinRelayMethodsObj = await _vendor.UpdateGowinRelayMethodsReceiver(vendorID, relayMethodList);
            return UpdateGowinRelayMethodsObj;
        }

        /*public async Task<JObject> GetVendorSearchByCompanyNumber(string vendorName, string companyNumber)
        {
            JObject UpdateVendorMobileRelayObj = await _vendor.SearchVendorReceiver(vendorName, companyNumber);
            return UpdateVendorMobileRelayObj;
        }*/
        public async Task<JObject> CheckVendorDuplicateName(string vendorName)
        {
            JObject CheckVendorDuplicateNameObj = await _vendor.CheckVendorDuplicateNameReceiver(vendorName);
            return CheckVendorDuplicateNameObj;
        }
        public async Task<JObject> CreateTiekctFile(int ticketID, string filePath)
        {
            JObject CreateTiekctFileObj = await _ticketFile.CreateTicketFileReceiver(ticketID, filePath);
            return CreateTiekctFileObj;
        }
        public async Task<JObject> GetServiceActivations(int vendorID)
        {
            JObject GetServiceActivationsObj = await _vendor.GetServiceActivationsReceiver(vendorID);
            return GetServiceActivationsObj;
        }
        public async Task<JObject> UpdateServiceActivations(int vendorID, int serviceActivationsID, bool isActive)
        {
            ServiceActivation serviceActivation = new ServiceActivation()
            {
                is_active = isActive,
            };
            JObject UpdateServiceActivationsObj = await _vendor.UpdateServiceActivationsReceiver(vendorID, serviceActivationsID, serviceActivation);
            return UpdateServiceActivationsObj;
        }
        public async Task<JObject> GetCopyVendor(int vendorID)
        {
            JObject GetCopyVendorObj = await _vendor.GetCopyVendorReceiver(vendorID);
            return GetCopyVendorObj;
        }
        public async Task<JObject> GetContactableEmployees(int vendorID)
        {
            JObject GetContactableEmployeesObj = await _vendor.GetContactableEmployeesReceiver(vendorID);
            return GetContactableEmployeesObj;
        }
    }
}
