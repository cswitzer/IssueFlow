using IssueFlow.Application.Profiles;
using IssueFlow.Application.Profiles.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfilesController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProfile(Guid id)
    {
        var profile = await _profileService.GetProfileAsync(id);
        if (profile is null)
            return NotFound();
        return Ok(profile);
    }

    [HttpGet("organization/{organizationId:guid}")]
    public async Task<IActionResult> GetProfilesByOrganization([FromRoute] Guid organizationId, [FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
        var profiles = await _profileService.GetProfilesByOrganization(organizationId, page, pageSize);
        return Ok(profiles);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProfiles([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
        var profiles = await _profileService.GetAllProfilesAsync(page, pageSize);
        return Ok(profiles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto createProfileDto)
    {
        var createdProfile = await _profileService.CreateProfileAsync(createProfileDto);
        return CreatedAtAction(nameof(GetProfile), new { id = createdProfile.Id }, createdProfile);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProfile([FromRoute] Guid id, [FromBody] UpdateProfileDto updateProfileDto)
    {
        var updatedProfile = await _profileService.UpdateProfileAsync(id, updateProfileDto);
        if (updatedProfile is null)
            return NotFound();
        return Ok(updatedProfile);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProfile([FromRoute] Guid id)
    {
        var deletedProfile = await _profileService.DeleteProfileAsync(id);
        if (deletedProfile is null)
            return NotFound();
        return Ok(deletedProfile);
    }
}
