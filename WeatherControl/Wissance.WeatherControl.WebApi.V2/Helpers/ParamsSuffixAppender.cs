using System.Text;

namespace Wissance.WeatherControl.WebApi.V2.Helpers
{

    public static class ParamsSuffixAppender
    {
        public static string Append(string param, string suffix = null)
        {
            StringBuilder sb = new StringBuilder(param);
            if (string.IsNullOrEmpty(suffix))
            {
                sb.Append(suffix);
            }

            return sb.ToString();
        }
    }
}