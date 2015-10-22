using System.Net.Http;
using System.Threading.Tasks;

namespace MapWeatherXamarin.Service.Networking
{
    public class RestClient:IRestClient
    {
        public async Task<string> GetAsync(string uri)
        {
            var httpClient = new HttpClient(); 
            var responce = await httpClient.GetAsync(uri);
            var content = await responce.Content.ReadAsStringAsync();
            return content;
        }
    }
}
