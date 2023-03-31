using System;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Services
{
	// Note: If this class get some changes that includes dependencies, register this class and its dependecies in Ioc Container to manage DI
	public class LoginService
	{
		public static LoginService shared = new LoginService();

        public event EventHandler<Result<string>> UserLoggedIn;

        public void PerformLogin(User user)
		{
			if (user.Email == "test@test.com" && user.Password == "tester")
				OnLoginCompleted(Result.Ok<string>(user.Email));
			else
				OnLoginCompleted(Result.Fail<string>("Invalid Credentials"));
		}

		protected virtual void OnLoginCompleted(Result<string> result)
		{
			UserLoggedIn?.Invoke(this, result);
		}
	}
}

