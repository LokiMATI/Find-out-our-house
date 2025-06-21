using FindOutOurHouse.API.AppServices;
using FindOutOurHouse.API.DTO.Places;
using FindOutOurHouse.DAL.Places;
using Microsoft.AspNetCore.Mvc;

namespace FindOutOurHouse.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController(PlaceService service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(
        Guid id)
    {
        try
        {
            return Ok(await service.GetPlaceAsync(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        string? title,
        string? description)
    {
        try
        {
            return Ok(await service.GetPlacesAsync(
                title,
                description));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("near")]
    public async Task<IActionResult> GetListAsync(
        [FromBody] PlaceGetDto input)
    {
        try
        {
            return Ok(await service.GetPlacesAsync(
                input));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] PlaceCreateDto input)
    {
        try
        {
            return Ok(await service.CreatePlaceAsync(input));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutAsync(
        Guid id,
        [FromBody] PlaceUpdateDto input)
    {
        try
        {
            return Ok(await service.UpdatePlaceAsync(
                id,
                input));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid id)
    {
        try
        {
            await service.DeletePlaceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}