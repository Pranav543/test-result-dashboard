using TestDashboard.API.DTOs;

namespace TestDashboard.API.Services;

public interface ITestResultService
{
    Task<IEnumerable<TestResultDto>> GetAllAsync(TestResultFilterDto filter);
    Task<TestResultDto?> GetByIdAsync(int id);
    Task<TestResultDto> CreateAsync(CreateTestResultDto dto);
    Task<bool> DeleteAsync(int id);
    Task<DashboardStatsDto> GetStatsAsync();
}