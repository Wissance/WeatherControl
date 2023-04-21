using Newtonsoft.Json;

namespace Wissance.WeatherControl.WebApi.V2.Data
{
    public class EdgeDbProjectCredentials
    {
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        [JsonProperty("tls_cert_data")]
        public string TlsCertData { get; set; }
        [JsonProperty("tls_ca")]
        public string TlsCa { get; set; }
        [JsonProperty("tls_security")]
        public string TlsSecurity { get; set; }
    }
}