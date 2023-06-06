namespace WebCoreProje
{
 
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "db4c6dde15f648e8a5324bf8df0db11d"; // OpenWeatherMap API anahtarını buraya girin

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        /*
         const apiKey = "db4c6dde15f648e8a5324bf8df0db11d"; 
const searchQuery = "Turkey";

const url = `https://api.opencagedata.com/geocode/v1/json?q=${searchQuery}&countrycode=tr&limit=100&key=${apiKey}`;
        
        $"https://api.opencagedata.com/geocode/v1/json?q={searchQuery}&countrycode=tr&limit=100&key={apiKey}";
         */
        public async Task<List<City>> GetCitiesAsync()
        {
            
           var  searchQuery = "Turkey";
            var url = $"https://api.opencagedata.com/geocode/v1/json?q={searchQuery}&countrycode=tr&limit=100&key={ApiKey}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var data = JsonSerializer.Deserialize<WeatherApiResponse>(content, options);

                return data?.List;
            }

            // API'den geçerli bir yanıt alınamadığında null döndürülür veya gerekli hata işleme mekanizması eklenir
            return null;
        }
    }

    public class WeatherApiResponse
    {
        public List<City> List { get; set; }
    }

}
