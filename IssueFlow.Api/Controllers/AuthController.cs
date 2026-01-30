using IssueFlow.Application.Auth;
using IssueFlow.Application.Profiles;
using IssueFlow.Application.Profiles.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IssueFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IProfileService _profileService;

    public AuthController(IAuthService authService, IProfileService profileService)
    {
        _authService = authService;
        _profileService = profileService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var authResult = await _authService.RegisterUser(registerRequest);
        if (!authResult.Success)
        {
            return BadRequest(authResult.Errors);
        }

        var createProfileData = new CreateProfileDto
        {
            UserId = authResult.UserId!,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            OrganizationIds = registerRequest.OrganizationIds
        };
        await _profileService.CreateProfileAsync(createProfileData);

        return Ok(authResult);
    }
}
