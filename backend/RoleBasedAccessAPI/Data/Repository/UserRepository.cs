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
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ User Login (Retaining working version)
        public async Task<int> LoginUserAsync(UserLoginDto loginDto)
        {
            int userId = -3; // Default error code

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_user_login", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Input Parameters
                    command.Parameters.Add(new MySqlParameter("p_username", MySqlDbType.VarChar) { Value = loginDto.Username });
                    command.Parameters.Add(new MySqlParameter("p_user_password", MySqlDbType.VarChar) { Value = loginDto.Password });

                    // ✅ Convert decryption key to HEX format
                    string decryptionKeyHex = "AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1";
                    command.Parameters.Add(new MySqlParameter("p_decryption_key", MySqlDbType.VarChar) { Value = decryptionKeyHex });

                    // Output Parameter
                    var outputParam = new MySqlParameter("p_user_id", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    await command.ExecuteNonQueryAsync();
                    userId = (int)(outputParam.Value ?? -3);
                }
            }
            return userId;
        }



        // ✅ Update Password
        public async Task<int> UpdatePasswordAsync(string username, string currentPassword, string newPassword)
        {
            int updateResult = -3;

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_update_user_password", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("user_name", MySqlDbType.VarChar) { Value = username });
                    command.Parameters.Add(new MySqlParameter("old_password", MySqlDbType.VarChar) { Value = currentPassword });
                    command.Parameters.Add(new MySqlParameter("new_password", MySqlDbType.VarChar) { Value = newPassword });
                    string decryptionKeyHex = "AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1";
                    command.Parameters.Add(new MySqlParameter("decryption_key", MySqlDbType.VarChar) { Value = decryptionKeyHex });

                    var outputParam = new MySqlParameter("response_code", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    await command.ExecuteNonQueryAsync();
                    updateResult = (int)(outputParam.Value ?? -3);
                }
            }
            return updateResult;
        }

        // ✅ Search Employee
        public async Task<List<employee>> SearchEmployeeAsync(string searchTerm)
        {
            List<employee> employees = new List<employee>();

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_search_employee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_search_term", MySqlDbType.VarChar) { Value = searchTerm });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new employee
                            {
                                EmployeeId = reader.GetInt32("employee_id"),
                                EmployeeName = reader.GetString("employee_name"),
                                Email = reader.GetString("email"),
                                Phone = reader.GetString("phone"),
                                RoleId = reader.GetInt32("role_id"),
                                ManagerId = reader.GetInt32("manager_id"),
                                EncryptedSalary = reader.GetString("encrypted_salary"),
                                HireDate = reader.GetDateTime("hire_date"),
                                IsWorking = reader.GetBoolean("is_working"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                UpdatedAt = reader.GetDateTime("updated_at")
                            });
                        }
                    }
                }
            }
            return employees;
        }

        #region GetAllEmployeesAsync OLD

        //// ✅ Get All Employees under user
        //public async Task<List<employee>> GetAllEmployeesAsync(int userId)
        //{
        //    List<employee> employees = new List<employee>();

        //    using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new MySqlCommand("sp_select_employees", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    employees.Add(new employee
        //                    {
        //                        EmployeeId = reader.GetInt32("employee_id"),
        //                        EmployeeName = reader.GetString("employee_name"),
        //                        Email = reader.GetString("email"),
        //                        Phone = reader.GetString("phone"),
        //                        RoleId = reader.GetInt32("role_id"),
        //                        ManagerId = reader.GetInt32("manager_id"),
        //                        EncryptedSalary = reader.GetString("encrypted_salary"),
        //                        HireDate = reader.GetDateTime("hire_date"),
        //                        IsWorking = reader.GetBoolean("is_working"),
        //                        CreatedAt = reader.GetDateTime("created_at"),
        //                        UpdatedAt = reader.GetDateTime("updated_at")
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return employees;
        //}
        
        #endregion
        public async Task<object> Get_sp_select_employees(int userId) //sp_select_employees
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_select_employees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // ✅ Pass User ID as a parameter
                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var employees = new List<Dictionary<string, object>>();

                            while (await reader.ReadAsync())
                            {
                                var employeeData = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    employeeData[columnName] = columnValue;
                                }
                                employees.Add(employeeData);
                            }
                            return employees; // ✅ Return JSON dynamically
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("User does not have permission"))
            {
                return new { Message = "Access Denied: You do not have permission to view employees." };
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }
        }





        // ✅ Insert Employee
        public async Task<(bool IsSuccess, string Message)> InsertEmployeeAsync(InsertEmployee insertEmployeeDto, string encryptionKey)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_insert_employee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = insertEmployeeDto.SourceEmployeeId });
                        command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = insertEmployeeDto.TargetEmployeeId });
                        command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar) { Value = insertEmployeeDto.EmployeeName });
                        command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar) { Value = insertEmployeeDto.EmployeeEmail });
                        command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar) { Value = insertEmployeeDto.EmployeePhone });
                        command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = insertEmployeeDto.EmployeeRoleId });
                        command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = insertEmployeeDto.EmployeeManagerId });
                        command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar) { Value = insertEmployeeDto.EmployeeSalary });
                        command.Parameters.Add(new MySqlParameter("p_employee_hire_date", MySqlDbType.Date) { Value = insertEmployeeDto.HireDate.Date });
                        command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar) { Value = encryptionKey });

                        var messageParam = new MySqlParameter("p_message", MySqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(messageParam);

                        await command.ExecuteNonQueryAsync();

                        string message = messageParam.Value?.ToString();
                        return (true, message);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception here clearly for debugging
                return (false, ex.Message);
            }
        }


        #region OLD UPDate EMP
        // ✅ Update Employee
        //public async Task<bool> UpdateEmployeeAsync(employee emp, int userId, string encryptionKey)
        //{
        //    try
        //    {
        //        using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
        //        {
        //            await connection.OpenAsync();

        //            using (var command = new MySqlCommand("sp_update_employee", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
        //                command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = emp.EmployeeId });
        //                command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar) { Value = emp.EmployeeName });
        //                command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar) { Value = emp.Email });
        //                command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar) { Value = emp.Phone });
        //                command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = emp.RoleId });
        //                command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = emp.ManagerId });
        //                command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar) { Value = emp.EncryptedSalary }); // Assuming you pass plaintext salary here
        //                command.Parameters.Add(new MySqlParameter("p_is_working", MySqlDbType.Bit) { Value = emp.IsWorking });
        //                command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar) { Value = encryptionKey });

        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception details here
        //        return false;
        //    }
        //}

        #endregion



        #region GetEmployeeDetailsAsync, Get Employee by ID old

        // ✅ Get Employee by ID
        //public async Task<employee> GetEmployeeDetailsAsync(int employeeId)
        //{
        //    employee employee = null;

        //    using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new MySqlCommand("sp_get_employee_details", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    employee = new employee
        //                    {
        //                        EmployeeId = reader.GetInt32(reader.GetOrdinal("employee_id")),
        //                        EmployeeName = reader.GetString(reader.GetOrdinal("employee_name")),
        //                        Email = reader.GetString(reader.GetOrdinal("email")),
        //                        Phone = reader.GetString(reader.GetOrdinal("phone")),
        //                        RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
        //                        ManagerId = reader.GetInt32(reader.GetOrdinal("manager_id")),
        //                        EncryptedSalary = reader.GetString(reader.GetOrdinal("encrypted_salary")),
        //                        HireDate = reader.GetDateTime(reader.GetOrdinal("hire_date")),
        //                        IsWorking = reader.GetBoolean(reader.GetOrdinal("is_working")),
        //                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
        //                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    return employee;
        //}


        #endregion


        public async Task<object> GetEmployeeDetailsAsync(int userId, int employeeId, string decryptionKey)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_get_employee_details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // ✅ Input Parameters
                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
                        command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

                        // ✅ Convert decryption key to HEX format (same approach as in LoginUserAsync)
                        string decryptionKeyHex = BitConverter.ToString(Encoding.UTF8.GetBytes(decryptionKey)).Replace("-", "");
                        command.Parameters.Add(new MySqlParameter("p_decryption_key", MySqlDbType.VarChar) { Value = decryptionKeyHex });

                        Console.WriteLine($"Decryption Key Sent to MySQL (Hex Format): {decryptionKeyHex}"); // Debugging log

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var result = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    result[columnName] = columnValue;
                                }
                                return result;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("Incorrect string value"))
            {
                return new { Message = "Decryption Key format is incorrect. Ensure it is valid and properly encoded." };
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }

            return null;
        }


        public async Task<bool> UpdateEmployeeAsync(UpdateEmployee updateEmployeeDto, string encryptionKey)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_update_employee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = updateEmployeeDto.SourceEmployeeId });
                        command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = updateEmployeeDto.TargetEmployeeId });
                        command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar) { Value = updateEmployeeDto.EmployeeName });
                        command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar) { Value = updateEmployeeDto.EmployeeEmail });
                        command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar) { Value = updateEmployeeDto.EmployeePhone });
                        command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = updateEmployeeDto.EmployeeRoleId });
                        command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = updateEmployeeDto.EmployeeManagerId });
                        command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar) { Value = updateEmployeeDto.EmployeeSalary });
                        command.Parameters.Add(new MySqlParameter("p_is_working", MySqlDbType.Bit) { Value = updateEmployeeDto.IsWorking });
                        command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar) { Value = encryptionKey });

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log exception here
                return false;
            }
        }


        // ✅ Select Timesheet
        public async Task<object> SelectTimesheetAsync(SelectTimesheet selectTimesheetDto)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_select_timesheet", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32)
                        {
                            Value = selectTimesheetDto.SourceEmployeeId
                        });

                        command.Parameters.Add(new MySqlParameter("target_user_id", MySqlDbType.Int32)
                        {
                            Value = selectTimesheetDto.TargetEmployeeId
                        });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var timesheets = new List<Dictionary<string, object>>();

                            while (await reader.ReadAsync())
                            {
                                var timesheetData = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    timesheetData[columnName] = columnValue;
                                }
                                timesheets.Add(timesheetData);
                            }
                            return timesheets;
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("Access Denied") || ex.Message.Contains("Hierarchy Violation"))
            {
                return new { Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }
        }



        // ✅ Update Project Mapping
        public async Task<int> UpdateProjectMappingAsync(int projectId, int employeeId)
        {
            int result = -1;

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_update_project_mapping", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_project_id", MySqlDbType.Int32) { Value = projectId });
                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result;
        }
      
       // ✅ Button Visibility
        public async Task<object> GetButtonVisibilityAsync(int employeeId)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_button_visibility", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var permissions = new List<Dictionary<string, object>>();

                            while (await reader.ReadAsync())
                            {
                                var permissionData = new Dictionary<string, object>
                        {
                            { "db_table_name", reader.GetString("db_table_name") },
                            { "can_select", reader.GetBoolean("can_select") },
                            { "can_insert", reader.GetBoolean("can_insert") },
                            { "can_update", reader.GetBoolean("can_update") },
                            { "can_delete", reader.GetBoolean("can_delete") }
                        };
                                permissions.Add(permissionData);
                            }

                            return permissions;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }
        }

        // ✅ Update Project Mapping
        public async Task<(bool IsSuccess, string Message)> UpdateProjectMappingAsync(UpdateProjectMapping updateMappingDto)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_update_project_mapping", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32)
                        { Value = updateMappingDto.SourceEmployeeId });

                        command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32)
                        { Value = updateMappingDto.TargetEmployeeId });

                        command.Parameters.Add(new MySqlParameter("p_project_id", MySqlDbType.Int32)
                        { Value = updateMappingDto.ProjetId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string message = reader.GetString("Message");
                                return (true, message);
                            }
                        }
                    }
                }
                return (false, "Unknown error occurred.");
            }
            catch (MySqlException ex)
            {
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                return (false, $"Unexpected error: {ex.Message}");
            }
        }
      
          }
}
      



















