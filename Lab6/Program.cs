using Newtonsoft.Json;
using System.Net;
using Lab6;


namespace lab6
{
    class program
    {
        static void Main(string[] args)
        {         

            List<Weather> weathers = new List<Weather>();

            Random random = new Random();

            int n = 0;
            while (n< 50)
            {
                double latitude= -90+random.NextDouble()*(90+90);
                double longitude = -180 + random.NextDouble() * (180 + 180);
                string Key = "d8499591d06290612897d7f0c9290496";
                //exclude- исключаем ненужные параметры, metric- используем стандартные единицы измерения
                string url = "https://api.openweathermap.org/data/3.0/onecall?lat=" + latitude.ToString() + 
                    "&" +"lon=" + longitude.ToString() + "&exclude=minutely,hourly,daily,alerts&" +"units=metric&appid=" + Key;
                             
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);// создаём объект реквеста для возможности запроса
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();// получаем респонс, считываем данные с реквеста
                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))// для считывания всех данных из респонса
                // using чтобы убрать все используемые им ресурсы, наследуется от класса, который реализует интерфейс
                {
                    response = streamReader.ReadToEnd();
                    //обрабатываем json в сущность с#, десереализуем строку и превращаем в объект
                }
                WeatherResponce weatherResponse=JsonConvert.DeserializeObject<WeatherResponce>(response);
                Console.WriteLine($"Temperature in {weatherResponse.Name}: {weatherResponse.main.Temp}");

                if  (weatherResponse.Name != "" || weatherResponse.sys.Country != "")
                {
                    Weather weather = new Weather(weatherResponse);
                    weathers.Add(weather);
                    Console.WriteLine($"Temperature in {weather.Name}: {weather.Temp}");
                    n++;

                }
                var linqMaxTemp = weathers.MaxBy(weather=>weather.Temp).Country;
                Console.WriteLine($"страна с самиой высокой температурой: {linqMaxTemp}");
                var linqMinTemp = weathers.MinBy(weather => weather.Temp).Country;
                Console.WriteLine($"страна с самиой низкой температурой: {linqMinTemp}");

                /*  var linqDescription1 = from weather in weathers
                                         where weather.Description == "clear sky" ||
                                         weather.Description == "rain" ||
                                         weather.Description == "few clouds"
                                         select weather;*/
                var linqCountry= weathers.Select(weather=>weather.Country).Distinct().ToList();
                Console.WriteLine($"уникальных стран: {linqCountry.Count}");

                var linqAverTemp = weathers.Average(weather => weather.Temp);
                Console.WriteLine($"средняя температура в мире: {linqAverTemp}");

                var linqDescription = weathers.FirstOrDefault(weather => weather.Description == "clear sky" ||
                                      weather.Description == "rain" ||
                                      weather.Description == "few clouds");
                Console.WriteLine($"Первая страна с определёнными значениями Description: {linqDescription.Country}, " +
                    $"{linqDescription.Name}, " +
                    $"discr: {linqDescription.Description}");




            }
            Console.WriteLine();

        }

    }

}