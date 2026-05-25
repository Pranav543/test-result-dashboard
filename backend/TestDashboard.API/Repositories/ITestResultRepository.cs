using TestDashboard.API.DTOs;
using TestDashboard.API.Models;

namespace TestDashboard.API.Repositories;

/// <summary>
/// Contract for data access operations on TestResult records.
/// Keeping this behind an interface allows the service layer to be
/// tested independently of the database (via mocking).
/// </summary>
public interface ITestResultRepository
{
    Task<IEnumerable<TestResult>> GetAllAsync(TestResultFilterDto filter);
    Task<TestResult?> GetByIdAsync(int id);
    Task<TestResult> AddAsync(TestResult result);
    Task<bool> DeleteAsync(int id);
    Task<DashboardStatsDto> GetStatsAsync();
}