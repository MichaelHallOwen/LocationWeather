using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LocationWeather.Models;

namespace LocationWeather.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        [HttpGet("[action]/{city}")]
        public async Task<IActionResult> City(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=e344ddec8249fbd3d70f3154736edb4d&units=imperial");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var resWeather = JsonConvert.DeserializeObject<WeatherResponse>(stringResult);
                    return Ok(new
                    {
                        Temp = Math.Round(decimal.Parse(resWeather.Main.Temp)),
                        Summary = string.Join(",", resWeather.Weather.Select(x => x.Main)),
                        City = resWeather.Name
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }
        
    }
}
