using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.Dapper
{
    public class Dapper : IDapper
    {
        private readonly IConfiguration _config;

        public Dapper(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }

        public DbConnection GetDbConnection()
        {
            try
            {
                var connectionString = _config.GetConnectionString("EWalletConnection");
                if (!string.IsNullOrEmpty(connectionString)) return new SqlConnection(connectionString);
                
                Console.Write("Dapper: 'EWalletConnection' Config dosyasından okunamadı!");
                throw new Exception("Dapper: 'EWalletConnection' Config dosyasından okunamadı!");
            }
            catch (Exception ex)
            {
                Console.Write("Dapper Connection Error :", ex);
                throw ex;
            }
        }

        public async Task<T> GetAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryAsync<T>(sql, parameters, commandType: commandType).ConfigureAwait(false);
        }

        public async Task<T> ExecuteAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = GetDbConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType).ConfigureAwait(false);
        }

        public async Task<T> InsertAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: tran).ConfigureAwait(false);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<T> UpdateAsync<T>(string sql, DynamicParameters parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = GetDbConnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType, transaction: tran).ConfigureAwait(false);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
