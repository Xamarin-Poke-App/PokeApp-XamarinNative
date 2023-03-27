using System;
using SharedCode.Database;
using SQLite;

namespace SharedCode.Util
{
	public class Singleton
	{
		private Singleton() { }
		private static SQLiteConnection _DBConnection;

		public static SQLiteConnection GetDBConnectionInstance(IPathManager pathManager)
		{
			if (_DBConnection == null)
			{
				_DBConnection = new SQLiteConnection(pathManager.GetPath());
			}
			return _DBConnection;
		}
	}
}
