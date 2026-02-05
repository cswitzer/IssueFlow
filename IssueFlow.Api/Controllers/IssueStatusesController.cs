using IssueFlow.Application.Issues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueStatusesController : ControllerBase
    {
        private readonly IIssueStatusService _issueStatusService;

        public IssueStatusesController(IIssueStatusService issueStatusService)
        {
            _issueStatusService = issueStatusService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIssueStatus([FromRoute] Guid id)
        {
            var issueStatus = await _issueStatusService.GetIssueStatusAsync(id);
            if (issueStatus is null)
                return NotFound();
            return Ok(issueStatus);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIssueStatuses()
        {
            var issueStatuses = await _issueStatusService.GetAllIssueStatusesAsync();
            return Ok(issueStatuses);
        }
    }
}
