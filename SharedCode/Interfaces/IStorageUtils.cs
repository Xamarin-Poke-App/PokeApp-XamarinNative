using System;
namespace SharedCode.Interfaces
{
	public interface IStorageUtils
	{
        bool GetIsLoggedIn();

        void SetIsLoggedIn(bool value);
    }
}

