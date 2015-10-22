using System.Threading.Tasks;

namespace MapWeatherXamarin.Service.Networking
{
    public interface IRestClient
    {
        Task<string> GetAsync(string uri);
    }
}
