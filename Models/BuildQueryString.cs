using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class QueryStringBuilder
    {
        public static string BuildQueryString(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return "";
            }

            var queryString = string.Join("&", parameters.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            return $"?{queryString}";
        }
    }

}
