using Microsoft.AspNetCore.Mvc;
using TestDashboard.API.DTOs;
using TestDashboard.API.Services;

namespace TestDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestResultsController : ControllerBase
{
    private readonly ITestResultService _service;

    public TestResultsController(ITestResultService service) => _service = service;

    /// <summary>
    /// GET /api/testresults
    /// Returns all test results, optionally filtered by DeviceModel, TestType, Passed, From, To.
    /// Example: GET /api/testresults?deviceModel=SEL-300G&passed=false
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TestResultFilterDto filter)
    {
        var results = await _service.GetAllAsync(filter);
        return Ok(results);
    }

    /// <summary>
    /// GET /api/testresults/stats
    /// Returns aggregate pass/fail stats for the dashboard header cards.
    /// </summary>
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _service.GetStatsAsync();
        return Ok(stats);
    }

    /// <summary>
    /// GET /api/testresults/{id}
    /// Returns a single test result by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// POST /api/testresults
    /// Logs a new test result. Returns 201 Created with the new resource.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTestResultDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// DELETE /api/testresults/{id}
    /// Removes a test result. Returns 204 No Content on success, 404 if not found.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}