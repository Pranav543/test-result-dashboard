using FluentAssertions;
using Moq;
using TestDashboard.API.DTOs;
using TestDashboard.API.Models;
using TestDashboard.API.Repositories;
using TestDashboard.API.Services;

namespace TestDashboard.Tests.Services;

public class TestResultServiceTests
{
    // ── Test fixtures ─────────────────────────────────────────────────────────
    private readonly Mock<ITestResultRepository> _repoMock = new();
    private readonly ITestResultService _sut; // System Under Test

    public TestResultServiceTests() =>
        _sut = new TestResultService(_repoMock.Object);

    private static TestResult MakeResult(int id = 1, bool passed = true) => new()
    {
        Id             = id,
        DeviceId       = $"SN-{id:D3}",
        DeviceModel    = "SEL-300G",
        TestType       = "Voltage",
        Passed         = passed,
        MeasuredValue  = 24.0,
        ExpectedValue  = 24.0,
        Tolerance      = 0.5,
        TechnicianName = "Alice",
        TestedAt       = DateTime.UtcNow
    };

    // ── GetAllAsync ───────────────────────────────────────────────────────────
    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos_ForAllResults()
    {
        var entities = new List<TestResult> { MakeResult(1), MakeResult(2, false) };
        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<TestResultFilterDto>()))
                 .ReturnsAsync(entities);

        var result = (await _sut.GetAllAsync(new TestResultFilterDto())).ToList();

        result.Should().HaveCount(2);
        result[0].DeviceId.Should().Be("SN-001");
        result[1].Passed.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmpty_WhenNoResults()
    {
        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<TestResultFilterDto>()))
                 .ReturnsAsync(new List<TestResult>());

        var result = await _sut.GetAllAsync(new TestResultFilterDto());

        result.Should().BeEmpty();
    }

    // ── GetByIdAsync ──────────────────────────────────────────────────────────
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((TestResult?)null);

        var result = await _sut.GetByIdAsync(99);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsMappedDto_WhenFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(MakeResult(1));

        var result = await _sut.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.DeviceModel.Should().Be("SEL-300G");
    }

    // ── CreateAsync ───────────────────────────────────────────────────────────
    [Fact]
    public async Task CreateAsync_CallsRepositoryOnce_AndReturnsMappedDto()
    {
        var dto = new CreateTestResultDto
        {
            DeviceId = "SN-010", DeviceModel = "SEL-500",
            TestType = "Current", Passed = true, TechnicianName = "Bob"
        };

        // Simulate the DB assigning Id = 42
        _repoMock.Setup(r => r.AddAsync(It.IsAny<TestResult>()))
                 .ReturnsAsync((TestResult t) => { t.Id = 42; return t; });

        var result = await _sut.CreateAsync(dto);

        result.Id.Should().Be(42);
        result.DeviceId.Should().Be("SN-010");
        _repoMock.Verify(r => r.AddAsync(It.IsAny<TestResult>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_TrimsWhitespace_OnStringFields()
    {
        var dto = new CreateTestResultDto
        {
            DeviceId = "  SN-011  ", DeviceModel = " SEL-400 ",
            TestType = " Voltage ", Passed = false, TechnicianName = " Carol "
        };

        TestResult? captured = null;
        _repoMock.Setup(r => r.AddAsync(It.IsAny<TestResult>()))
                 .Callback<TestResult>(t => captured = t)
                 .ReturnsAsync((TestResult t) => t);

        await _sut.CreateAsync(dto);

        captured!.DeviceId.Should().Be("SN-011");
        captured.TechnicianName.Should().Be("Carol");
    }

    // ── DeleteAsync ───────────────────────────────────────────────────────────
    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenRecordExists()
    {
        _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _sut.DeleteAsync(1);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenRecordNotFound()
    {
        _repoMock.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);

        var result = await _sut.DeleteAsync(999);

        result.Should().BeFalse();
    }

    // ── GetStatsAsync ─────────────────────────────────────────────────────────
    [Fact]
    public async Task GetStatsAsync_ReturnsCorrectAggregates()
    {
        var expected = new DashboardStatsDto
            { Total = 20, Passed = 16, Failed = 4, PassRate = 80.0 };
        _repoMock.Setup(r => r.GetStatsAsync()).ReturnsAsync(expected);

        var stats = await _sut.GetStatsAsync();

        stats.Total.Should().Be(20);
        stats.PassRate.Should().Be(80.0);
        stats.Failed.Should().Be(4);
    }
}