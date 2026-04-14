using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class IssueStatusesControllerTests
{
    private readonly Mock<IIssueStatusService> _mockIssueStatusService;
    private readonly IssueStatusesController _controller;

    public IssueStatusesControllerTests()
    {
        _mockIssueStatusService = new Mock<IIssueStatusService>();
        _controller = new IssueStatusesController(_mockIssueStatusService.Object);
    }

    [Fact]
    public async Task GetIssueStatus_WhenExists_ReturnOkWithIssueStatus()
    {
        var issueStatusId = Guid.NewGuid();
        var expectedIssueStatus = new ReadIssueStatusDto
        {
            Id = issueStatusId,
            Name = "In Progress",
            Description = "Issue is currently being worked on",
            IsFinal = false,
            SortOrder = 2
        };

        _mockIssueStatusService
            .Setup(s => s.GetIssueStatusAsync(issueStatusId))
            .ReturnsAsync(expectedIssueStatus);

        await ControllerTestHelpers.GetById<IIssueStatusService, ReadIssueStatusDto>()
            .WithMockService(_mockIssueStatusService)
            .WithId(issueStatusId)
            .WithControllerAction(id => _controller.GetIssueStatus(id))
            .WithExpectedDto(expectedIssueStatus)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.Name, actual.Name),
                (expected, actual) => Assert.Equal(expected.Description, actual.Description),
                (expected, actual) => Assert.Equal(expected.IsFinal, actual.IsFinal),
                (expected, actual) => Assert.Equal(expected.SortOrder, actual.SortOrder)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetIssueStatus_WhenNotExists_ReturnNotFound()
    {
        var issueStatusId = Guid.NewGuid();

        _mockIssueStatusService
            .Setup(s => s.GetIssueStatusAsync(issueStatusId))
            .ReturnsAsync((ReadIssueStatusDto?)null);

        await ControllerTestHelpers.GetById<IIssueStatusService, ReadIssueStatusDto>()
            .WithMockService(_mockIssueStatusService)
            .WithId(issueStatusId)
            .WithControllerAction(id => _controller.GetIssueStatus(id))
            .AssertReturnsNotFound();
    }
}
