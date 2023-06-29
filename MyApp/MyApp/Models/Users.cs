using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.Models
{
	public class Users
	{
		[PrimaryKey, NotNull]
		public string Email { get; set; }
		[NotNull]
		public string Password { get; set; }
		[NotNull]
		public string Name { get; set; }
		[NotNull]
        public string PhoneNumber { get; set; }
	}
}
