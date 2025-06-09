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
        private const int THOI_GIAN_HUY_BO = 10;

        private readonly CancellationTokenSource _cancellationTokenSource = new(TimeSpan.FromSeconds(30));
        private readonly List<SqlParameter> _params = [];
        private readonly string _nameOfProcedure;
        private SqlConnection? _connection;
        private readonly ILogger<DatabaseReader> _logger;

        public DatabaseReader(string nameOfProcedure)
        {
            _nameOfProcedure = nameOfProcedure;
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<DatabaseReader>();
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                string? connStr = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build()
                    .GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connStr))
                {
                    _logger.LogError("Connection string is null or empty.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(_nameOfProcedure))
                {
                    _logger.LogError("Procedure name is not provided.");
                    return;
                }

                _connection = new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing DatabaseReader.");
            }
        }

        public void SqlParams(string nameOfParam, SqlDbType sqlType, object value)
        {
            _params.Add(new SqlParameter(nameOfParam, sqlType) { Value = value });
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            if (_connection == null)
            {
                _logger.LogError("Connection is not initialized.");
                return -1;
            }

            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                    await _connection.OpenAsync(_cancellationTokenSource.Token);

                using var transaction = _connection.BeginTransaction();
                using var command = _connection.CreateCommand();

                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = _nameOfProcedure;
                command.Parameters.AddRange(_params.ToArray());
                command.CommandTimeout = THOI_GIAN_HUY_BO;

                int result = await command.ExecuteNonQueryAsync();
                transaction.Commit();
                return result;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"SQL error during ExecuteNonQueryAsync (SP: {_nameOfProcedure})");

                try { _connection?.BeginTransaction()?.Rollback(); }
                catch (Exception rollbackEx)
                {
                    _logger.LogWarning(rollbackEx, "Rollback failed.");
                }

                return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during ExecuteNonQueryAsync");
                return -1;
            }
            finally
            {
                Dispose();
            }
        }

        public async Task<object?> ExecuteScalarAsync()
        {
            if (_connection == null)
            {
                _logger.LogError("Connection is not initialized.");
                return null;
            }

            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                    await _connection.OpenAsync(_cancellationTokenSource.Token);

                using var command = _connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = _nameOfProcedure;
                command.Parameters.AddRange(_params.ToArray());
                command.CommandTimeout = THOI_GIAN_HUY_BO;

                return await command.ExecuteScalarAsync();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"SQL error during ExecuteScalarAsync (SP: {_nameOfProcedure})");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during ExecuteScalarAsync");
                return null;
            }
            finally
            {
                Dispose();
            }
        }

        public async Task<SqlDataReader?> ExecuteReaderAsync()
        {
            if (_connection == null)
            {
                _logger.LogError("Connection is not initialized.");
                return null;
            }

            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                    await _connection.OpenAsync(_cancellationTokenSource.Token);

                var command = _connection.CreateCommand(); // intentionally not using
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = _nameOfProcedure;
                command.Parameters.AddRange(_params.ToArray());
                command.CommandTimeout = THOI_GIAN_HUY_BO;

                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"SQL error during ExecuteReaderAsync (SP: {_nameOfProcedure})");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during ExecuteReaderAsync");
                return null;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                try
                {
                    if (_connection.State != System.Data.ConnectionState.Closed)
                        _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to close SQL connection.");
                }

                _connection.Dispose();
                _connection = null;
            }
        }

    }
}