#region Old code 

//using Microsoft.EntityFrameworkCore;
//using MySqlConnector;
//using RoleBasedAccessAPI.Data.Model;
//using System.Data;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//namespace RoleBasedAccessAPI.Data.Repository
//{
//    public class UserRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public UserRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // ✅ User Login
//        public async Task<int> LoginUserAsync(UserLoginDto loginDto)
//        {
//            int userId = -3; // Default error code

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_user_login", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    // Input Parameters
//                    command.Parameters.Add(new MySqlParameter("p_username", MySqlDbType.VarChar) { Value = loginDto.Username });
//                    command.Parameters.Add(new MySqlParameter("p_user_password", MySqlDbType.VarChar) { Value = loginDto.Password });

//                    string decryptionKeyHex = BitConverter.ToString(Encoding.UTF8.GetBytes("MTSU2025")).Replace("-", "");
//                    command.Parameters.Add(new MySqlParameter("p_decryption_key", MySqlDbType.VarChar) { Value = decryptionKeyHex });

//                    // Output Parameter
//                    var outputParam = new MySqlParameter("p_user_id", MySqlDbType.Int32)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(outputParam);

//                    await command.ExecuteNonQueryAsync();
//                    userId = (int)(outputParam.Value ?? -3);
//                }
//            }
//            return userId;
//        }

