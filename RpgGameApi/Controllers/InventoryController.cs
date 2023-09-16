using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgGame.Misc;
using RpgGame.Models.DTO;
using RpgGame.Models.Entity;
using RpgGame.Models.Request;
using RpgGame.Services.Interfaces;

namespace RpgGame.Controllers;

[ApiController]
[Authorize]
[Route("/inventory")]
public class InventoryController : Controller
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateInventoryRequest? body)
    {
        if(body is null)
            return BadRequest("Model cannot be null.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        CreateInventoryDTO result = await _inventoryService.CreateAsync(User.GetId(), body);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetAsync([FromRoute] ulong? id)
    {
        if (!id.HasValue || id == 0) 
            return BadRequest("Inventory id can't be null or empty.");

        Inventory result = await _inventoryService.GetInventoryByIdAsync(id.Value);

        return Ok(result);
    }
}
