using System;
using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public class EventPhoto
	{
		public EventPhoto()
		{
		}

		public ImageSource ImageSource
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public DateTime DateTaken
		{
			get;
			set;
		}
	}
}

