using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{

    public struct Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string  Description { get; set; }

        public Weather(WeatherResponce weatherResponce)
        {
            this.Country=weatherResponce.sys.Country;
            this.Name = weatherResponce.Name;
            this.Temp= weatherResponce.main.Temp;
            this.Description = weatherResponce.weather[0].Description;
            
        }


        public void print()
        {
            Console.WriteLine($"Country:{Country}, Name: {Name}, Temp: {Temp}, Discription: {Description}");
        }
    }
}

