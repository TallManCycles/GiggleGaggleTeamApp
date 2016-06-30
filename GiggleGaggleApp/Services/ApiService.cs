using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace GiggleGaggleApp
{
	public class ApiService
	{
		private string BaseUrl = "http://api.openweathermap.org/data/2.5/forecast?q=Brisbane&APIKEY=";

		public static async Task<HttpResponseMessage> GetWeatherForecast(string url)
		{
			BaseResponse b = new BaseResponse();

			try
			{
				var httpClient = new HttpClient();
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
				b.response = await httpClient.SendAsync(request);

				if (b.response.StatusCode == HttpStatusCode.OK)
				{
					return b;
				}
				else if (b.response.StatusCode == HttpStatusCode.NotFound)
				{
					b.Errors.Add("The weather was not found");
					return b;
				}
				else
				{
					return b;
				}
			}
			catch (HttpRequestException ex)
			{
				b.Errors.Add(ex.Message);
				return b;
			}
		}

		public async void GetForecast()
		{
			var data = await GetWeatherForecast(BaseUrl);

			if (data != null)
			{
				try
				{
					var serializer = new DataContractJsonSerializer(typeof(Forecast));
					using (var stream = new MemoryStream())
					{
						var response = (Forecast)serializer.ReadObject(stream);
						var main = response.message;
					}
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}

	}
}
