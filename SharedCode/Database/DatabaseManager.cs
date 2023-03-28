using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using SharedCode.Util;

namespace SharedCode.Database
{
	public class DatabaseManager : IDatabaseManager
	{
        private IPathManager path;

        public DatabaseManager(IPathManager pathManager)
		{
			this.path = pathManager;
        }

        public bool checkTableExists(string tableName)
		{
			try
			{
                var db = Util.Singleton.GetDBConnectionInstance(this.path);
                var tableInfo = db.GetTableInfo(tableName);
                if (tableInfo.Count > 0)
				{
					return true;
				} else {
					return false;
				}
            } catch {
				return false;
			}
        }

		public void StoreData<T>(T newItem, string tableName) where T : new()
        {
            try
			{
                var db = Util.Singleton.GetDBConnectionInstance(this.path);
                if (checkTableExists(tableName))
				{
                    db.Insert(newItem);
                } else {
					db.CreateTable<T>();
					db.Insert(newItem);
				}
            } catch(Exception e) {
				Console.WriteLine("Error: " + e);
			}
        }

		public void UpdateData<T>(T updateItem, string tableName) where T : new()
        {
            try
            {
                var db = Util.Singleton.GetDBConnectionInstance(this.path);
                if (checkTableExists(tableName))
				{
                    db.Update(updateItem);
                }
                else {
                    db.CreateTable<T>();
                    db.Insert(updateItem);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }

		public void DeleteDataById<T>(T item, string tableName, int id) where T : new()
        {
			try
			{
                var db = Util.Singleton.GetDBConnectionInstance(this.path);
                if (checkTableExists(tableName))
					db.Delete<T>(id);
			} catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }


        public Result<List<T>> GetAllData<T>() where T : new()
		{
            var db = Util.Singleton.GetDBConnectionInstance(this.path);
            List<T> typeList = new List<T>();
            try
            {
                var data = db.Table<T>();
                foreach (var res in data)
                {
                    typeList.Add(res);
                }
                return Result.Ok<List<T>>(typeList);
            } catch {
                return Result.Fail<List<T>>("Could not get all data");
            }
        }
	}
}
