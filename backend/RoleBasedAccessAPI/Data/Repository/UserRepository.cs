using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RoleBasedAccessAPI.Data.Model;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                    string decryptionKeyHex = BitConverter.ToString(Encoding.UTF8.GetBytes("MTSU2025")).Replace("-", "");
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
                    command.Parameters.Add(new MySqlParameter("p_username", MySqlDbType.VarChar) { Value = username });
                    command.Parameters.Add(new MySqlParameter("p_current_password", MySqlDbType.VarChar) { Value = currentPassword });
                    command.Parameters.Add(new MySqlParameter("p_new_password", MySqlDbType.VarChar) { Value = newPassword });

                    var outputParam = new MySqlParameter("p_result", MySqlDbType.Int32)
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

        // ✅ Get All Employees
        public async Task<List<employee>> GetAllEmployeesAsync(int userId)
        {
            List<employee> employees = new List<employee>();

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_select_employees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });

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

        // ✅ Insert Employee
        public async Task<string> InsertEmployeeAsync(employee emp, int userId, string encryptionKey)
        {
            string message = "Error inserting employee.";

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_insert_employee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = emp.EmployeeId });
                    command.Parameters.Add(new MySqlParameter("p_employee_name", MySqlDbType.VarChar, 100) { Value = emp.EmployeeName });
                    command.Parameters.Add(new MySqlParameter("p_employee_email", MySqlDbType.VarChar, 100) { Value = emp.Email });
                    command.Parameters.Add(new MySqlParameter("p_employee_phone", MySqlDbType.VarChar, 15) { Value = emp.Phone });
                    command.Parameters.Add(new MySqlParameter("p_employee_role_id", MySqlDbType.Int32) { Value = emp.RoleId });
                    command.Parameters.Add(new MySqlParameter("p_employee_manager_id", MySqlDbType.Int32) { Value = emp.ManagerId });
                    command.Parameters.Add(new MySqlParameter("p_employee_salary", MySqlDbType.VarChar, 255) { Value = emp.EncryptedSalary });
                    command.Parameters.Add(new MySqlParameter("p_employee_hire_date", MySqlDbType.Date) { Value = emp.HireDate });
                    command.Parameters.Add(new MySqlParameter("p_is_working", MySqlDbType.Bool) { Value = emp.IsWorking });
                    command.Parameters.Add(new MySqlParameter("p_encryption_key", MySqlDbType.VarChar, 64) { Value = encryptionKey });

                    var outputMessage = new MySqlParameter("p_message", MySqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();
                    message = outputMessage.Value.ToString();
                }
            }
            return message;
        }

        // ✅ Update Employee
        public async Task<string> UpdateEmployeeAsync(employee emp, int userId, string encryptionKey)
        {
            string message = "Error updating employee.";

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_update_employee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = userId });
                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = emp.EmployeeId });

                    var outputMessage = new MySqlParameter("p_message", MySqlDbType.VarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputMessage);

                    await command.ExecuteNonQueryAsync();
                    message = outputMessage.Value.ToString();
                }
            }
            return message;
        }

        // ✅ Get Employee by ID
        public async Task<employee> GetEmployeeDetailsAsync(int employeeId)
        {
            employee employee = null;

            using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_get_employee_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("p_employee_id", MySqlDbType.Int32) { Value = employeeId });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new employee
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("employee_id")),
                                EmployeeName = reader.GetString(reader.GetOrdinal("employee_name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.GetString(reader.GetOrdinal("phone")),
                                RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
                                ManagerId = reader.GetInt32(reader.GetOrdinal("manager_id")),
                                EncryptedSalary = reader.GetString(reader.GetOrdinal("encrypted_salary")),
                                HireDate = reader.GetDateTime(reader.GetOrdinal("hire_date")),
                                IsWorking = reader.GetBoolean(reader.GetOrdinal("is_working")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            };
                        }
                    }
                }
            }
            return employee;
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