//        // ✅ Update Password
//        public async Task<int> UpdatePasswordAsync(string username, string currentPassword, string newPassword)
//        {
//            int updateResult = -3;

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_update_user_password", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    command.Parameters.Add(new MySqlParameter("p_username", MySqlDbType.VarChar) { Value = username });
//                    command.Parameters.Add(new MySqlParameter("p_current_password", MySqlDbType.VarChar) { Value = currentPassword });
//                    command.Parameters.Add(new MySqlParameter("p_new_password", MySqlDbType.VarChar) { Value = newPassword });

//                    var outputParam = new MySqlParameter("p_result", MySqlDbType.Int32)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(outputParam);

//                    await command.ExecuteNonQueryAsync();
//                    updateResult = (int)(outputParam.Value ?? -3);
//                }
//            }
//            return updateResult;
//        }

//        // ✅ Search Employee
//        public async Task<List<employee>> SearchEmployeeAsync(string searchTerm)
//        {
//            List<employee> employees = new List<employee>();

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_search_employee", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;
//                    command.Parameters.Add(new MySqlParameter("p_search_term", MySqlDbType.VarChar) { Value = searchTerm });

//                    using (var reader = await command.ExecuteReaderAsync())
//                    {
//                        while (await reader.ReadAsync())
//                        {
//                            employees.Add(new employee
//                            {
//                                EmployeeId = reader.GetInt32("employee_id"),
//                                EmployeeName = reader.GetString("employee_name"),
//                                Email = reader.GetString("email"),
//                                Phone = reader.GetString("phone"),
//                                RoleId = reader.GetInt32("role_id"),
//                                ManagerId = reader.GetInt32("manager_id"),
//                                EncryptedSalary = reader.GetString("encrypted_salary"),
//                                HireDate = reader.GetDateTime("hire_date"),
//                                IsWorking = reader.GetBoolean("is_working"),
//                                CreatedAt = reader.GetDateTime("created_at"),
//                                UpdatedAt = reader.GetDateTime("updated_at")
//                            });
//                        }
//                    }
//                }
//            }
//            return employees;
//        }

