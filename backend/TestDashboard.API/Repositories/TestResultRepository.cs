using Microsoft.EntityFrameworkCore;
using TestDashboard.API.Data;
using TestDashboard.API.DTOs;
using TestDashboard.API.Models;

namespace TestDashboard.API.Repositories;

public class TestResultRepository : ITestResultRepository
{
    private readonly AppDbContext _db;

    public TestResultRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<TestResult>> GetAllAsync(TestResultFilterDto filter)
    {
        var query = _db.TestResults.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.DeviceModel))
            query = query.Where(r => r.DeviceModel == filter.DeviceModel);

        if (!string.IsNullOrWhiteSpace(filter.TestType))
            query = query.Where(r => r.TestType == filter.TestType);

        if (filter.Passed.HasValue)
            query = query.Where(r => r.Passed == filter.Passed.Value);

        if (filter.From.HasValue)
            query = query.Where(r => r.TestedAt >= filter.From.Value);

        if (filter.To.HasValue)
            query = query.Where(r => r.TestedAt <= filter.To.Value);

        return await query.OrderByDescending(r => r.TestedAt).ToListAsync();
    }

    public async Task<TestResult?> GetByIdAsync(int id) =>
        await _db.TestResults.FindAsync(id);

    public async Task<TestResult> AddAsync(TestResult result)
    {
        _db.TestResults.Add(result);
        await _db.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _db.TestResults.FindAsync(id);
        if (result is null) return false;

        _db.TestResults.Remove(result);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<DashboardStatsDto> GetStatsAsync()
    {
        var total  = await _db.TestResults.CountAsync();
        var passed = await _db.TestResults.CountAsync(r => r.Passed);

        return new DashboardStatsDto
        {
            Total    = total,
            Passed   = passed,
            Failed   = total - passed,
            PassRate = total == 0 ? 0 : Math.Round((double)passed / total * 100, 1)
        };
    }
}