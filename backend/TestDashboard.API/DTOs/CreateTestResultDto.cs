using System.ComponentModel.DataAnnotations;

namespace TestDashboard.API.DTOs;

/// <summary>Payload sent by the client when logging a new test result.</summary>
public class CreateTestResultDto
{
    [Required, MaxLength(50)]
    public string DeviceId { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string DeviceModel { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string TestType { get; set; } = string.Empty;

    public bool Passed { get; set; }

    public double? MeasuredValue { get; set; }
    public double? ExpectedValue { get; set; }
    public double? Tolerance { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    [Required, MaxLength(100)]
    public string TechnicianName { get; set; } = string.Empty;
}