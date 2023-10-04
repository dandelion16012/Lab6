using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Lab6;


namespace Lab6
{
}
    class program
    {
        static void Main(string[] args)
        {
        string Key = "9efa86170885592ef3b1450c671eb1bc";

        List<Weather> weathers = new List<Weather>();


            Random random = new Random();

            int n = 0;
            while (n < 50)
            {
            double latitude = -90 + random.NextDouble() * (90 + 90);
            double longitude = -180 + random.NextDouble() * (180 + 180);


            //exclude- исключаем ненужные параметры, metric- используем стандартные единицы измерения
            string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + latitude.ToString() + "&" +
                          "lon=" + longitude.ToString() + "&exclude=minutely,hourly,daily,alerts&" +
                          "units=metric&appid=" + Key;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);// создаём объект реквеста для возможности запроса
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();// получаем респонс, считываем данные с реквеста
                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))// для считывания всех данных из респонса
                                                                                                         // using чтобы убрать все используемые им ресурсы, наследуется от класса, который реализует интерфейс
                {
                    response = streamReader.ReadToEnd();
                    //обрабатываем json в сущность с#, десереализуем строку и превращаем в объект

                    WeatherResponce weatherResponse = JsonConvert.DeserializeObject<WeatherResponce>(response);
                //Console.WriteLine($"Temperature in {weatherResponse.Name}: {weatherResponse.main.Temp}");

                //if (weatherResponse.Name != "" || weatherResponse.sys.Country != "")
                if (weatherResponse.Name != "" || weatherResponse.Name != "")
                {
                        Weather weather = new Weather(weatherResponse);
                        weathers.Add(weather);
                        weather.print();
                        //Console.WriteLine($"Temperature in {weather.Name}: {weather.Temp}");
                        n++;

                    }
                }
            }
            Console.WriteLine();
            var linqMaxTemp = weathers.MaxBy(weather => weather.Temp).Country;
            Console.WriteLine($"страна с самиой высокой температурой: {linqMaxTemp}");
            var linqMinTemp = weathers.MinBy(weather => weather.Temp).Country;
            Console.WriteLine($"страна с самиой низкой температурой: {linqMinTemp}");


            var linqCountry = weathers.Select(weather => weather.Country).Distinct().ToList();
            Console.WriteLine($"уникальных стран: {linqCountry.Count}");

            var linqAverTemp = weathers.Average(weather => weather.Temp);
            Console.WriteLine($"средняя температура в мире: {linqAverTemp}");

            var linqDescription = weathers.FirstOrDefault(weather => weather.Description == "clear sky" ||
                                  weather.Description == "rain" ||
                                  weather.Description == "few clouds");
            Console.WriteLine($"Первая страна с определёнными значениями Description: {linqDescription.Country}, " +
                $"{linqDescription.Name}, " +
                $"discr: {linqDescription.Description}");


            /*  var linqDescription1 = from weather in weathers
                                         where weather.Description == "clear sky" ||
                                         weather.Description == "rain" ||
                                         weather.Description == "few clouds"
                                         select weather;*/




        }
           
    }