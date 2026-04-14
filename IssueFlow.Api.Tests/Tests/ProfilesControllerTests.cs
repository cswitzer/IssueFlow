using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Profiles;
using IssueFlow.Application.Profiles.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class ProfilesControllerTests
{
    private readonly Mock<IProfileService> _mockProfileService;
    private readonly ProfilesController _controller;

    public ProfilesControllerTests()
    {
        _mockProfileService = new Mock<IProfileService>();
        _controller = new ProfilesController(_mockProfileService.Object);
    }

    [Fact]
    public async Task GetProfile_WhenExists_ReturnsOkWithProfile()
    {
        var profileId = Guid.NewGuid();
        var expectedProfile = new ReadProfileDto
        {
            Id = profileId,
            DisplayName = "John Doe",
            ProfilePictureUrl = "https://example.com/pic.jpg",
            CreatedAt = DateTime.UtcNow
        };

        _mockProfileService
            .Setup(s => s.GetProfileAsync(profileId))
            .ReturnsAsync(expectedProfile);

        await ControllerTestHelpers.GetById<IProfileService, ReadProfileDto>()
            .WithMockService(_mockProfileService)
            .WithId(profileId)
            .WithControllerAction(id => _controller.GetProfile(id))
            .WithExpectedDto(expectedProfile)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.DisplayName, actual.DisplayName),
                (expected, actual) => Assert.Equal(expected.ProfilePictureUrl, actual.ProfilePictureUrl)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetProfile_WhenNotExists_ReturnsNotFound()
    {
        var profileId = Guid.NewGuid();

        _mockProfileService
            .Setup(s => s.GetProfileAsync(profileId))
            .ReturnsAsync((ReadProfileDto?)null);

        await ControllerTestHelpers.GetById<IProfileService, ReadProfileDto>()
            .WithMockService(_mockProfileService)
            .WithId(profileId)
            .WithControllerAction(id => _controller.GetProfile(id))
            .AssertReturnsNotFound();
    }

    [Fact]
    public async Task GetProfilesByOrganization_ReturnsOkWithList()
    {
        var organizationId = Guid.NewGuid();
        var expectedProfiles = new List<ReadProfileDto>
        {
            new ReadProfileDto { Id = Guid.NewGuid(), DisplayName = "John Doe", CreatedAt = DateTime.UtcNow },
            new ReadProfileDto { Id = Guid.NewGuid(), DisplayName = "Jane Doe", CreatedAt = DateTime.UtcNow }
        };

        _mockProfileService
            .Setup(s => s.GetProfilesByOrganization(organizationId, 1, 15))
            .ReturnsAsync(expectedProfiles);

        var result = await _controller.GetProfilesByOrganization(organizationId, 1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProfiles = Assert.IsAssignableFrom<IReadOnlyList<ReadProfileDto>>(okResult.Value);
        Assert.Equal(expectedProfiles.Count, returnedProfiles.Count);
        _mockProfileService.Verify(s => s.GetProfilesByOrganization(organizationId, 1, 15), Times.Once);
    }

    [Fact]
    public async Task GetAllProfiles_ReturnsOkWithList()
    {
        var expectedProfiles = new List<ReadProfileDto>
        {
            new ReadProfileDto { Id = Guid.NewGuid(), DisplayName = "John Doe", CreatedAt = DateTime.UtcNow },
            new ReadProfileDto { Id = Guid.NewGuid(), DisplayName = "Jane Doe", CreatedAt = DateTime.UtcNow }
        };

        _mockProfileService
            .Setup(s => s.GetAllProfilesAsync(1, 15))
            .ReturnsAsync(expectedProfiles);

        var result = await _controller.GetAllProfiles(1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProfiles = Assert.IsAssignableFrom<IReadOnlyList<ReadProfileDto>>(okResult.Value);
        Assert.Equal(expectedProfiles.Count, returnedProfiles.Count);
        _mockProfileService.Verify(s => s.GetAllProfilesAsync(1, 15), Times.Once);
    }

    [Fact]
    public async Task CreateProfile_ReturnsCreatedAtActionWithProfile()
    {
        var createDto = new CreateProfileDto
        {
            UserId = "user-123",
            FirstName = "John",
            LastName = "Doe",
            ProfilePictureUrl = "https://example.com/pic.jpg"
        };

        var createdProfile = new ReadProfileDto
        {
            Id = Guid.NewGuid(),
            DisplayName = $"{createDto.FirstName} {createDto.LastName}",
            ProfilePictureUrl = createDto.ProfilePictureUrl,
            CreatedAt = DateTime.UtcNow
        };

        _mockProfileService
            .Setup(s => s.CreateProfileAsync(createDto))
            .ReturnsAsync(createdProfile);

        var result = await _controller.CreateProfile(createDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedProfile = Assert.IsType<ReadProfileDto>(createdAtActionResult.Value);
        Assert.Equal(createdProfile.Id, returnedProfile.Id);
        Assert.Equal(createdProfile.DisplayName, returnedProfile.DisplayName);
        _mockProfileService.Verify(s => s.CreateProfileAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task UpdateProfile_WhenExists_ReturnsOkWithUpdatedProfile()
    {
        var profileId = Guid.NewGuid();
        var updateDto = new UpdateProfileDto { FirstName = "Jane", LastName = "Smith" };
        var updatedProfile = new ReadProfileDto
        {
            Id = profileId,
            DisplayName = "Jane Smith",
            CreatedAt = DateTime.UtcNow
        };

        _mockProfileService
            .Setup(s => s.UpdateProfileAsync(profileId, updateDto))
            .ReturnsAsync(updatedProfile);

        var result = await _controller.UpdateProfile(profileId, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProfile = Assert.IsType<ReadProfileDto>(okResult.Value);
        Assert.Equal(updatedProfile.DisplayName, returnedProfile.DisplayName);
        _mockProfileService.Verify(s => s.UpdateProfileAsync(profileId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateProfile_WhenNotExists_ReturnsNotFound()
    {
        var profileId = Guid.NewGuid();
        var updateDto = new UpdateProfileDto { FirstName = "Jane" };

        _mockProfileService
            .Setup(s => s.UpdateProfileAsync(profileId, updateDto))
            .ReturnsAsync((ReadProfileDto?)null);

        var result = await _controller.UpdateProfile(profileId, updateDto);

        Assert.IsType<NotFoundResult>(result);
        _mockProfileService.Verify(s => s.UpdateProfileAsync(profileId, updateDto), Times.Once);
    }

    [Fact]
    public async Task DeleteProfile_WhenExists_ReturnsOkWithDeletedProfile()
    {
        var profileId = Guid.NewGuid();
        var deletedProfile = new ReadProfileDto
        {
            Id = profileId,
            DisplayName = "Deleted User",
            CreatedAt = DateTime.UtcNow
        };

        _mockProfileService
            .Setup(s => s.DeleteProfileAsync(profileId))
            .ReturnsAsync(deletedProfile);

        var result = await _controller.DeleteProfile(profileId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProfile = Assert.IsType<ReadProfileDto>(okResult.Value);
        Assert.Equal(deletedProfile.Id, returnedProfile.Id);
        _mockProfileService.Verify(s => s.DeleteProfileAsync(profileId), Times.Once);
    }

    [Fact]
    public async Task DeleteProfile_WhenNotExists_ReturnsNotFound()
    {
        var profileId = Guid.NewGuid();

        _mockProfileService
            .Setup(s => s.DeleteProfileAsync(profileId))
            .ReturnsAsync((ReadProfileDto?)null);

        var result = await _controller.DeleteProfile(profileId);

        Assert.IsType<NotFoundResult>(result);
        _mockProfileService.Verify(s => s.DeleteProfileAsync(profileId), Times.Once);
    }
}
