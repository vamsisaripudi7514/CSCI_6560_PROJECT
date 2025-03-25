using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RoleBasedAccessAPI.Data.Model;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RoleBasedAccessAPI.Data.Repository
{
    public class ProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Select Project
        public async Task<object> SelectProjectAsync(SelectProject selectProjectDto)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_select_project", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32)
                        {
                            Value = selectProjectDto.SourceEmployeeId
                        });

                        command.Parameters.Add(new MySqlParameter("target_id", MySqlDbType.Int32)
                        {
                            Value = selectProjectDto.TargetEmployeeId
                        });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var projects = new List<Dictionary<string, object>>();

                            while (await reader.ReadAsync())
                            {
                                var projectData = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    projectData[columnName] = columnValue;
                                }
                                projects.Add(projectData);
                            }

                            return projects;
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("permission"))
            {
                return new { Message = "Access Denied: You do not have permission to access this data." };
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }
        }


        // ✅ Update Project
        public async Task<(bool IsSuccess, string Message)> UpdateProjectAsync(UpdateProject updateProjectDto)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_update_project", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = updateProjectDto.SourceEmployeeId });
                        command.Parameters.Add(new MySqlParameter("p_project_id", MySqlDbType.Int32) { Value = updateProjectDto.ProjectId });
                        command.Parameters.Add(new MySqlParameter("p_project_name", MySqlDbType.VarChar) { Value = updateProjectDto.ProjectName });
                        command.Parameters.Add(new MySqlParameter("p_project_description", MySqlDbType.Text) { Value = updateProjectDto.ProjectDescription });
                        command.Parameters.Add(new MySqlParameter("p_manager_id", MySqlDbType.Int32) { Value = updateProjectDto.ManagerId });
                        command.Parameters.Add(new MySqlParameter("p_start_date", MySqlDbType.Date) { Value = updateProjectDto.StartDate.Date });
                        command.Parameters.Add(new MySqlParameter("p_end_date", MySqlDbType.Date) { Value = updateProjectDto.EndDate.Date });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string message = reader.GetString(0);
                                bool isSuccess = message.Contains("successfully");
                                return (isSuccess, message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            return (false, "An unexpected error occurred while updating the project.");
        }

        // Project Mapping Update List
        public async Task<object> ProjectMappingUpdateListAsync(int employeeId)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_project_mapping_update_list", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = employeeId });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var projects = new List<Dictionary<string, object>>();

                            while (await reader.ReadAsync())
                            {
                                var projectData = new Dictionary<string, object>
                        {
                            { "project_id", reader.GetInt32("project_id") },
                            { "project_name", reader.GetString("project_name") }
                        };
                                projects.Add(projectData);
                            }

                            return projects;
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("Invalid User Role"))
            {
                return new { Message = "Invalid User Role." };
            }
            catch (MySqlException ex) when (ex.Message.Contains("User not found"))
            {
                return new { Message = "User not found." };
            }
            catch (Exception ex)
            {
                return new { Message = "An unexpected error occurred.", Error = ex.Message };
            }
        }

        public async Task<(bool IsSuccess, string Message)> InsertProjectAsync(AddProject data, string encryptionKey)
        {
            try
            {
                using (var connection = (MySqlConnection)_context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("sp_insert_project", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Value = data.employee_id });
                        command.Parameters.Add(new MySqlParameter("p_project_id", MySqlDbType.Int32) { Value = data.project_id });
                        command.Parameters.Add(new MySqlParameter("p_project_name", MySqlDbType.VarChar) { Value = data.project_name });
                        command.Parameters.Add(new MySqlParameter("p_project_description", MySqlDbType.Text) { Value = data.project_description });
                        command.Parameters.Add(new MySqlParameter("p_manager_id", MySqlDbType.Int32) { Value = data.manager_id });
                        command.Parameters.Add(new MySqlParameter("p_start_date", MySqlDbType.Date) { Value = data.start_date.Date });
                        command.Parameters.Add(new MySqlParameter("p_end_date", MySqlDbType.Date) { Value = data.end_date.Date });


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
    }

}


