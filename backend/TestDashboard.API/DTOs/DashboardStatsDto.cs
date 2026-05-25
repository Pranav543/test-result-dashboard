namespace TestDashboard.API.DTOs;

/// <summary>Aggregate statistics shown in the dashboard header cards.</summary>
public class DashboardStatsDto
{
    public int Total { get; set; }
    public int Passed { get; set; }
    public int Failed { get; set; }

    /// <summary>Pass rate as a percentage (0–100).</summary>
    public double PassRate { get; set; }
}