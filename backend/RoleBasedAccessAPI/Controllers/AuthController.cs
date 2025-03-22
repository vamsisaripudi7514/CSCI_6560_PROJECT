using Microsoft.AspNetCore.Mvc; 
using RoleBasedAccessAPI.Data.Repository;
using RoleBasedAccessAPI.Data.Model;
using RoleBasedAccessAPI.Utility;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RoleBasedAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var userId = await _userRepository.LoginUserAsync(loginDto);

            if (userId == -1)
                return Unauthorized(new { flag = -1, message = "Invalid password" });

            if (userId == -2)
                return NotFound(new { flag = -2, message = "User not found" });

            if (userId < 0)
                return BadRequest(new { flag = -3, message = "Error logging in" });

            // ✅ Store User ID in Session
            HttpContext.Session.SetInt32("UserId", userId);

            // ✅ Generate JWT Token with UserID & Password
            var token = JwtHelper.GenerateJwtToken(loginDto.Username, userId, loginDto.Password);
            return Ok(new { flag = 1, message = "Login successful", token });
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPassword updatePasswordDto)
        {
            var updateResult = await _userRepository.UpdatePasswordAsync(
                updatePasswordDto.UserName,
                updatePasswordDto.OldPassword,
                updatePasswordDto.NewPassword);

            if (updateResult == -1)
                return Unauthorized(new { flag = -1, message = "Invalid current password" });

            if (updateResult == -2)
                return NotFound(new { flag = -2, message = "User not found" });

            if (updateResult < 0)
                return BadRequest(new { flag = -3, message = "Error updating password" });

            return Ok(new { flag = 1, message = "Password updated successfully" });
        }

        [HttpPost("ButtonVisibility")]
        public async Task<IActionResult> GetButtonVisibility([FromBody] ButtonVisibility buttonVisibilityDto)
        {
            var result = await _userRepository.GetButtonVisibilityAsync(buttonVisibilityDto.EmployeeId);

            if (result is List<Dictionary<string, object>> permissions && permissions.Count > 0)
            {
                return Ok(permissions);
            }

            if (result is IDictionary<string, object> dictResult && dictResult.ContainsKey("Message"))
            {
                return BadRequest(new { Message = dictResult["Message"] });
            }

            return NotFound(new { Message = "No permissions found for this employee." });
        }

    }
}



    






#region OLD CODE

//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using RoleBasedAccessAPI.Data.Repository;
//using RoleBasedAccessAPI.Data.Model;
//using RoleBasedAccessAPI.Utility;

//namespace RoleBasedAccessAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserRepository _userRepository;

//        public AuthController(UserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
//        {
//            var userId = await _userRepository.LoginUserAsync(loginDto);

//            if (userId == -1)
//                return Unauthorized(new { flag = -1, message = "Invalid password" });

//            if (userId == -2)
//                return NotFound(new { flag = -2, message = "User not found" });

//            if (userId < 0)
//                return BadRequest(new { flag = -3, message = "Error logging in" });

//            // Generate JWT Token
//            var token = JwtHelper.GenerateJwtToken(loginDto.Username, loginDto.Password);
//            return Ok(new { flag = 1, message = "Login successful", token });
//        }

//        [HttpPut("updatePassword")]
//        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
//        {
//            var updateResult = await _userRepository.UpdatePasswordAsync(
//                updatePasswordDto.Username,
//                updatePasswordDto.CurrentPassword,
//                updatePasswordDto.NewPassword);

//            if (updateResult == -1)
//                return Unauthorized(new { flag = -1, message = "Invalid current password" });

//            if (updateResult == -2)
//                return NotFound(new { flag = -2, message = "User not found" });

//            if (updateResult < 0)
//                return BadRequest(new { flag = -3, message = "Error updating password" });

//            return Ok(new { flag = 1, message = "Password updated successfully" });
//        }
//    }
//}


#endregion



























