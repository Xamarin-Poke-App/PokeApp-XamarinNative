using System;
using SharedCode.Model;
using SharedCode.Util;

namespace SharedCode.Controller
{
	public class LoginController
	{
		public static Result DoLogin(User user)
		{
			if (user.Email == "test@test.com" && user.Password == "tester")
				return Result.Ok();
            return Result.Fail("Invalid Credentials");
		}
	}
}

