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
		//private string BaseUrl = "http://api.openweathermap.org/data/2.5/forecast?q=Brisbane&APIKEY=";

		//public async Task<HttpResponseMessage> GetWeatherForecast(string url)
		//{
		//	BaseResponse b = new BaseResponse();

		//	try
		//	{
		//		var httpClient = new HttpClient();
		//		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
		//		b.response = await httpClient.SendAsync(request);

		//		if (b.response.StatusCode == HttpStatusCode.OK)
		//		{
		//			return b;
		//		}
		//		else if (b.response.StatusCode == HttpStatusCode.NotFound)
		//		{
		//			b.Errors.Add("Not Found");
		//			return b;
		//		}
		//		else
		//		{
		//			return b;
		//		}
		//	}
		//	catch (HttpRequestException ex)
		//	{
		//		b.Errors.Add(ex.Message);
		//		return b;
		//	}
		//}
	}
}
