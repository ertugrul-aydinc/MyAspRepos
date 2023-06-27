using _5___Models;
using _5___ServiceContracts;

namespace _5___Services;

public class CityWeathersService : ICityWeathersService
{
    private readonly List<CityWeather> _cityWeathers;

    public CityWeathersService()
    {
        _cityWeathers = new List<CityWeather>()
            {
                new CityWeather{CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
                new CityWeather{CityUniqueCode = "NYC", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
                new CityWeather{CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82}
            };
    }


    public CityWeather GetCityWeatherByCityCode(string cityCode)
    {
        CityWeather? searchingCityWeather = _cityWeathers.Where(cw => cw.CityUniqueCode == cityCode).FirstOrDefault();

        if (searchingCityWeather is null)
            return new CityWeather();

        return searchingCityWeather;
    }

    public List<CityWeather> GetCityWeathers()
    {
        return _cityWeathers;
    }

}

