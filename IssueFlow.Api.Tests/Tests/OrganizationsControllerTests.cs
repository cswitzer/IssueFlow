using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Organizations;
using IssueFlow.Application.Organizations.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class OrganizationsControllerTests
{
    private readonly Mock<IOrganizationService> _mockOrganizationService;
    private readonly OrganizationsController _controller;

    public OrganizationsControllerTests()
    {
        _mockOrganizationService = new Mock<IOrganizationService>();
        _controller = new OrganizationsController(_mockOrganizationService.Object);
    }

    [Fact]
    public async Task GetOrganization_WhenExists_ReturnsOkWithOrganization()
    {
        var organizationId = Guid.NewGuid();
        var expectedOrganization = new ReadOrganizationDto
        {
            Id = organizationId,
            Name = "Test Organization",
            OwnerProfileId = Guid.NewGuid(),
            IsActive = true
        };

        _mockOrganizationService
            .Setup(s => s.GetOrganizationAsync(organizationId))
            .ReturnsAsync(expectedOrganization);

        await ControllerTestHelpers.GetById<IOrganizationService, ReadOrganizationDto>()
            .WithMockService(_mockOrganizationService)
            .WithId(organizationId)
            .WithControllerAction(id => _controller.GetOrganization(id))
            .WithExpectedDto(expectedOrganization)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.Name, actual.Name),
                (expected, actual) => Assert.Equal(expected.OwnerProfileId, actual.OwnerProfileId),
                (expected, actual) => Assert.Equal(expected.IsActive, actual.IsActive)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetOrganization_WhenNotExists_ReturnsNotFound()
    {
        var organizationId = Guid.NewGuid();

        _mockOrganizationService
            .Setup(s => s.GetOrganizationAsync(organizationId))
            .ReturnsAsync((ReadOrganizationDto?)null);

        await ControllerTestHelpers.GetById<IOrganizationService, ReadOrganizationDto>()
            .WithMockService(_mockOrganizationService)
            .WithId(organizationId)
            .WithControllerAction(id => _controller.GetOrganization(id))
            .AssertReturnsNotFound();
    }

    [Fact]
    public async Task GetOrganizationsByProfileOwner_ReturnsOkWithList()
    {
        var profileId = Guid.NewGuid();
        var expectedOrganizations = new List<ReadOrganizationDto>
        {
            new ReadOrganizationDto { Id = Guid.NewGuid(), Name = "Org 1", OwnerProfileId = profileId, IsActive = true },
            new ReadOrganizationDto { Id = Guid.NewGuid(), Name = "Org 2", OwnerProfileId = profileId, IsActive = true }
        };

        _mockOrganizationService
            .Setup(s => s.GetOrganizationsByProfileOwner(profileId, 1, 10))
            .ReturnsAsync(expectedOrganizations);

        var result = await _controller.GetOrganizationsByProfileOwner(profileId, 1, 10);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrganizations = Assert.IsAssignableFrom<IReadOnlyList<ReadOrganizationDto>>(okResult.Value);
        Assert.Equal(expectedOrganizations.Count, returnedOrganizations.Count);
        _mockOrganizationService.Verify(s => s.GetOrganizationsByProfileOwner(profileId, 1, 10), Times.Once);
    }

    [Fact]
    public async Task GetAllOrganizations_ReturnsOkWithList()
    {
        var expectedOrganizations = new List<ReadOrganizationDto>
        {
            new ReadOrganizationDto { Id = Guid.NewGuid(), Name = "Org 1", OwnerProfileId = Guid.NewGuid(), IsActive = true },
            new ReadOrganizationDto { Id = Guid.NewGuid(), Name = "Org 2", OwnerProfileId = Guid.NewGuid(), IsActive = false }
        };

        _mockOrganizationService
            .Setup(s => s.GetAllOrganizationsAsync(1, 15))
            .ReturnsAsync(expectedOrganizations);

        var result = await _controller.GetAllOrganizations(1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrganizations = Assert.IsAssignableFrom<IReadOnlyList<ReadOrganizationDto>>(okResult.Value);
        Assert.Equal(expectedOrganizations.Count, returnedOrganizations.Count);
        _mockOrganizationService.Verify(s => s.GetAllOrganizationsAsync(1, 15), Times.Once);
    }

    [Fact]
    public async Task CreateOrganization_ReturnsCreatedAtActionWithOrganization()
    {
        var createDto = new CreateOrganizationDto
        {
            Name = "New Organization",
            OwnerProfileId = Guid.NewGuid()
        };

        var createdOrganization = new ReadOrganizationDto
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            OwnerProfileId = createDto.OwnerProfileId,
            IsActive = true
        };

        _mockOrganizationService
            .Setup(s => s.CreateOrganizationAsync(createDto))
            .ReturnsAsync(createdOrganization);

        var result = await _controller.CreateOrganization(createDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedOrganization = Assert.IsType<ReadOrganizationDto>(createdAtActionResult.Value);
        Assert.Equal(createdOrganization.Id, returnedOrganization.Id);
        Assert.Equal(createdOrganization.Name, returnedOrganization.Name);
        _mockOrganizationService.Verify(s => s.CreateOrganizationAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task UpdateOrganization_WhenExists_ReturnsOkWithUpdatedOrganization()
    {
        var organizationId = Guid.NewGuid();
        var updateDto = new UpdateOrganizationDto { Name = "Updated Organization" };
        var updatedOrganization = new ReadOrganizationDto
        {
            Id = organizationId,
            Name = updateDto.Name!,
            OwnerProfileId = Guid.NewGuid(),
            IsActive = true
        };

        _mockOrganizationService
            .Setup(s => s.UpdateOrganizationAsync(organizationId, updateDto))
            .ReturnsAsync(updatedOrganization);

        var result = await _controller.UpdateOrganization(organizationId, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrganization = Assert.IsType<ReadOrganizationDto>(okResult.Value);
        Assert.Equal(updatedOrganization.Name, returnedOrganization.Name);
        _mockOrganizationService.Verify(s => s.UpdateOrganizationAsync(organizationId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateOrganization_WhenNotExists_ReturnsNotFound()
    {
        var organizationId = Guid.NewGuid();
        var updateDto = new UpdateOrganizationDto { Name = "Updated Organization" };

        _mockOrganizationService
            .Setup(s => s.UpdateOrganizationAsync(organizationId, updateDto))
            .ReturnsAsync((ReadOrganizationDto?)null);

        var result = await _controller.UpdateOrganization(organizationId, updateDto);

        Assert.IsType<NotFoundResult>(result);
        _mockOrganizationService.Verify(s => s.UpdateOrganizationAsync(organizationId, updateDto), Times.Once);
    }

    [Fact]
    public async Task DeleteOrganization_WhenExists_ReturnsOkWithDeletedOrganization()
    {
        var organizationId = Guid.NewGuid();
        var deletedOrganization = new ReadOrganizationDto
        {
            Id = organizationId,
            Name = "Deleted Organization",
            OwnerProfileId = Guid.NewGuid(),
            IsActive = false
        };

        _mockOrganizationService
            .Setup(s => s.DeleteOrganizationAsync(organizationId))
            .ReturnsAsync(deletedOrganization);

        var result = await _controller.DeleteOrganization(organizationId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedOrganization = Assert.IsType<ReadOrganizationDto>(okResult.Value);
        Assert.Equal(deletedOrganization.Id, returnedOrganization.Id);
        _mockOrganizationService.Verify(s => s.DeleteOrganizationAsync(organizationId), Times.Once);
    }

    [Fact]
    public async Task DeleteOrganization_WhenNotExists_ReturnsNotFound()
    {
        var organizationId = Guid.NewGuid();

        _mockOrganizationService
            .Setup(s => s.DeleteOrganizationAsync(organizationId))
            .ReturnsAsync((ReadOrganizationDto?)null);

        var result = await _controller.DeleteOrganization(organizationId);

        Assert.IsType<NotFoundResult>(result);
        _mockOrganizationService.Verify(s => s.DeleteOrganizationAsync(organizationId), Times.Once);
    }
}
