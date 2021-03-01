using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Model
{
    public class WeatherData
    {
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }

        public Wind Wind { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Degrees { get; set; }
    }

}