//        // ✅ Get All Employees
//        public async Task<List<employee>> GetAllEmployeesAsync()
//        {
//            List<employee> employees = new List<employee>();

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_select_employees", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    using (var reader = await command.ExecuteReaderAsync())
//                    {
//                        while (await reader.ReadAsync())
//                        {
//                            employees.Add(new employee
//                            {
//                                EmployeeId = reader.GetInt32("employee_id"),
//                                EmployeeName = reader.GetString("employee_name"),
//                                Email = reader.GetString("email"),
//                                Phone = reader.GetString("phone"),
//                                RoleId = reader.GetInt32("role_id"),
//                                ManagerId = reader.GetInt32("manager_id"),
//                                EncryptedSalary = reader.GetString("encrypted_salary"),
//                                HireDate = reader.GetDateTime("hire_date"),
//                                IsWorking = reader.GetBoolean("is_working"),
//                                CreatedAt = reader.GetDateTime("created_at"),
//                                UpdatedAt = reader.GetDateTime("updated_at")
//                            });
//                        }
//                    }
//                }
//            }
//            return employees;
//        }


//        // ✅ Insert Employee Method
//        public async Task<string> InsertEmployeeAsync(employee emp, int userId, string encryptionKey)
//        {
//            string message = "Error inserting employee.";

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_insert_employee", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    // Input Parameters
//                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = emp.EmployeeId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar, 100) { Value = emp.EmployeeName });
//                    command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar, 100) { Value = emp.Email });
//                    command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar, 15) { Value = emp.Phone });
//                    command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = emp.RoleId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = emp.ManagerId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar, 255) { Value = emp.EncryptedSalary });
//                    command.Parameters.Add(new MySqlParameter("p_employee_hire_date", MySqlDbType.Date) { Value = emp.HireDate });
//                    command.Parameters.Add(new MySqlParameter("p_is_working", MySqlDbType.Bool) { Value = emp.IsWorking });
//                    command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar, 64) { Value = encryptionKey });

