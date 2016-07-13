using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GiggleGaggleApp
{
	public class BaseResponse
	{
		public List<string> Errors;

		public BaseResponse()
		{
			Errors = new List<String>();
		}
	}
}

