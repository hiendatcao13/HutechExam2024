using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.Common;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Hutech.Exam.Server.DAL.DataReader
{
    public class DatabaseReader : IDisposable
    {
        private static readonly int THOI_GIAN_HUY_BO = 10; // (giây), là n giây thực hiện procedure sẽ được hủy bỏ sau n giây đó
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30)); // (giây), là n giây connection pool tồn tại và sẽ được hủy bỏ sau n giây đó
        private string? _connectionString { get; set; }
        private IConfiguration? _configuration { get; set; }
        private List<SqlParameter> _params { get; set; }
        private string _nameOfProcedure { get; set; }
        private SqlConnection? connection { get; set; }

        public DatabaseReader(string nameOfProcedure)
        {
            _params = new List<SqlParameter>();
            _nameOfProcedure = nameOfProcedure;
            Initalize(nameOfProcedure);
            connection = new SqlConnection(_connectionString);
        }
        private void Initalize(string nameOfProcedure)
        {
            configureConnectionString();
            if (_connectionString != null)
                checkError(nameOfProcedure, _connectionString);
        }
        private void configureConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
                _connectionString = connectionString;
        }
        private void checkError(string nameOfProcedure, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(nameOfProcedure))
            {
                throw new ArgumentNullException(nameof(nameOfProcedure));
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
        }
        public void SqlParams(string nameOfParam, SqlDbType sqltype, object value)
        {
            SqlParameter parameter = new SqlParameter(nameOfParam, sqltype)
            {
                Value = value
            };
            _params.Add(parameter);
        }
        // return lines
        public async Task<int> ExcuteNonQuery()
        {
            if (connection == null)
                throw new InvalidOperationException("Connection is not initialized.");

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync(_cancellationTokenSource.Token);
            }

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = _nameOfProcedure;
                        command.Parameters.AddRange(_params.ToArray());
                        command.CommandTimeout = THOI_GIAN_HUY_BO;
                        int result = await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return result;
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 208)
                    {
                        throw new Exception($"Procedure: {_nameOfProcedure} not found in SQL Server", ex);
                    }
                    transaction.Rollback();
                    throw new Exception("An error occurred: " + ex.Message, ex);
                }
                finally
                {
                    Dispose();
                }
            }
        }
        // return the first value in the first line (id)
        public async Task<object?> ExecuteScalar()
        {
            if (connection == null)
                throw new InvalidOperationException("Connection is not initialized.");

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync(_cancellationTokenSource.Token);
            }

            try
            {
                SqlCommand command = connection.CreateCommand(); // ❌ Không dùng `using`
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = _nameOfProcedure;
                command.Parameters.AddRange(_params.ToArray());
                command.CommandTimeout = THOI_GIAN_HUY_BO;

                return await command.ExecuteScalarAsync();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208)
                {
                    throw new Exception($"Procedure: {_nameOfProcedure} not found in SQL Server", ex);
                }
                throw new Exception("An error occurred: " + ex.Message, ex);
            }
        }

        public async Task<SqlDataReader> ExecuteReader()
        {
            if (connection == null)
                throw new InvalidOperationException("Connection is not initialized.");

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync(_cancellationTokenSource.Token);
            }

            try
            {
                SqlCommand command = connection.CreateCommand(); // ❌ Không dùng `using` ở đây
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = _nameOfProcedure;
                command.Parameters.AddRange(_params.ToArray());
                command.CommandTimeout = THOI_GIAN_HUY_BO;

                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208)
                {
                    throw new Exception($"Procedure: {_nameOfProcedure} not found in SQL Server", ex);
                }
                throw new Exception("An error occurred: " + ex.Message, ex);
            }
        }
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }
    }
}
