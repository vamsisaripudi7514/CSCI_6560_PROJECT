using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAccessAPI.Data.Model;
using RoleBasedAccessAPI.Data.Repository;
using System.Threading.Tasks;

namespace RoleBasedAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // ✅ Select Project
        
        [HttpPost("selectProject")]
        public async Task<IActionResult> SelectProject([FromBody] SelectProject selectProjectDto)
        {
            var result = await _projectRepository.SelectProjectAsync(selectProjectDto);

            if (result is List<Dictionary<string, object>> projects && projects.Count > 0)
            {
                return Ok(projects);
            }

            if (result is IDictionary<string, object> dictResult && dictResult.ContainsKey("Message"))
            {
                return Unauthorized(new { Message = dictResult["Message"] });
            }

            return NotFound(new { Message = "No projects found for the employee." });
        }


        [HttpPut("updateProject")]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProject updateProjectDto)
        {
            var result = await _projectRepository.UpdateProjectAsync(updateProjectDto);

            if (result.IsSuccess)
                return Ok(new { Message = result.Message });

            if (result.Message.Contains("Access Denied"))
                return Unauthorized(new { Message = result.Message });

            return BadRequest(new { Message = result.Message });
        }

        [HttpPost("projectMappingUpdateList")]
        public async Task<IActionResult> ProjectMappingUpdateList([FromBody] ProjectMappingList projectMappingListDto)
        {
            var result = await _projectRepository.ProjectMappingUpdateListAsync(projectMappingListDto.EmployeeID);

            if (result is List<Dictionary<string, object>> projects && projects.Count > 0)
            {
                return Ok(projects);
            }

            if (result is IDictionary<string, object> dictResult && dictResult.ContainsKey("Message"))
            {
                var message = dictResult["Message"].ToString();

                if (message == "Invalid User Role." || message == "User not found.")
                    return Unauthorized(new { Message = message });

                return BadRequest(new { Message = message });
            }

            return NotFound(new { Message = "No projects found." });
        }

        [HttpPost("InsertProject")]
        public async Task<IActionResult> InsertEmployee([FromBody] AddProject data)
        {
            string encryptionKey = "B17D2A77D226A5F55F122D5E92F8104E7E45C8E98923322424563E8F0367B613";

            var result = await _projectRepository.InsertProjectAsync(data, encryptionKey);

            if (result.IsSuccess)
                return Ok(new { message = result.Message });

            return BadRequest(new { message = result.Message });
        }
    }


}
