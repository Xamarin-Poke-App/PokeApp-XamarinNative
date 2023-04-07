using System;
using System.Collections.Generic;
using SQLite;
using SharedCode.Util;
using System.Threading.Tasks;

namespace SharedCode.Database
{
	public class DatabaseManager : IDatabaseManager
	{
        private IPathManager path;
        private SQLiteAsyncConnection db;

        public DatabaseManager(IPathManager pathManager)
		{
			this.path = pathManager;
            this.db = new SQLiteAsyncConnection(pathManager.GetPath());
        }

        public async Task<bool> checkTableExistsAsync(string tableName)
		{
			try
			{
                var tableInfo = await db.GetTableInfoAsync(tableName);
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

		public async Task StoreDataAsync<T>(T newItem, string tableName) where T : new()
        {
            try
			{
                if (await checkTableExistsAsync(tableName))
				{
                    await db.InsertAsync(newItem);
                } else {
					await db.CreateTableAsync<T>();
					await db.InsertAsync(newItem);
				}
            } catch(Exception e) {
				Console.WriteLine("Error: " + e);
			}
        }

		public async Task UpdateDataAsync<T>(T updateItem, string tableName) where T : new()
        {
            try
            {
                if (await checkTableExistsAsync(tableName))
				{
                    await db.UpdateAsync(updateItem);
                }
                else {
                    await db.CreateTableAsync<T>();
                    await db.InsertAsync(updateItem);
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }

		public async Task DeleteDataByIdAsync<T>(T item, string tableName, int id) where T : new()
        {
			try
			{
                if (await checkTableExistsAsync(tableName))
					await db.DeleteAsync<T>(id);
			} catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }


        public async Task<Result<List<T>>> GetAllDataAsync<T>() where T : new()
		{
            List<T> typeList = new List<T>();
            try
            {
                var data = await db.Table<T>().ToListAsync();
                foreach (var res in data)
                {
                    typeList.Add(res);
                }
                return Result.Ok<List<T>>(typeList);
            } catch {
                return Result.Fail<List<T>>("Could not get all data");
            }
        }

        public async Task<Result<T>> GetDataByIdAsync<T>(int id, string tableName) where T : IGenericId, new()
        {
            try
            {
                var data = await db.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
                return Result.Ok<T>(data);
            }
            catch
            {
                return Result.Fail<T>("Could not get data");
            }
        }
	}
}
