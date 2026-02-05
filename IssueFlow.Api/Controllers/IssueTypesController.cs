using IssueFlow.Application.Issues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTypesController : ControllerBase
    {
        private readonly IIssueTypeService _issueTypeService;

        public IssueTypesController(IIssueTypeService issueTypeService)
        {
            _issueTypeService = issueTypeService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIssueType([FromRoute] Guid id)
        {
            var issueType = await _issueTypeService.GetIssueTypeAsync(id);
            if (issueType is null)
                return NotFound();
            return Ok(issueType);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIssueTypes()
        {
            var issueTypes = await _issueTypeService.GetAllIssueTypesAsync();
            return Ok(issueTypes);
        }
    }
}
