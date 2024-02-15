using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Models
{
    public class AddressInfo
    {
        public string sido { get; set; }
        public string sigugun { get; set; }
        public string admin_dongmyun { get; set; }
        public string law_dongmyun { get; set; }
        public string road_dongmyun { get; set; }
        public string admin_detailed_address { get; set; }
        public string law_detailed_address { get; set; }
        public string road_detailed_address { get; set; }
        public string ri { get; set; }
        public string zip_code { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string custom_detailed_address { get; set; }
    }
    public class GetAddressItem
    {
        public AddressInfo GetAddressInfo(JObject addressInfoObj, string address, string addressDetail)
        {
            JToken targetItem = addressInfoObj["items"].AsEnumerable().
                GroupBy(jToken => jToken["road"].
                AsEnumerable().Count(item => address.Contains((string)item))).
                SelectMany(group => group).
                OrderByDescending(jToken => (int)jToken["road"].
                AsEnumerable().
                Count(item => address.Contains((string)item))).
                FirstOrDefault();

            //JToken targetItem = addressInfoObj["items"].FirstOrDefault(item => item["road"].All(element => address.Contains((string)element)));

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
                    custom_detailed_address = addressDetail
                };
                return addressInfo;
            }
            else
            {
                return null;
            }
        }
        public string[] GetAddressToken(string address)
        {
            Dictionary<string, string> sido_dictionary = new Dictionary<string, string>
        {
            {"서울", "서울특별시"},
            {"서울시", "서울특별시"},
            {"강원", "강원특별자치도"},
            {"강원도", "강원특별자치도"},
            {"경기", "경기도"},
            {"경남", "경상남도"},
            {"경북", "경상북도"},
            {"광주", "광주광역시"},
            {"대구", "대구광역시"},
            {"대구시", "대구광역시"},
            {"대전", "대전광역시"},
            {"대전시", "대전광역시"},
            {"부산", "부산광역시"},
            {"부산시", "부산광역시"},
            {"세종", "세종특별자치시"},
            {"세종시", "세종특별자치시"},
            {"세종특별시", "세종특별자치시"},
            {"울산", "울산광역시"},
            {"울산시", "울산광역시"},
            {"인천", "인천광역시"},
            {"인천시", "인천광역시"},
            {"전남", "전라남도"},
            {"전북", "전라북도"},
            {"제주", "제주특별자치도"},
            {"제주도", "제주특별자치도"},
            {"충남", "충청남도"},
            {"충북", "충청북도"}
        };
            string pattern = @"^(?<sido>" + string.Join("|", sido_dictionary.Values.Union(sido_dictionary.Keys)) + ")" +
                "(?<sigugun>.+(시|구|군))" +
                "(?<dongmyun>.+(로|길))?" +
                "(?<ri>.+리)?" +
                "(?<detail>.+)?";
            Match match = Regex.Match(address, pattern);

            if (match.Success)
            {
                Console.WriteLine(match.Groups);
                string[] resultArray = match.Groups.Cast<Group>()
                    //.Where(group => group.Captures.Count > 0 && !char.IsDigit(group.Captures[0].Value.First()))
                    .Where(group => group.Captures.Count > 0)
                    .Select(group => sido_dictionary.TryGetValue(group.Value, out var value) ? value : Regex.Replace(group.Value, @"[^\w\d]", "").Trim())
                    .ToArray();

                return resultArray;
            }
            else
            {
                return null;
            }
        }
    }
}
