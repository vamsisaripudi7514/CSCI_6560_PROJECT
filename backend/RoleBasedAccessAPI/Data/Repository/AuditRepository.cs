using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RoleBasedAccessAPI.Data.Model;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace RoleBasedAccessAPI.Data.Repository
{
    public class AuditRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> sp_audit_logs(SelectAuditLogs data)
        {
            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_select_audit_logs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = data.employeeID });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var result = new List<Dictionary<string, object>>();
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            result.Add(row);
                        }

                        if (result.Count == 1 && result[0].ContainsKey("Message"))
                        {
                            return result[0]["Message"];
                        }

                        return result;
                    }
                }
            }
        }

        public async Task<object> sp_get_user_audit_logs(GetUserAuditLogs data)
        {
            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_get_user_audit_logs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = data.employeeID });
                    command.Parameters.Add(new MySqlParameter("p_search_user_id", MySqlDbType.Int32) { Value = data.searchID });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var result = new List<Dictionary<string, object>>();
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            result.Add(row);
                        }

                        return result;
                    }
                }
            }
        }


    }
}
