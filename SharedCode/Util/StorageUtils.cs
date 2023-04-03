using System;
using SharedCode.Interfaces;

namespace SharedCode.Util
{

	public class StorageUtils: IStorageUtils
	{
        private IStorage Storage;

        public StorageUtils(IStorage storage)
        {
            this.Storage = storage;
        }

        public bool GetIsLoggedIn()
        {
            return Storage.Read("isLoggedIn", false);
        }

        public void SetIsLoggedIn(bool value)
        {
            Storage.Store("isLoggedIn", value);
        }
    }
}

