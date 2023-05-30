using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace Wissance.WeatherControl.WebApi.V2.Extensions
{
    public static class IDictionaryExtensions 
    {
        public static void AddRange<TK,TV>(this IDictionary<TK,TV> original, IDictionary<TK,TV> dictToAdd)
        {
            foreach (KeyValuePair<TK,TV> item in dictToAdd)
            {
                if (!original.ContainsKey(item.Key))
                {
                    original[item.Key] = item.Value;
                }
            }
        }
    }
}