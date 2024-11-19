using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDriversAppWithMongoDb.Models;
using WebApiDriversAppWithMongoDb.Services;

namespace WebApiDriversAppWithMongoDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriversController(DriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var existingDriver =await _driverService.GetDriverByIdAsync(id);

        if(existingDriver is null)
            return NotFound();

        return Ok(existingDriver);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allExistingDrivers = await _driverService.GetDriversAsync();

        if (allExistingDrivers.Any())
            return Ok(allExistingDrivers);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Driver driver)
    {
        await _driverService.CreateDriverAsync(driver);

        return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Driver driver)
    {
        Driver existingDriver = await _driverService.GetDriverByIdAsync(id);
        if (existingDriver is null) return BadRequest();

        driver.Id = existingDriver.Id;

        await _driverService.UpdataDriverAsync(driver);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        Driver existingDriver = await _driverService.GetDriverByIdAsync(id);

        if (existingDriver is null) return BadRequest();

        await _driverService.DeleteDriverAsync(id);

        return NoContent();
    }


}
