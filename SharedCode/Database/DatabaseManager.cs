using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

namespace SharedCode.Database
{
	public class DatabaseManager
	{
        IPathManager path;

        public DatabaseManager(IPathManager pathManager)
		{
			this.path = pathManager;
		}

		private bool checkTableExists(string tableName)
		{
			try
			{
                var db = Util.Singleton.GetDBConnectionInstance(path);
                var tableInfo = db.GetTableInfo(tableName);
                if(tableInfo.Count > 0)
				{
					return true;
				} else {
					return false;
				}
            } catch {
				return false;
			}
            
        }

		public void StoreData<T>(ref T model, string tableName)
        {
            try
			{
                var db = Util.Singleton.GetDBConnectionInstance(path);
                if (checkTableExists(tableName))
				{
                    db.Insert(model);
                } else {
					db.CreateTable<T>();
					db.Insert(model);
				}
            } catch(Exception e) {
				Console.WriteLine("Error:" + e);
			}
		}

		public void UpdateData<T>(ref T model, string tableName)
		{
            try
            {
                var db = Util.Singleton.GetDBConnectionInstance(path);
                if (checkTableExists(tableName))
				{
                    db.Update(model);
                }
                else {
                    var a = db.CreateTable<T>();
                    db.Insert(model);
                }
                
            }
            catch (Exception e) {
                Console.WriteLine("Error:" + e);
            }
        }

		public void DeleteDataById<T>(ref T model, string tableName, int id)
		{
			try
			{
                var db = Util.Singleton.GetDBConnectionInstance(path);
				if (checkTableExists(tableName))
					db.Delete<T>(id);
			} catch (Exception e) {
                Console.WriteLine("Error:" + e);
            }
        }
	}
}
