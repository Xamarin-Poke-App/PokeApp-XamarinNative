using System;
using System.IO;
using SharedCode.Database;

namespace PokeAppiOS.Utils
{
	public class PathManager : IPathManager
	{
        private static string sqliteFilename = "PokeAppDatabase.db3";
        private static string libraryPath;
        public PathManager() {}

        public string GetPath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }
    }
}
