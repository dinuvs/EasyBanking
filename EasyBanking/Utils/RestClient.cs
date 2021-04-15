using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EasyBanking.Utils
{
    public class RestClient : IRestClient
    {
        public async Task<string> GetAsync(string url)
        {
            HttpClient client = new();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            var response = await client.SendAsync(request);
            if(!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }

    }
}
