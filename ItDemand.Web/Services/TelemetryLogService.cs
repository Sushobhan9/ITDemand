using Dapper;
using ItDemand.Domain.DataContext;
using ItDemand.Web.Controllers;
using ItDemand.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ItDemand.Web.Services
{
    public class TelemetryLogService
    {
        private readonly ItDemandContext _db;
        private readonly string _connectionString;

        public TelemetryLogService(ItDemandContext dbContext)
        {
            _db = dbContext;
            _connectionString = _db.Database.GetDbConnection().ConnectionString;
        }

        public int GetTotalCount()
        {
            // https://stackoverflow.com/questions/28916917/sql-count-rows-in-a-table
            var sql = @"
                select sum([rows])
                from sys.partitions
                where object_id=object_id('ApplicationLog')
                and index_id in (0,1)";

            using var conn = new SqlConnection(_connectionString);
            var result = conn.QueryFirstOrDefault<int>(sql);
            return result;
        }

        public async Task<IEnumerable<ApplicationLogEntry>> GetLogEntries(
            int offset, int pageSize, string sortColumn, string sortDirection, string searchValue)
        {
            var sql = @$"
                select Id, 
                       Text, 
                       Category, 
                       EntryDate, 
                       Type, 
                       UserAccountName, 
                       UserDisplayName, 
                       UserRegion, 
                       UserBusinessUnit,
                       Url,
                       Browser,
                       HostAddress
                from ApplicationLog
                where Text like '%{searchValue}%' or UserDisplayName like '%{searchValue}%'
                order by {sortColumn} {sortDirection}
                offset {offset} rows
                fetch next {pageSize} rows only";

            using var conn = new SqlConnection(_connectionString);
            var result = await conn.QueryAsync<ApplicationLogEntry>(sql);
            return result;
        }

        public async Task<int> CreateLogEntry(ApplicationLogEntry logEntry)
        {
            var sql =
                @$"INSERT INTO ApplicationLog (
                    [Text]
                   ,[Category]
                   ,[EntryDate]
                   ,[Type]
                   ,[UserAccountName]
                   ,[UserDisplayName]
                   ,[UserRegion]
                   ,[UserBusinessUnit]
                   ,[Url]
                   ,[Browser]
                   ,[HostAddress]) 
                VALUES(
                    '{logEntry.Text}'
                   ,'{logEntry.Category}'
                   ,'{logEntry.EntryDate}'
                   ,'{logEntry.Type}'
                   ,'{logEntry.UserAccountName}'
                   ,'{logEntry.UserDisplayName}'
                   ,'{logEntry.UserRegion}'
                   ,'{logEntry.UserBusinessUnit}'
                   ,'{logEntry.Url}'
                   ,'{logEntry.Browser}'
                   ,'{logEntry.HostAddress}')";

            using var conn = new SqlConnection(_connectionString);
            SqlCommand command = new(sql, conn);
            command.Connection.Open();
            var result = await command.ExecuteNonQueryAsync();
            return result;
        }
    }
}
