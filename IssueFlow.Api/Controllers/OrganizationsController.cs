using IssueFlow.Application.Organizations;
using IssueFlow.Application.Organizations.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            var organization = await _organizationService.GetOrganizationAsync(id);
            if (organization is null)
                return NotFound();
            return Ok(organization);
        }

        [HttpGet("owner/{profileId:guid}")]
        public async Task<IActionResult> GetOrganizationsByProfileOwner([FromRoute] Guid profileId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var organizations = await _organizationService.GetOrganizationsByProfileOwner(profileId, page, pageSize);
            return Ok(organizations);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrganizations([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
        {
            var organizations = await _organizationService.GetAllOrganizationsAsync(page, pageSize);
            return Ok(organizations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
        {
            var organization = await _organizationService.CreateOrganizationAsync(createOrganizationDto);
            return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateOrganization([FromRoute] Guid id, [FromBody] UpdateOrganizationDto updateOrganizationDto)
        {
            var organization = await _organizationService.UpdateOrganizationAsync(id, updateOrganizationDto);
            if (organization is null)
                return NotFound();
            return Ok(organization);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] Guid id)
        {
            var organization = await _organizationService.DeleteOrganizationAsync(id);
            if (organization is null)
                return NotFound();
            return Ok(organization);
        }
    }
}
