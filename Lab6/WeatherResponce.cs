using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class WeatherResponce 
    {
        public string Name { get; set; }
        public TemperatureInfo main {  get; set; }
        public CountryInfo sys { get; set; }
        public DescriptionInfo [] weather { get; set; }

    }
}
