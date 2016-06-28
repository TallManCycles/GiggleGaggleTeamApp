using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GiggleGaggleApp
{
	public partial class UserDetails : ContentPage
	{
		public UserDetails()
		{
			InitializeComponent();

			if (App.User != null)
				this.BindingContext = App.User;
		}
	}
}

