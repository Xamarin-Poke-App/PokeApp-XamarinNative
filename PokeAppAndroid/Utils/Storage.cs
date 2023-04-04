using System;
using Android.App;
using Android.Content;
using SharedCode.Interfaces;

namespace PokeAppAndroid.Utils
{
    public class Storage : IStorage
    {
        ISharedPreferences pref;
        ISharedPreferencesEditor editor;

        public Storage()
        {
            pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            editor = pref.Edit();
        }

        public string Read(string key, string defaultValue)
        {
            return pref.GetString(key, defaultValue);
        }

        public int Read(string key, int defaultValue)
        {
            return pref.GetInt(key, defaultValue);
        }

        public bool Read(string key, bool defaultValue)
        {
            return pref.GetBoolean(key, defaultValue);
        }

        public void Remove(string key)
        {
            editor.Remove(key);
            editor.Apply();
        }

        public void Store(string key, bool value)
        {
            editor.PutBoolean(key, Convert.ToBoolean(value));
            editor.Apply();
        }

        public void Store(string key, string value)
        {
            editor.PutString(key, Convert.ToString(value));
            editor.Apply();
        }

        public void Store(string key, float value)
        {
            editor.PutFloat(key, (float)Convert.ToDouble(value));
            editor.Apply();
        }

        public void Store(string key, int value)
        {
            editor.PutInt(key, Convert.ToInt16(value));
            editor.Apply();
        }
    }
}

