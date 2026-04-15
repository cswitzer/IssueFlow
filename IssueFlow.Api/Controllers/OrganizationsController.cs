using IssueFlow.Api.Extensions;
using IssueFlow.Application.Authorization;
using IssueFlow.Application.Organizations;
using IssueFlow.Application.Organizations.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationAuthorizationService _authorizationService;

        public OrganizationsController(
            IOrganizationService organizationService,
            IOrganizationAuthorizationService authorizationService)
        {
            _organizationService = organizationService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfOrganizationAsync(userId, id))
                return Forbid();

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
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfOrganizationAsync(userId, id))
                return Forbid();

            var organization = await _organizationService.UpdateOrganizationAsync(id, updateOrganizationDto);
            if (organization is null)
                return NotFound();
            return Ok(organization);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            if (!await _authorizationService.IsUserMemberOfOrganizationAsync(userId, id))
                return Forbid();

            var organization = await _organizationService.DeleteOrganizationAsync(id);
            if (organization is null)
                return NotFound();
            return Ok(organization);
        }
    }
}
