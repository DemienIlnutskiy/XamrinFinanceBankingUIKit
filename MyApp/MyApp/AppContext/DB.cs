using MyApp.Models;
using PhoneNumbers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace MyApp.AppContext
{
	public class DB
	{
		private readonly SQLiteConnection _connection;
		private string userName, email, password;

		public DB(string path)
		{
			_connection = new SQLiteConnection(path);
			_connection.CreateTable<Users>();
			
		}

		public List<Users> GetUsers()
		{
			try
			{
				return _connection.Table<Users>().ToList();
			}
			catch { return null; }
		}

		public bool LoginUser(string userEmail, string userPassword)
		{
			foreach( var user in _connection.Table<Users>().ToList())
			{
				if(user.Email==userEmail&&user.Password==userPassword)
					return true;
			}
			return false;
		}

		public bool RegisterUser(string userName, string userEmail, string Password)
		{
			try 
			{ 
				this.userName = userName;
				email = userEmail;
				this.password = Password;
				return true;
			}
			catch { return false; }
		}

		public bool InsertPhoneNumber(string phoneNumber)
		{
			try
			{
				_connection.Insert(new Users { Name = userName, Email = email, Password = password, PhoneNumber = phoneNumber });
				return true;
			}
			catch 
			{
				return false;
			}
		}
		public bool UpdatePasword(string pasword,string emailAddress)
		{
			try
			{
				var user = _connection.Get<Users>(emailAddress);
				_connection.Update(new Users { Name = user.Name, Email = user.Email, Password = pasword, PhoneNumber = user.PhoneNumber });
				return true;
			}
			catch { return false; }
		}
	}
}
