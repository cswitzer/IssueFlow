using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IssueFlow.Api.Tests.Utilities;

/// <summary>
///  Helper class providing reusable test methods for controller testing.
///  Reduces boilerplate code and promotes consistency across controller tests.
/// </summary>
public class GetByIdTestBuilder<TService, TDto>
    where TService : class
    where TDto : class
{
    private Mock<TService>? _mockService;
    private Func<Guid, Task<IActionResult>>? _controllerAction;
    private TDto? _expectedDto;
    private Guid? _id;
    private Action<TDto, TDto>[]? _customAssertions;

    public GetByIdTestBuilder<TService, TDto> WithMockService(Mock<TService> mockService)
    {
        _mockService = mockService;
        return this;
    }

    public GetByIdTestBuilder<TService, TDto> WithControllerAction(Func<Guid, Task<IActionResult>> controllerAction)
    {
        _controllerAction = controllerAction;
        return this;
    }

    public GetByIdTestBuilder<TService, TDto> WithExpectedDto(TDto expectedDto)
    {
        _expectedDto = expectedDto;
        return this;
    }

    public GetByIdTestBuilder<TService, TDto> WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public GetByIdTestBuilder<TService, TDto> WithCustomAssertions(params Action<TDto, TDto>[] customAssertions)
    {
        _customAssertions = customAssertions;
        return this;
    }

    public async Task AssertReturnsOk()
    {
        if (_mockService == null || _controllerAction == null || _id == null)
            throw new InvalidOperationException("All necessary components must be provided before calling AssertReturnsOk.");

        var result = await _controllerAction(_id.Value);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDto = Assert.IsType<TDto>(okResult.Value);

        if (_expectedDto != null && _customAssertions != null)
        {
            foreach (var assertion in _customAssertions)
            {
                assertion(_expectedDto, returnedDto);
            }
        }
    }

    public async Task AssertReturnsNotFound()
    {
        if (_mockService == null || _controllerAction == null || _id == null)
            throw new InvalidOperationException("Required properties must be set before executing the test.");

        var result = await _controllerAction(_id.Value);

        Assert.IsType<NotFoundResult>(result);
    }
}

/// <summary>
/// Enhanced helper class with both direct methods and fluent builders.
/// </summary>
public static class ControllerTestHelpers
{
    public static GetByIdTestBuilder<TService, TDto> GetById<TService, TDto>()
        where TService : class
        where TDto : class
    {
        return new GetByIdTestBuilder<TService, TDto>();
    }
}
