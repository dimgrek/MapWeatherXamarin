using System.Collections.Generic;

namespace MapWeatherXamarin.Models
{
    public class ForecastResponce
    {
        public List<MainTemp> list { get; set; }
    }
    public class MainTemp
    {
        public Main Main { get; set; }
        public string dt_txt { get; set; }
    }
}
