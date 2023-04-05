using System;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedCode.Database
{
	public interface IDatabaseManager
	{
        Task<bool> checkTableExistsAsync(string tableName);
        Task StoreDataAsync<T>(T newItem, string tableName) where T : new();
        Task UpdateDataAsync<T>(T updateItem, string tableName) where T : new();
        Task DeleteDataByIdAsync<T>(T item, string tableName, int id) where T : new();
        Task<Result<List<T>>> GetAllDataAsync<T>() where T : new();
    }
}

