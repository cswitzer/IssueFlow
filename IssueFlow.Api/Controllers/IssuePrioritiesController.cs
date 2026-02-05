using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuePrioritiesController : ControllerBase
    {
        private readonly IIssuePriorityService _issuePriorityService;

        public IssuePrioritiesController(IIssuePriorityService issuePriorityService)
        {
            _issuePriorityService = issuePriorityService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIssuePriority([FromRoute] Guid id)
        {
            var issuePriority = await _issuePriorityService.GetIssuePriorityAsync(id);
            if (issuePriority is null)
                return NotFound();
            return Ok(issuePriority);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIssuePriorities()
        {
            var issuePriorities = await _issuePriorityService.GetAllIssuePrioritiesAsync();
            return Ok(issuePriorities);
        }
    }
}
