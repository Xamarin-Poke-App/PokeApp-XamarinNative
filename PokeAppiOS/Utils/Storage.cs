using System;
using Foundation;
using SharedCode.Interfaces;

namespace PokeAppiOS.Utils
{
    public class Storage: IStorage
    {
        NSUserDefaults user;

        public Storage()
        {
            user = NSUserDefaults.StandardUserDefaults;
        }

        public string Read(string key, string defaultValue)
        {
            return user.StringForKey(key) ?? defaultValue;
        }

        public int Read(string key, int defaultValue)
        {
            return (int)user.IntForKey(key);
        }

        public bool Read(string key, bool defaultValue)
        {
            return user.BoolForKey(key);
        }

        public void Remove(string key)
        {
            user.RemoveObject(key);
        }

        public void Store(string key, bool value)
        {
            user.SetBool(Convert.ToBoolean(value), key);
        }

        public void Store(string key, string value)
        {
            user.SetString(Convert.ToString(value), key);
        }

        public void Store(string key, float value)
        {
            user.SetFloat((float)Convert.ToDouble(value), key);
        }

        public void Store(string key, int value)
        {
            user.SetInt(Convert.ToInt16(value), key);
        }
    }
}
