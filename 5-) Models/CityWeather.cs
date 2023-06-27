﻿namespace _5___Models;

public class CityWeather
{
    public string CityUniqueCode { get; set; } = null!;
    public string CityName { get; set; } = null!;
    public DateTime? DateAndTime { get; set; }
    public int? TemperatureFahrenheit { get; set; }
}

