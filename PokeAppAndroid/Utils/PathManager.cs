using System;
using SharedCode.Database;
using System.IO;

namespace PokeAppAndroid.Utils
{
	public class PathManager : IPathManager
	{
        private static string sqliteFilename = "PokeAppDatabase.db3";
        private static string libraryPath;

        public PathManager() {}

        public string GetPath()
        {
            libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }
    }
}
