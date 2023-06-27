using _5___Models;

namespace _5___ServiceContracts;

public interface ICityWeathersService
{
    List<CityWeather> GetCityWeathers();
    CityWeather GetCityWeatherByCityCode(string cityCode);
}

