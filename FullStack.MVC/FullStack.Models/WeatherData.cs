﻿namespace FullStack.Models
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
        public string Icon { get; set; }

        public Wind Wind { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Degrees { get; set; }
    }

}
