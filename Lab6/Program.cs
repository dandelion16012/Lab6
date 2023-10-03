using Newtonsoft.Json;
using System.Net;
using Lab6;

struct Weather
{
    public string Country {  get; set; }
    public string Name { get; set; }
    public double Temp { get; set; }
    public string Discription {  get; set; }
   

}

namespace lab6
{
    class program
    {
        static void Main(string[] args)
        {         

            List<Weather> weatherData = new List<Weather>();

            Random random = new Random();

            int count = 0;
            //while (count < 50)
            {
                double latitude= -90+random.NextDouble()*(90+90);
                double longitude = -180 + random.NextDouble() * (180 + 180);
                string Key = "9efa86170885592ef3b1450c671eb1bc";
                //exclude- исключаем ненужные параметры, metric- используем стандартные единицы измерения
                string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + latitude.ToString() + "&" +"lon=" + longitude.ToString() + "&exclude=minutely,hourly,daily,alerts&" +"units=metric&appid=" + Key;
                             
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);// создаём объект реквеста для возможности запроса
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();// получаем респонс, считываем данные с реквеста
                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))// для считывания всех данных из респонса
                // using чтобы убрать все используемые им ресурсы, наследуется от класса, который реализует интерфейс
                {
                    response = streamReader.ReadToEnd();
                    //обрабатываем json в сущность с#, дисереализуем строку и превращаем в объект
                }
                WeatherResponce weatherResponce=JsonConvert.DeserializeObject<WeatherResponce>(response);
                Console.WriteLine($"Temperature in {weatherResponce.Name}: {weatherResponce.main.Temp}");
                count++;
            }

        }
    }
}