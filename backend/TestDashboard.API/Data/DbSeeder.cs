using TestDashboard.API.Models;

namespace TestDashboard.API.Data;

/// <summary>
/// Populates the database with realistic sample data on first run.
/// Skips seeding if records already exist.
/// </summary>
public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.TestResults.Any()) return;

        var now = DateTime.UtcNow;

        var results = new List<TestResult>
        {
            // ── 10 days ago ───────────────────────────────────────────────────
            new() { DeviceId = "SN-001", DeviceModel = "SEL-300G", TestType = "Voltage",
                    Passed = true,  MeasuredValue = 24.1,  ExpectedValue = 24.0, Tolerance = 0.5,
                    TechnicianName = "Alice",  TestedAt = now.AddDays(-10) },
            new() { DeviceId = "SN-002", DeviceModel = "SEL-300G", TestType = "Voltage",
                    Passed = false, MeasuredValue = 26.8,  ExpectedValue = 24.0, Tolerance = 0.5,
                    Notes = "Output too high — possible regulator fault",
                    TechnicianName = "Bob",    TestedAt = now.AddDays(-10) },
            new() { DeviceId = "SN-003", DeviceModel = "SEL-400",  TestType = "Continuity",
                    Passed = true,  MeasuredValue = 0.3,   ExpectedValue = 0.0,  Tolerance = 1.0,
                    TechnicianName = "Carol",  TestedAt = now.AddDays(-10) },

            // ── 7 days ago ────────────────────────────────────────────────────
            new() { DeviceId = "SN-004", DeviceModel = "SEL-400",  TestType = "Isolation",
                    Passed = true,  MeasuredValue = 2100,  ExpectedValue = 2000, Tolerance = 200,
                    TechnicianName = "Alice",  TestedAt = now.AddDays(-7) },
            new() { DeviceId = "SN-005", DeviceModel = "SEL-300G", TestType = "Continuity",
                    Passed = false, MeasuredValue = 15.0,  ExpectedValue = 0.0,  Tolerance = 1.0,
                    Notes = "Open circuit detected on board J2",
                    TechnicianName = "Dave",   TestedAt = now.AddDays(-7) },
            new() { DeviceId = "SN-006", DeviceModel = "SEL-500",  TestType = "Voltage",
                    Passed = true,  MeasuredValue = 12.05, ExpectedValue = 12.0, Tolerance = 0.2,
                    TechnicianName = "Bob",    TestedAt = now.AddDays(-7) },

            // ── 5 days ago ────────────────────────────────────────────────────
            new() { DeviceId = "SN-007", DeviceModel = "SEL-500",  TestType = "Isolation",
                    Passed = true,  MeasuredValue = 1950,  ExpectedValue = 2000, Tolerance = 200,
                    TechnicianName = "Carol",  TestedAt = now.AddDays(-5) },
            new() { DeviceId = "SN-008", DeviceModel = "SEL-500",  TestType = "Current",
                    Passed = true,  MeasuredValue = 1.49,  ExpectedValue = 1.5,  Tolerance = 0.05,
                    TechnicianName = "Dave",   TestedAt = now.AddDays(-5) },
            new() { DeviceId = "SN-009", DeviceModel = "SEL-300G", TestType = "Current",
                    Passed = false, MeasuredValue = 1.75,  ExpectedValue = 1.5,  Tolerance = 0.05,
                    Notes = "Overcurrent — replaced fuse F3",
                    TechnicianName = "Alice",  TestedAt = now.AddDays(-5) },

            // ── 3 days ago ────────────────────────────────────────────────────
            new() { DeviceId = "SN-010", DeviceModel = "SEL-400",  TestType = "Voltage",
                    Passed = true,  MeasuredValue = 24.0,  ExpectedValue = 24.0, Tolerance = 0.5,
                    TechnicianName = "Bob",    TestedAt = now.AddDays(-3) },
            new() { DeviceId = "SN-011", DeviceModel = "SEL-400",  TestType = "Current",
                    Passed = true,  MeasuredValue = 1.51,  ExpectedValue = 1.5,  Tolerance = 0.05,
                    TechnicianName = "Carol",  TestedAt = now.AddDays(-3) },
            new() { DeviceId = "SN-012", DeviceModel = "SEL-500",  TestType = "Continuity",
                    Passed = true,  MeasuredValue = 0.1,   ExpectedValue = 0.0,  Tolerance = 1.0,
                    TechnicianName = "Dave",   TestedAt = now.AddDays(-3) },

            // ── Yesterday ─────────────────────────────────────────────────────
            new() { DeviceId = "SN-013", DeviceModel = "SEL-300G", TestType = "Isolation",
                    Passed = true,  MeasuredValue = 2050,  ExpectedValue = 2000, Tolerance = 200,
                    TechnicianName = "Alice",  TestedAt = now.AddDays(-1) },
            new() { DeviceId = "SN-014", DeviceModel = "SEL-500",  TestType = "Voltage",
                    Passed = false, MeasuredValue = 11.5,  ExpectedValue = 12.0, Tolerance = 0.2,
                    Notes = "Low voltage — power supply under spec",
                    TechnicianName = "Bob",    TestedAt = now.AddDays(-1) },
            new() { DeviceId = "SN-015", DeviceModel = "SEL-400",  TestType = "Isolation",
                    Passed = true,  MeasuredValue = 2200,  ExpectedValue = 2000, Tolerance = 200,
                    TechnicianName = "Carol",  TestedAt = now.AddDays(-1) },
        };

        db.TestResults.AddRange(results);
        db.SaveChanges();
    }
}