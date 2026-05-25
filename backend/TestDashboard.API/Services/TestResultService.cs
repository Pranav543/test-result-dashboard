using TestDashboard.API.DTOs;
using TestDashboard.API.Models;
using TestDashboard.API.Repositories;

namespace TestDashboard.API.Services;

public class TestResultService : ITestResultService
{
    private readonly ITestResultRepository _repo;

    public TestResultService(ITestResultRepository repo) => _repo = repo;

    public async Task<IEnumerable<TestResultDto>> GetAllAsync(TestResultFilterDto filter)
    {
        var results = await _repo.GetAllAsync(filter);
        return results.Select(MapToDto);
    }

    public async Task<TestResultDto?> GetByIdAsync(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        return result is null ? null : MapToDto(result);
    }

    public async Task<TestResultDto> CreateAsync(CreateTestResultDto dto)
    {
        var entity = new TestResult
        {
            DeviceId       = dto.DeviceId.Trim(),
            DeviceModel    = dto.DeviceModel.Trim(),
            TestType       = dto.TestType.Trim(),
            Passed         = dto.Passed,
            MeasuredValue  = dto.MeasuredValue,
            ExpectedValue  = dto.ExpectedValue,
            Tolerance      = dto.Tolerance,
            Notes          = dto.Notes?.Trim(),
            TechnicianName = dto.TechnicianName.Trim(),
            TestedAt       = DateTime.UtcNow
        };

        var created = await _repo.AddAsync(entity);
        return MapToDto(created);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    public Task<DashboardStatsDto> GetStatsAsync() => _repo.GetStatsAsync();

    // ── Private mapper (no AutoMapper dependency needed for a project this size) ──
    private static TestResultDto MapToDto(TestResult r) => new()
    {
        Id             = r.Id,
        DeviceId       = r.DeviceId,
        DeviceModel    = r.DeviceModel,
        TestType       = r.TestType,
        Passed         = r.Passed,
        MeasuredValue  = r.MeasuredValue,
        ExpectedValue  = r.ExpectedValue,
        Tolerance      = r.Tolerance,
        Notes          = r.Notes,
        TestedAt       = r.TestedAt,
        TechnicianName = r.TechnicianName
    };
}