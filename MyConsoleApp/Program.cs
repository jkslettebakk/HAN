using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SerializeExtra
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
        public string SummaryField;
        public IList<DateTimeOffset> DatesAvailable { get; set; }
        public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; }
        public string[] SummaryWords { get; set; }
    }

    public class HighLowTemps
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            string summer = "Hot,hot";
            DateTime dateTime = DateTime.Now;

            var weatherForecast = new WeatherForecast
            {
                Date = dateTime,
                TemperatureCelsius = 25,
                Summary = summer,
                SummaryField = "Hot",
                DatesAvailable = new List<DateTimeOffset>() 
                    { DateTime.Now.AddDays(-2), dateTime },
                TemperatureRanges = new Dictionary<string, HighLowTemps>
                    {
                        ["Cold"] = new HighLowTemps { High = 20, Low = -10 },
                        ["Hot"] = new HighLowTemps { High = 60 , Low = 20 }
                    },
                SummaryWords = new[] { "Cool", "Windy", "Humid" }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(weatherForecast, options);

            Console.WriteLine(jsonString);
        }
    }
}
