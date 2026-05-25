using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestDashboard.API.Controllers;
using TestDashboard.API.DTOs;
using TestDashboard.API.Services;

namespace TestDashboard.Tests.Controllers;

public class TestResultsControllerTests
{
    private readonly Mock<ITestResultService> _serviceMock = new();
    private readonly TestResultsController _sut;

    public TestResultsControllerTests() =>
        _sut = new TestResultsController(_serviceMock.Object);

    private static TestResultDto MakeDto(int id = 1, bool passed = true) => new()
    {
        Id = id, DeviceId = $"SN-{id:D3}", DeviceModel = "SEL-300G",
        TestType = "Voltage", Passed = passed, TechnicianName = "Alice",
        TestedAt = DateTime.UtcNow
    };

    // ── GET /api/testresults ──────────────────────────────────────────────────
    [Fact]
    public async Task GetAll_Returns200_WithListOfResults()
    {
        var dtos = new List<TestResultDto> { MakeDto(1), MakeDto(2, false) };
        _serviceMock.Setup(s => s.GetAllAsync(It.IsAny<TestResultFilterDto>()))
                    .ReturnsAsync(dtos);

        var result = await _sut.GetAll(new TestResultFilterDto()) as OkObjectResult;

        result!.StatusCode.Should().Be(200);
        (result.Value as IEnumerable<TestResultDto>)!.Should().HaveCount(2);
    }

    // ── GET /api/testresults/stats ────────────────────────────────────────────
    [Fact]
    public async Task GetStats_Returns200_WithAggregateData()
    {
        _serviceMock.Setup(s => s.GetStatsAsync())
                    .ReturnsAsync(new DashboardStatsDto { Total = 10, Passed = 8, Failed = 2, PassRate = 80 });

        var result = await _sut.GetStats() as OkObjectResult;
        var stats  = result!.Value as DashboardStatsDto;

        result.StatusCode.Should().Be(200);
        stats!.Total.Should().Be(10);
    }

    // ── GET /api/testresults/{id} ─────────────────────────────────────────────
    [Fact]
    public async Task GetById_Returns404_WhenNotFound()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TestResultDto?)null);

        var result = await _sut.GetById(99);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetById_Returns200_WhenFound()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(MakeDto(1));

        var result = await _sut.GetById(1) as OkObjectResult;

        result!.StatusCode.Should().Be(200);
        (result.Value as TestResultDto)!.Id.Should().Be(1);
    }

    // ── POST /api/testresults ─────────────────────────────────────────────────
    [Fact]
    public async Task Create_Returns201_WithLocationHeader()
    {
        var dto = new CreateTestResultDto
        {
            DeviceId = "SN-020", DeviceModel = "SEL-500",
            TestType = "Current", Passed = true, TechnicianName = "Dave"
        };
        _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(MakeDto(20));

        var result = await _sut.Create(dto) as CreatedAtActionResult;

        result!.StatusCode.Should().Be(201);
        result.ActionName.Should().Be(nameof(_sut.GetById));
    }

    // ── DELETE /api/testresults/{id} ──────────────────────────────────────────
    [Fact]
    public async Task Delete_Returns204_WhenDeleted()
    {
        _serviceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _sut.Delete(1);

        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_Returns404_WhenNotFound()
    {
        _serviceMock.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        var result = await _sut.Delete(99);

        result.Should().BeOfType<NotFoundResult>();
    }
}