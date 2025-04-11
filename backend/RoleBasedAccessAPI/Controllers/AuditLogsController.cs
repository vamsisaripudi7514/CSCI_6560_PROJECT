using Microsoft.AspNetCore.Mvc;
using RoleBasedAccessAPI.Data.Model;
using RoleBasedAccessAPI.Data.Repository;
using RoleBasedAccessAPI.Utility;

namespace RoleBasedAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorize]
    public class AuditLogsController : Controller
    {
        private readonly AuditRepository _auditRepository;
        private readonly string _encryptionKey;

        public AuditLogsController(AuditRepository auditRepository, IConfiguration configuration)
        {
            _auditRepository = auditRepository;
            //_encryptionKey = configuration["EncryptionKey"] ?? "AFE9BCD9E0C659720653DA721409A5001E62C561C03949C3341146C3E8FF4BD1"; // Secure encryption key from appsettings.json
        }

        [HttpPost("GetAuditLogs")]
        public async Task<IActionResult> GetAuditLogs([FromBody] SelectAuditLogs data)
        {
            try
            {
                var result = await _auditRepository.sp_audit_logs(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("GetUserAuditLogs")]
        public async Task<IActionResult> GetUserAuditLogs([FromBody] GetUserAuditLogs data)
        {
            try
            {
                var result = await _auditRepository.sp_get_user_audit_logs(data);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
