using System;

namespace GiggleGaggleApp
{
	public class Event
	{
		public Event ()
		{
			
		}

		public string Title {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public DateTime Date {
			get;
			set;
		}

		public DateTime Time
		{
			get;
			set;
		}

		public Forecast forecast
		{
			get;
			set;
		}

		public rideType RideType
		{
			get;
			set;
		}

		public elevation Elevation
		{
			get;
			set;
		}

		public string MeetingLocation
		{
			get;
			set;
		}

		public string OrganiserName
		{
			get;
			set;
		}

		public enum rideType
		{
			Casual,
			Tempo,
			Fast
		}

		public enum elevation
		{
			Flat,
			Undulating,
			Hilly,
			Mountain
		};


	}
}

