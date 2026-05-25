namespace TestDashboard.API.Models;

/// <summary>
/// Represents a single test run performed on a device on the factory floor.
/// </summary>
public class TestResult
{
    public int Id { get; set; }

    /// <summary>Device serial number (e.g. "SN-00123").</summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>Product model name (e.g. "SEL-300G").</summary>
    public string DeviceModel { get; set; } = string.Empty;

    /// <summary>Category of test performed (e.g. "Voltage", "Continuity", "Isolation").</summary>
    public string TestType { get; set; } = string.Empty;

    public bool Passed { get; set; }

    /// <summary>Actual reading taken during the test.</summary>
    public double? MeasuredValue { get; set; }

    /// <summary>Nominal/target reading for this test.</summary>
    public double? ExpectedValue { get; set; }

    /// <summary>Acceptable deviation from expected value (±).</summary>
    public double? Tolerance { get; set; }

    /// <summary>Optional technician notes or failure description.</summary>
    public string? Notes { get; set; }

    public DateTime TestedAt { get; set; } = DateTime.UtcNow;

    public string TechnicianName { get; set; } = string.Empty;
}