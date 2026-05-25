namespace TestDashboard.API.DTOs;

/// <summary>Query parameters for filtering the test result list.</summary>
public class TestResultFilterDto
{
    public string? DeviceModel { get; set; }
    public string? TestType { get; set; }
    public bool? Passed { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}