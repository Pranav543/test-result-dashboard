namespace TestDashboard.API.DTOs;

/// <summary>Read-only view of a test result returned to clients.</summary>
public class TestResultDto
{
    public int Id { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string DeviceModel { get; set; } = string.Empty;
    public string TestType { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public double? MeasuredValue { get; set; }
    public double? ExpectedValue { get; set; }
    public double? Tolerance { get; set; }
    public string? Notes { get; set; }
    public DateTime TestedAt { get; set; }
    public string TechnicianName { get; set; } = string.Empty;
}