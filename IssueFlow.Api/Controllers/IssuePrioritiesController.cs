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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadIssuePriorityDto>> GetIssuePriority([FromRoute] Guid id)
        {
            var issuePriority = await _issuePriorityService.GetIssuePriorityAsync(id);
            if (issuePriority is null)
            {
                return NotFound();
            }
            return Ok(issuePriority);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ReadIssuePriorityDto>>> GetAllIssuePriorities()
        {
            var issuePriorities = await _issuePriorityService.GetAllIssuePrioritiesAsync();
            return Ok(issuePriorities);
        }
    }
}
