using IssueFlow.Api.Extensions;
using IssueFlow.Application.Authorization;
using IssueFlow.Application.Projects;
using IssueFlow.Application.Projects.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IOrganizationAuthorizationService _authorizationService;

        public ProjectsController(
            IProjectService projectService,
            IOrganizationAuthorizationService authorizationService)
        {
            _projectService = projectService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfProjectOrganizationAsync(userId, id))
                return Forbid();

            var project = await _projectService.GetProjectAsync(id);
            if (project is null)
                return NotFound();
            return Ok(project);
        }

        [HttpGet("owner/{profileId:guid}")]
        public async Task<IActionResult> GetProjectsByProfileOwner([FromRoute] Guid profileId, [FromQuery] int page = 1, [FromQuery] int pageSize = 15)
        {
            var projects = await _projectService.GetProjectsByProfileOwnerAsync(profileId, page, pageSize);
            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
        {
            var projects = await _projectService.GetAllProjectsAsync(page, pageSize);
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfOrganizationAsync(userId, createProjectDto.OrganizationId))
                return Forbid();

            var createdProject = await _projectService.CreateProjectAsync(createProjectDto);
            return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfProjectOrganizationAsync(userId, id))
                return Forbid();

            var updatedProject = await _projectService.UpdateProjectAsync(id, updateProjectDto);
            if (updatedProject is null)
                return NotFound();
            return Ok(updatedProject);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfProjectOrganizationAsync(userId, id))
                return Forbid();

            var deletedProject = await _projectService.DeleteProjectAsync(id);
            if (deletedProject is null)
                return NotFound();
            return Ok(deletedProject);
        }
    }
}
