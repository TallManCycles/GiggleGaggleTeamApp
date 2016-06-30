using System;
using System.Runtime.Serialization;

namespace GiggleGaggleApp
{
	[DataContract]
	public class Forecast
	{
		public Forecast()
		{
			//api.openweathermap.org / data / 2.5 / forecast / daily ? q = Brisbane & cnt = 1 & APIKEY = 4eda855eff41a8daf3dcd10e14c93a82
		}

		[DataMember(Name="message")] 
		public string message
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public int Degrees
		{
			get;
			set;
		}
	}
}


//"city": {
//    "id": 2174003,
//    "name": "Brisbane",
//    "coord": {
//      "lon": 153.028091,
//      "lat": -27.467939
//    },
//    "country": "AU",
//    "population": 0
//  },
//  "cod": "200",
//  "message": 0.0514,
//  "cnt": 1,
//  "list": [
//    {
//      "dt": 1466643600,
//      "temp": {
//        "day": 289.56,
//        "min": 289.09,
//        "max": 289.56,
//        "night": 289.09,
//        "eve": 289.56,
//        "morn": 289.56
//      },
//      "pressure": 1012.95,
//      "humidity": 93,
//      "weather": [
//        {
//          "id": 500,
//          "main": "Rain",
//          "description": "light rain",
//          "icon": "10n"
//        }
//      ],
//      "speed": 1.43,
//      "deg": 228,
//      "clouds": 88
//    }

