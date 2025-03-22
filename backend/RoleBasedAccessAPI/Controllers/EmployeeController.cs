using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RoleBasedAccessAPI.Data.Model;
using RoleBasedAccessAPI.Data.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            _encryptionKey = configuration["EncryptionKey"] ?? "MTSU2025"; // Secure encryption key from appsettings.json
        }


        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "Invalid User ID." });
            }

            var employees = await _userRepository.GetAllEmployeesAsync(userId);

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


        [HttpGet("GetEmployeeDetailsAsync")]
        public async Task<IActionResult> GetEmployeeDetailsAsync([FromQuery] int userId, [FromQuery] int employeeId, [FromQuery] string decryptionKey)
        {
            if (userId <= 0 || employeeId <= 0 || string.IsNullOrEmpty(decryptionKey))
            {
                return BadRequest(new { Message = "Invalid request parameters" });
            }

            var employeeData = await _userRepository.GetEmployeeDetailsAsync(userId, employeeId, decryptionKey);

            if (employeeData == null)
            {
                return NotFound(new { Message = "Employee details not found or access denied" });
            }

            return Ok(employeeData); // Return as JSON without mapping to a model
        }

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

        // ✅ Insert Employee API
        [HttpPost("InsertEmployee")]
        public async Task<IActionResult> InsertEmployee([FromBody] employee emp)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue || userId.Value <= 0)
                return Unauthorized(new { flag = -1, message = "User not logged in." });

            if (emp == null)
                return BadRequest(new { flag = -1, message = "Invalid employee data." });

            var resultMessage = await _userRepository.InsertEmployeeAsync(emp, userId.Value, _encryptionKey);

            if (resultMessage.Contains("successfully"))
                return Ok(new { flag = 1, message = resultMessage });

            return BadRequest(new { flag = -1, message = resultMessage });
        }

        // ✅ Update Employee API
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] employee emp)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue || userId.Value <= 0)
                return Unauthorized(new { flag = -1, message = "User not logged in." });

            if (emp == null)
                return BadRequest(new { flag = -1, message = "Invalid employee data." });

            var resultMessage = await _userRepository.UpdateEmployeeAsync(emp, userId.Value, _encryptionKey);

            if (resultMessage.Contains("successfully"))
                return Ok(new { flag = 1, message = resultMessage });

            return BadRequest(new { flag = -1, message = resultMessage });
        }

        // ✅ Update Project Mapping
        [HttpPut("UpdateProjectMapping")]
        public async Task<IActionResult> UpdateProjectMapping([FromQuery] int projectId, [FromQuery] int employeeId)
        {
            if (projectId <= 0 || employeeId <= 0)
                return BadRequest(new { flag = -1, message = "Invalid project ID or employee ID." });

            var result = await _userRepository.UpdateProjectMappingAsync(projectId, employeeId);
            if (result > 0)
                return Ok(new { flag = 1, message = "Project mapping updated successfully." });

            return BadRequest(new { flag = -1, message = "Failed to update project mapping." });
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