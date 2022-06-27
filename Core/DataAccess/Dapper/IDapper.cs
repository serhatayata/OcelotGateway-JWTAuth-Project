using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace Core.DataAccess.Dapper
{
    public interface IDapper: IDisposable
    {
        DbConnection GetDbConnection();
        Task<T> GetAsync<T>(string sql, DynamicParameters parameters, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> GetAllAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.Text);
        Task<T> ExecuteAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure);
        Task<T> InsertAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure);
        Task<T> UpdateAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure);
    }
}