//                    // Output Parameter for Message
//                    var outputMessage = new MySqlParameter("p_message", MySqlDbType.VarChar, 100)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(outputMessage);

//                    // Execute Stored Procedure
//                    await command.ExecuteNonQueryAsync();

//                    message = outputMessage.Value.ToString();
//                }
//            }
//            return message;
//        }

//        // ✅ Update Employee Method
//        public async Task<string> UpdateEmployeeAsync(employee emp, int userId, string encryptionKey)
//        {
//            string message = "Error updating employee.";

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_update_employee", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    // Input Parameters
//                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = emp.EmployeeId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar, 100) { Value = emp.EmployeeName });
//                    command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar, 150) { Value = emp.Email });
//                    command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar, 15) { Value = emp.Phone });
//                    command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = emp.RoleId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = emp.ManagerId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar, 255) { Value = emp.EncryptedSalary });
//                    command.Parameters.Add(new MySqlParameter("p_is_working", MySqlDbType.Bool) { Value = emp.IsWorking });
//                    command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar, 64) { Value = encryptionKey });

//                    // Output Parameter for Message
//                    var outputMessage = new MySqlParameter("p_message", MySqlDbType.VarChar, 100)
//                    {
//                        Direction = ParameterDirection.Output
//                    };
//                    command.Parameters.Add(outputMessage);

//                    // Execute Stored Procedure
//                    await command.ExecuteNonQueryAsync();

//                    message = outputMessage.Value.ToString();
//                }
//            }
//            return message;
//        }

//        // ✅ Get Employee by ID
//        public async Task<employee> GetEmployeeDetailsAsync(int employeeId)
//        {
//            employee employee = null;

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_get_employee_details", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;
//                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

//                    using (var reader = await command.ExecuteReaderAsync())
//                    {
//                        if (await reader.ReadAsync())
//                        {
//                            employee = new employee
//                            {
//                                EmployeeId = reader.GetInt32("employee_id"),
//                                EmployeeName = reader.GetString("employee_name"),
//                                Email = reader.GetString("email"),
//                                Phone = reader.GetString("phone"),
//                                RoleId = reader.GetInt32("role_id"),
//                                ManagerId = reader.GetInt32("manager_id"),
//                                EncryptedSalary = reader.GetString("encrypted_salary"),
//                                HireDate = reader.GetDateTime("hire_date"),
//                                IsWorking = reader.GetBoolean("is_working"),
//                                CreatedAt = reader.GetDateTime("created_at"),
//                                UpdatedAt = reader.GetDateTime("updated_at")
//                            };
//                        }
//                    }
//                }
//            }
//            return employee;
//        }

//        // ✅ Update Project Mapping
//        public async Task<int> UpdateProjectMappingAsync(int projectId, int employeeId)
//        {
//            int result = -1;

//            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
//            {
//                await connection.OpenAsync();

//                using (var command = new MySqlCommand("sp_update_project_mapping", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;
//                    command.Parameters.Add(new MySqlParameter("p_project_id", MySqlDbType.Int32) { Value = projectId });
//                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

//                    result = await command.ExecuteNonQueryAsync();
//                }
//            }
//            return result;
//        }
//    }
//}



#endregion