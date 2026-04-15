using IssueFlow.Api.Extensions;
using IssueFlow.Application.Authorization;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IOrganizationAuthorizationService _authorizationService;

        public IssuesController(
            IIssueService issueService,
            IOrganizationAuthorizationService authorizationService)
        {
            _issueService = issueService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIssue([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfIssueOrganizationAsync(userId, id))
                return Forbid();

            var issue = await _issueService.GetIssueAsync(id);
            if (issue is null)
                return NotFound();
            return Ok(issue);
        }

        [HttpGet("project/{projectId:guid}")]
        public async Task<IActionResult> GetIssuesByProject([FromRoute] Guid projectId, [FromQuery] int page = 1, [FromQuery] int pageSize = 15)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfProjectOrganizationAsync(userId, projectId))
                return Forbid();

            var issues = await _issueService.GetIssuesByProjectAsync(projectId, page, pageSize);
            return Ok(issues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto createIssueDto)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfProjectOrganizationAsync(userId, createIssueDto.ProjectId))
                return Forbid();

            var createdIssue = await _issueService.CreateIssueAsync(createIssueDto);
            return CreatedAtAction(nameof(GetIssue), new { id = createdIssue.Id }, createdIssue);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateIssue([FromRoute] Guid id, [FromBody] UpdateIssueDto updateIssueDto)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfIssueOrganizationAsync(userId, id))
                return Forbid();

            var updatedIssue = await _issueService.UpdateIssueAsync(id, updateIssueDto);
            if (updatedIssue is null)
                return NotFound();
            return Ok(updatedIssue);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIssue([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfIssueOrganizationAsync(userId, id))
                return Forbid();

            var deletedIssue = await _issueService.DeleteIssueAsync(id);
            if (deletedIssue is null)
                return NotFound();
            return Ok(deletedIssue);
        }
    }
}
