using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GiggleGaggleApp
{
	public static class SettingsService
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		public static string Username
		{
			get 
			{ 
				return AppSettings.GetValueOrDefault<string>("Username", string.Empty); 
			}
			set 
			{ 
				AppSettings.AddOrUpdateValue<string>("Username", value); 
			}
		}

		public static bool IsLoggedIn
		{
			get
			{
				return AppSettings.GetValueOrDefault<bool>("IsLoggedIn", false);
			}
			set
			{
				AppSettings.AddOrUpdateValue<bool>("IsLoggedIn", value);
			}
		}

		public static OAuthSettings OAuthSettings
		{
			get
			{
				return DeserializeObject<OAuthSettings>(AppSettings.GetValueOrDefault("OAuthSettings", ""));
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>("OAuthSettings", SerializeObject(value));
			}
		}

		public static User CurrentUser
		{
			get
			{
				return DeserializeObject<User>(AppSettings.GetValueOrDefault<string>("CurrentUser", string.Empty));
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>("CurrentUser", SerializeObject<User>(value));
			}
		}

		private static string SerializeObject<T>(T value) where T : class
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (StringWriter sw = new StringWriter())
			using (XmlWriter xmlw = XmlWriter.Create(sw))
			{
				serializer.Serialize(xmlw, value);
				return sw.ToString();
			}
		}

		private static T DeserializeObject<T>(string stringValue) where T : class
		{
			var serializer = new XmlSerializer(typeof(T));

			object result;

	        using (TextReader reader = new StringReader(stringValue))
	        {
	            result = serializer.Deserialize(reader);
	        }
			return (T)result;
		}
	}
}

