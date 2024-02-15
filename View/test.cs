using System.Collections.Generic;
using System;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1.View
{
    public class API
    {
        private static HttpClient _httpClient;
        public API()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://fe.catalogyo.commerce.staging.yogiyo.co.kr/")
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("X-apikey", "rpa");
            _httpClient.DefaultRequestHeaders.Add("X-apisecret", "aLiFvCDYOCGhS0oD0FHGs1XG9Bp7Fgrb");
            _httpClient.DefaultRequestHeaders.Add("Referer", "RPA");
        }
        public async Task<JObject> APIReceiver(object Obj)
        {
            /*var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            string jsonString = JsonConvert.SerializeObject(Obj, settings);*/
            string jsonString = JsonConvert.SerializeObject(Obj);
            var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            

            var Endpoint = string.Format("/api/v1/{0}/rpa/{1}/sync-menu", "YGY-staging-g", "1001318");

            using (var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Endpoint)
            {
                Content = jsonContent
            })
            {
                try
                {
                    using (var response = await _httpClient.SendAsync(requestMessage))
                    {
                        Console.WriteLine(response.RequestMessage);
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
    public class CatalogYoMenuRequestObj
    {
        public List<CatalogYoMenuItem> menus { get; set; }
        public List<CatalogYoMenuOption> options { get; set; }
    }

    public class CatalogYoMenuItem
    {
        public string check_for_delete {  get; set; }
        public string category_name { get; set; }
        public string category_description { get; set; }
        public int category_sequence { get; set; }
        public string franchise_item_code { get; set; }
        public string menu_name { get; set; }
        public string menu_description { get; set; }
        public int price { get; set; }
        public int sequence { get; set; }
        public string invisible { get; set; }
        public string invisible_date { get; set; }
        public string related_option_category_name { get; set; }
        public string menu_attributes { get; set; }
        public string menu_deli_time_start { get; set; }
        public string menu_deli_time_end { get; set; }
    }
    public class CatalogYoMenuOption
    {
        public string check_for_delete {  get; set; }
        public string mandatory { get; set; }
        public string quantity_changeable { get; set; }
        public string multiple { get; set; }
        public int multiple_count { get; set; }
        public string option_category_name { get; set; }
        public string franchise_item_code { get; set; }
        public string option_name { get; set; }
        public int price { get; set; }
        public int sequence { get; set; }
        public string invisible { get; set; }
        public string invisible_until { get; set; }
        public string deposit {  get; set; }
        public string reusable_packaging {  get; set; }
    }
    public class test
    {
        private static DataTable ReadCsvToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // 첫 번째 행을 읽어 DataTable의 열로 추가
                if (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        dataTable.Columns.Add(field);
                    }
                }

                // 나머지 행을 읽어 DataTable의 행으로 추가
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    DataRow dataRow = dataTable.Rows.Add();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        dataRow[i] = fields[i];
                    }
                }
            }
            return dataTable;
        }
        
        public async Task<JObject> Run()
        {
            string csvMenuFilePath = "C:\\Users\\A202111078\\Desktop\\CM\\291-40-01367_원온원커피_1312848_menu.csv"; // CSV 파일 경로 및 파일명 설정
            string csvOptionFilePath = "C:\\Users\\A202111078\\Desktop\\CM\\291-40-01367_원온원커피_1312848_option.csv"; // CSV 파일 경로 및 파일명 설정
            List<CatalogYoMenuItem> catalogYoMenuItems = new List<CatalogYoMenuItem>();
            List<CatalogYoMenuOption> catalogYoMenuOptions = new List<CatalogYoMenuOption>();

            // CSV 파일을 읽어 DataTable로 변환
            DataTable menuDataTable = ReadCsvToDataTable(csvMenuFilePath);
            DataTable optionDataTable = ReadCsvToDataTable(csvOptionFilePath);
            
            foreach (DataRow row in menuDataTable.Rows)
            {
                string menu_section_name = row["category_name"].ToString();
                string menu_section_description = row["category_description"].ToString();
                string menu_name = row["menu_name"].ToString();
                string menu_description = row["menu_description"].ToString();
                string related_option_category_name = row["related_option_category_name"].ToString();
                int price = Convert.ToInt32(row["price"]);
                /*string invisible = Convert.ToBoolean(row["invisible"]) ? "Y" : "N";
                string invisible_until = "9999-12-31";*/

                menu_section_description = menu_section_description.Replace("\n", "<br>");
                menu_description = menu_description.Replace("\n", "<br>");

                catalogYoMenuItems.Add(new CatalogYoMenuItem
                {
                    check_for_delete = "N",
                    category_name = menu_section_name,
                    category_description = menu_section_description,
                    category_sequence = 1,
                    menu_name = menu_name,
                    menu_description = menu_description,
                    sequence = 1,
                    price = price,
                    related_option_category_name = related_option_category_name,
                    invisible = "N",
                    invisible_date = "",
                });
            }

            foreach (DataRow row in optionDataTable.Rows)
            {
                string optionSectionName = row["option_category_name"].ToString();
                string quantity_changeable = row["quantity_changeable"].ToString();
                string mandatory = row["mandatory"].ToString();
                string multiple = row["multiple"].ToString();
                int selectableOptionNumbers = Convert.ToInt32(row["multiple_count"]);
                string option_name = row["option_name"].ToString();
                int price = Convert.ToInt32(row["price"]);

                /*string invisible = Convert.ToBoolean(row["invisible"]) ? "Y" : "N";
                string invisible_until = "9999-12-31";
                string Deposit = Convert.ToBoolean(row["is deposit"]) ? "Y" : "N";
                string Resuable = Convert.ToBoolean(row["is reusable packaging"]) ? "Y" : "N";*/

                catalogYoMenuOptions.Add(new CatalogYoMenuOption
                {
                    check_for_delete = "N",
                    quantity_changeable = quantity_changeable,
                    mandatory = mandatory,
                    multiple = multiple,
                    multiple_count = selectableOptionNumbers,
                    option_category_name = optionSectionName,
                    option_name = option_name,
                    price = price,
                    invisible = "N",
                    invisible_until = "",
                    sequence = 1,
                    deposit = "N",
                    reusable_packaging = "N"
                });
            }

            /*MenuRequestObj MenuRequestObj = new MenuRequestObj()
            {
                menus = menuItems,
                optionss = optionItems
            };*/

            CatalogYoMenuRequestObj catalogYoMenuRequestObj = new CatalogYoMenuRequestObj()
            {
                menus = catalogYoMenuItems,
                options = catalogYoMenuOptions
            };

            string jsonContent = JsonConvert.SerializeObject(catalogYoMenuRequestObj);

            File.WriteAllText("C:\\Users\\A202111078\\Desktop\\RPA_to_CatalogYo_API_"+DateTime.Now.ToString("yyyyMMddhhmmss")+".json", jsonContent);

            //Console.WriteLine(JsonConvert.SerializeObject(MenuRequestObj));
            return await new API().APIReceiver(catalogYoMenuRequestObj);
        }
    }
}
