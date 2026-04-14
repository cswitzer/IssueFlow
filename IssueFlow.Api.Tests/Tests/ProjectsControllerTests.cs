using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Projects;
using IssueFlow.Application.Projects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class ProjectsControllerTests
{
    private readonly Mock<IProjectService> _mockProjectService;
    private readonly ProjectsController _controller;

    public ProjectsControllerTests()
    {
        _mockProjectService = new Mock<IProjectService>();
        _controller = new ProjectsController(_mockProjectService.Object);
    }

    [Fact]
    public async Task GetProject_WhenExists_ReturnsOkWithProject()
    {
        var projectId = Guid.NewGuid();
        var expectedProject = new ReadProjectDto
        {
            Id = projectId,
            Name = "Test Project",
            Description = "A test project",
            OwnerProfileId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockProjectService
            .Setup(s => s.GetProjectAsync(projectId))
            .ReturnsAsync(expectedProject);

        await ControllerTestHelpers.GetById<IProjectService, ReadProjectDto>()
            .WithMockService(_mockProjectService)
            .WithId(projectId)
            .WithControllerAction(id => _controller.GetProject(id))
            .WithExpectedDto(expectedProject)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.Name, actual.Name),
                (expected, actual) => Assert.Equal(expected.Description, actual.Description),
                (expected, actual) => Assert.Equal(expected.OwnerProfileId, actual.OwnerProfileId),
                (expected, actual) => Assert.Equal(expected.OrganizationId, actual.OrganizationId)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetProject_WhenNotExists_ReturnsNotFound()
    {
        var projectId = Guid.NewGuid();

        _mockProjectService
            .Setup(s => s.GetProjectAsync(projectId))
            .ReturnsAsync((ReadProjectDto?)null);

        await ControllerTestHelpers.GetById<IProjectService, ReadProjectDto>()
            .WithMockService(_mockProjectService)
            .WithId(projectId)
            .WithControllerAction(id => _controller.GetProject(id))
            .AssertReturnsNotFound();
    }

    [Fact]
    public async Task GetProjectsByProfileOwner_ReturnsOkWithList()
    {
        var profileId = Guid.NewGuid();
        var expectedProjects = new List<ReadProjectDto>
        {
            new ReadProjectDto { Id = Guid.NewGuid(), Name = "Project 1", OwnerProfileId = profileId, OrganizationId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow },
            new ReadProjectDto { Id = Guid.NewGuid(), Name = "Project 2", OwnerProfileId = profileId, OrganizationId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow }
        };

        _mockProjectService
            .Setup(s => s.GetProjectsByProfileOwnerAsync(profileId, 1, 15))
            .ReturnsAsync(expectedProjects);

        var result = await _controller.GetProjectsByProfileOwner(profileId, 1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProjects = Assert.IsAssignableFrom<IReadOnlyList<ReadProjectDto>>(okResult.Value);
        Assert.Equal(expectedProjects.Count, returnedProjects.Count);
        _mockProjectService.Verify(s => s.GetProjectsByProfileOwnerAsync(profileId, 1, 15), Times.Once);
    }

    [Fact]
    public async Task GetAllProjects_ReturnsOkWithList()
    {
        var expectedProjects = new List<ReadProjectDto>
        {
            new ReadProjectDto { Id = Guid.NewGuid(), Name = "Project 1", OwnerProfileId = Guid.NewGuid(), OrganizationId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow },
            new ReadProjectDto { Id = Guid.NewGuid(), Name = "Project 2", OwnerProfileId = Guid.NewGuid(), OrganizationId = Guid.NewGuid(), CreatedAt = DateTime.UtcNow }
        };

        _mockProjectService
            .Setup(s => s.GetAllProjectsAsync(1, 15))
            .ReturnsAsync(expectedProjects);

        var result = await _controller.GetAllProjects(1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProjects = Assert.IsAssignableFrom<IReadOnlyList<ReadProjectDto>>(okResult.Value);
        Assert.Equal(expectedProjects.Count, returnedProjects.Count);
        _mockProjectService.Verify(s => s.GetAllProjectsAsync(1, 15), Times.Once);
    }

    [Fact]
    public async Task CreateProject_ReturnsCreatedAtActionWithProject()
    {
        var createDto = new CreateProjectDto
        {
            Name = "New Project",
            Key = "NEWPROJ",
            Description = "A new project",
            OwnerProfileId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };

        var createdProject = new ReadProjectDto
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            Description = createDto.Description,
            OwnerProfileId = createDto.OwnerProfileId,
            OrganizationId = createDto.OrganizationId,
            CreatedAt = DateTime.UtcNow
        };

        _mockProjectService
            .Setup(s => s.CreateProjectAsync(createDto))
            .ReturnsAsync(createdProject);

        var result = await _controller.CreateProject(createDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedProject = Assert.IsType<ReadProjectDto>(createdAtActionResult.Value);
        Assert.Equal(createdProject.Id, returnedProject.Id);
        Assert.Equal(createdProject.Name, returnedProject.Name);
        _mockProjectService.Verify(s => s.CreateProjectAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task UpdateProject_WhenExists_ReturnsOkWithUpdatedProject()
    {
        var projectId = Guid.NewGuid();
        var updateDto = new UpdateProjectDto { Name = "Updated Project", Description = "Updated description" };
        var updatedProject = new ReadProjectDto
        {
            Id = projectId,
            Name = updateDto.Name!,
            Description = updateDto.Description,
            OwnerProfileId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockProjectService
            .Setup(s => s.UpdateProjectAsync(projectId, updateDto))
            .ReturnsAsync(updatedProject);

        var result = await _controller.UpdateProject(projectId, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProject = Assert.IsType<ReadProjectDto>(okResult.Value);
        Assert.Equal(updatedProject.Name, returnedProject.Name);
        _mockProjectService.Verify(s => s.UpdateProjectAsync(projectId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateProject_WhenNotExists_ReturnsNotFound()
    {
        var projectId = Guid.NewGuid();
        var updateDto = new UpdateProjectDto { Name = "Updated Project" };

        _mockProjectService
            .Setup(s => s.UpdateProjectAsync(projectId, updateDto))
            .ReturnsAsync((ReadProjectDto?)null);

        var result = await _controller.UpdateProject(projectId, updateDto);

        Assert.IsType<NotFoundResult>(result);
        _mockProjectService.Verify(s => s.UpdateProjectAsync(projectId, updateDto), Times.Once);
    }

    [Fact]
    public async Task DeleteProject_WhenExists_ReturnsOkWithDeletedProject()
    {
        var projectId = Guid.NewGuid();
        var deletedProject = new ReadProjectDto
        {
            Id = projectId,
            Name = "Deleted Project",
            OwnerProfileId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockProjectService
            .Setup(s => s.DeleteProjectAsync(projectId))
            .ReturnsAsync(deletedProject);

        var result = await _controller.DeleteProject(projectId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedProject = Assert.IsType<ReadProjectDto>(okResult.Value);
        Assert.Equal(deletedProject.Id, returnedProject.Id);
        _mockProjectService.Verify(s => s.DeleteProjectAsync(projectId), Times.Once);
    }

    [Fact]
    public async Task DeleteProject_WhenNotExists_ReturnsNotFound()
    {
        var projectId = Guid.NewGuid();

        _mockProjectService
            .Setup(s => s.DeleteProjectAsync(projectId))
            .ReturnsAsync((ReadProjectDto?)null);

        var result = await _controller.DeleteProject(projectId);

        Assert.IsType<NotFoundResult>(result);
        _mockProjectService.Verify(s => s.DeleteProjectAsync(projectId), Times.Once);
    }
}
