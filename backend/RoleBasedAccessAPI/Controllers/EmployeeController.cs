using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RoleBasedAccessAPI.Data.Model;
using RoleBasedAccessAPI.Data.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RoleBasedAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly string _encryptionKey;

        public EmployeeController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _encryptionKey = configuration["EncryptionKey"] ?? "AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1"; // Secure encryption key from appsettings.json
        }


        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> Get_sp_select_employees([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "Invalid User ID." });
            }

            var employees = await _userRepository.Get_sp_select_employees(userId);

            if (employees == null || (employees is List<Dictionary<string, object>> list && list.Count == 0))
            {
                return NotFound(new { Message = "No employees found or you do not have access." });
            }

            return Ok(employees);
        }



        #region OLD GetAllEmployees
        //// ✅ Get All Employees
        //[HttpGet("GetAllEmployees")]
        //public async Task<IActionResult> GetAllEmployees()
        //{
        //    int? userId = HttpContext.Session.GetInt32("UserId");

        //    if (!userId.HasValue || userId.Value <= 0)
        //        return Unauthorized(new { flag = -1, message = "User not logged in." });

        //    var employees = await _userRepository.GetAllEmployeesAsync(userId.Value);

        //    if (employees == null || employees.Count == 0)
        //        return NotFound(new { flag = -1, message = "No employees found." });

        //    return Ok(new { flag = 1, message = "Employees retrieved successfully.", data = employees });
        //}


        #endregion


        // ✅ Search Employees
        [HttpGet("SearchEmployee")]
        public async Task<IActionResult> SearchEmployees([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return BadRequest(new { flag = -1, message = "Search term cannot be empty." });

            var employees = await _userRepository.SearchEmployeeAsync(searchTerm);
            if (employees == null || employees.Count == 0)
                return NotFound(new { flag = -1, message = "No matching employees found." });

            return Ok(new { flag = 1, message = "Search results retrieved successfully.", data = employees });
        }



        [HttpPost("GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetailsAsync([FromBody] GetEmployeeDetail request)
        {
            if (request == null || request.SourceEmployeeId <= 0 || request.TargetEmployeeId <= 0)
            {
                return BadRequest(new { Message = "Invalid request parameters" });
            }

            // Hardcoded decryption key
            string decryptionKey = "B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613";

            var employeeData = await _userRepository.GetEmployeeDetailsAsync(request, decryptionKey);

            if (employeeData == null)
            {
                return NotFound(new { Message = "Employee details not found or access denied" });
            }

            if (employeeData is IEnumerable<IDictionary<string, object>> employeeList && employeeList.Any())
            {
                return Ok(employeeList);
            }

            return StatusCode(500, employeeData);
        }





        // ✅ Insert Employee API
        [HttpPost("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee([FromBody] InsertEmployee insertEmployeeDto)
        {
            string encryptionKey = "B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613";

            var result = await _userRepository.InsertEmployeeAsync(insertEmployeeDto, encryptionKey);

            if (result.IsSuccess)
                return Ok(new { message = result.Message });

            return BadRequest(new { message = result.Message });
        }


        // ✅ Update Employee API
        [HttpPut("updateEmployee")]
        //[Authorize]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployee updateEmployeeDto)
        {
            string encryptionKey = "B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613";

            bool isUpdated = await _userRepository.UpdateEmployeeAsync(updateEmployeeDto, encryptionKey);

            if (isUpdated)
                return Ok(new { message = "Employee updated successfully." });

            return BadRequest(new { message = "Failed to update employee." });
        }


        [HttpPost("selectTimesheet")]
        public async Task<IActionResult> SelectTimesheet([FromBody] SelectTimesheet selectTimesheetDto)
        {
            var result = await _userRepository.SelectTimesheetAsync(selectTimesheetDto);

            if (result is List<Dictionary<string, object>> timesheets && timesheets.Count > 0)
            {
                return Ok(timesheets);
            }

            if (result is IDictionary<string, object> dictResult && dictResult.ContainsKey("Message"))
            {
                return Unauthorized(new { Message = dictResult["Message"] });
            }

            return NotFound(new { Message = "No timesheets found for the employee." });
        }



        // ✅ Update Project Mapping
        [HttpPut("updateProjectMapping")]
        public async Task<IActionResult> UpdateProjectMapping([FromBody] UpdateProjectMapping updateMappingDto)
        {
            var (isSuccess, message) = await _userRepository.UpdateProjectMappingAsync(updateMappingDto);

            if (isSuccess)
                return Ok(new { Message = message });

            if (message.Contains("Access Denied") || message.Contains("Role Based Access Denied"))
                return Unauthorized(new { Message = message });

            if (message.Contains("Invalid"))
                return BadRequest(new { Message = message });

            return StatusCode(500, new { Message = message });
        }


        [HttpPost("GetProjects")]
        public async Task<IActionResult> GetProjects([FromBody] GetProjects data)
        {
            var (isOk, result) = await _userRepository.GetProjects(data);
            if (result is List<Dictionary<string, object>> projectsData && projectsData.Count > 0 && isOk)
            {
                return Ok(projectsData);
            }

            else if(isOk == false)
            {
                return BadRequest(result);
            }

            return StatusCode(500, new { message = "Database Error" });
        }

        [HttpPost("GetProject")]
        public async Task<IActionResult> GetProject([FromBody] GetProject data)
        {
            var (isOk, result) = await _userRepository.GetProject(data);
            if (result is Dictionary<string, object> projectData && projectData.Count > 0 && isOk)
            {
                return Ok(projectData);
            }

            else if (isOk == false)
            {
                return BadRequest(result);
            }

            return StatusCode(500, new { message = "Database Error" });
        }

    }
}




















#region Old COde



//using Microsoft.AspNetCore.Mvc;
//using RoleBasedAccessAPI.Data.Model;
//using RoleBasedAccessAPI.Data.Repository;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace RoleBasedAccessAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeController : ControllerBase
//    {
//        private readonly UserRepository _userRepository;
//        private readonly string _encryptionKey;

//        public EmployeeController(UserRepository userRepository, IConfiguration configuration)
//        {
//            _userRepository = userRepository;
//            _encryptionKey = configuration["EncryptionKey"] ?? "MTSU2025"; // Default value if not set in appsettings.json
//        }

//        // ✅ Get All Employees
//        [HttpGet("GetAllEmployees")]
//        public async Task<IActionResult> GetAllEmployees()
//        {
//            var employees = await _userRepository.GetAllEmployeesAsync();
//            if (employees == null || employees.Count == 0)
//                return NotFound(new { flag = -1, message = "No employees found." });

//            return Ok(new { flag = 1, message = "Employees retrieved successfully.", data = employees });
//        }

//        // ✅ Search Employees
//        [HttpGet("SearchEmployee")]
//        public async Task<IActionResult> SearchEmployees([FromQuery] string searchTerm)
//        {
//            if (string.IsNullOrEmpty(searchTerm))
//                return BadRequest(new { flag = -1, message = "Search term cannot be empty." });

//            var employees = await _userRepository.SearchEmployeeAsync(searchTerm);
//            if (employees == null || employees.Count == 0)
//                return NotFound(new { flag = -1, message = "No matching employees found." });

//            return Ok(new { flag = 1, message = "Search results retrieved successfully.", data = employees });
//        }

//        // ✅ Get Employee Details
//        [HttpGet("GetEmployeeById")]
//        public async Task<IActionResult> GetEmployeeDetails([FromQuery] int employeeId)
//        {
//            if (employeeId <= 0)
//                return BadRequest(new { flag = -1, message = "Invalid employee ID." });

//            var employee = await _userRepository.GetEmployeeDetailsAsync(employeeId);
//            if (employee == null)
//                return NotFound(new { flag = -1, message = "Employee not found." });

//            return Ok(new { flag = 1, message = "Employee details retrieved successfully.", data = employee });
//        }

//        // ✅ Insert Employee API
//        [HttpPost("InsertEmployee")]
//        public async Task<IActionResult> InsertEmployee([FromBody] employee emp, [FromQuery] int userId)
//        {
//            if (emp == null)
//                return BadRequest(new { flag = -1, message = "Invalid employee data." });

//            if (userId <= 0)
//                return BadRequest(new { flag = -1, message = "Invalid user ID." });

//            var resultMessage = await _userRepository.InsertEmployeeAsync(emp, userId, _encryptionKey);

//            if (resultMessage.Contains("successfully"))
//                return Ok(new { flag = 1, message = resultMessage });

//            return BadRequest(new { flag = -1, message = resultMessage });
//        }

//        // ✅ Update Employee API
//        [HttpPut("UpdateEmployee")]
//        public async Task<IActionResult> UpdateEmployee([FromBody] employee emp, [FromQuery] int userId)
//        {
//            if (emp == null)
//                return BadRequest(new { flag = -1, message = "Invalid employee data." });

//            if (userId <= 0)
//                return BadRequest(new { flag = -1, message = "Invalid user ID." });

//            var resultMessage = await _userRepository.UpdateEmployeeAsync(emp, userId, _encryptionKey);

//            if (resultMessage.Contains("successfully"))
//                return Ok(new { flag = 1, message = resultMessage });

//            return BadRequest(new { flag = -1, message = resultMessage });
//        }

//        // ✅ Update Project Mapping
//        [HttpPut("UpdateProjectMapping")]
//        public async Task<IActionResult> UpdateProjectMapping([FromQuery] int projectId, [FromQuery] int employeeId)
//        {
//            if (projectId <= 0 || employeeId <= 0)
//                return BadRequest(new { flag = -1, message = "Invalid project ID or employee ID." });

//            var result = await _userRepository.UpdateProjectMappingAsync(projectId, employeeId);
//            if (result > 0)
//                return Ok(new { flag = 1, message = "Project mapping updated successfully." });

//            return BadRequest(new { flag = -1, message = "Failed to update project mapping." });
//        }
//    }
//}



#endregion


#region old GEt EMP by ID 
//// ✅ Get Employee Details
//[HttpGet("GetEmployeeById")]
//public async Task<IActionResult> GetEmployeeDetails([FromQuery] int employeeId)
//{
//    if (employeeId <= 0)
//        return BadRequest(new { flag = -1, message = "Invalid employee ID." });

//    var employee = await _userRepository.GetEmployeeDetailsAsync(employeeId);
//    if (employee == null)
//        return NotFound(new { flag = -1, message = "Employee not found." });

//    return Ok(new { flag = 1, message = "Employee details retrieved successfully.", data = employee });
//}
#endregion