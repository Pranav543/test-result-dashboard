using Microsoft.EntityFrameworkCore;
using TestDashboard.API.Models;

namespace TestDashboard.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TestResult> TestResults => Set<TestResult>();
}