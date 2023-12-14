using System.Net.Http;

namespace GetIPInfoApi
{
    public static class IPExternalApi
    {
        public static async Task<string> GetIpInfoFromExternalApi(string ipAddress)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://ipinfo.io/{ipAddress}/geo");
            return response;
        }
    }
}
