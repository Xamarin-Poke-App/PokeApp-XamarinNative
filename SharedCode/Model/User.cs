using System;
namespace SharedCode.Model
{
	public class User
	{
		public string Email;
		public string Password;

		public User(string UserEmail, string UserPassword)
		{
			Email = UserEmail;
			Password = UserPassword;
		}
	}
}

