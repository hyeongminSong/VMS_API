using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class QueryStringBuilder
    {
        public static string BuildQueryString(Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return "";
            }

            parameters = parameters.Where(kvp => kvp.Value != null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var queryString = string.Join("&", parameters.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value.ToString())}"));
            return $"?{queryString}";
        }
    }

}
