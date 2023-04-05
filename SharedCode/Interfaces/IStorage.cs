using System;
namespace SharedCode.Interfaces
{
	public interface IStorage
	{
        void Store(string key, bool value);

        void Store(string key, string value);

        void Store(string key, float value);

        void Store(string key, int value);

        string Read(string key, string defaultValue);

        int Read(string key, int defaultValue);

        bool Read(string key, bool defaultValue);

        void Remove(string key);
    }
}

