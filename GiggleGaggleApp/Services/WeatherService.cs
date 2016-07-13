using System;
using System.IO;
using System.Runtime.Serialization.Json;
using WeatherNet;
using WeatherNet.Clients;
using WeatherNet.Model;
using WeatherNet.Util.Api;

namespace GiggleGaggleApp
{
	public class WeatherService
	{
		public WeatherService()
		{
			ClientSettings.SetApiKey("4eda855eff41a8daf3dcd10e14c93a82");
		}

		public Result<FiveDaysForecastResult> GetFiveDayForcast()
		{
			return FiveDaysForecast.GetByCityName("Brisbane", "Australia");
		}

		public SingleResult<CurrentWeatherResult> GetCurrentWeather()
		{
			return CurrentWeather.GetByCityName("Brisbane", "Australia", "en", "metric");
		}

		public Result<SixteenDaysForecastResult> GetSixteenDayForcast()
		{
			return SixteenDaysForecast.GetByCityName("Brisbane", "Australia", 16);
		}

	}
}

