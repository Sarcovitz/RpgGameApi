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
[Route("/character")]
public class CharacterController : Controller
{
    private readonly ICharacterService _characterService;
    private readonly IInventoryService _inventoryService;
    public CharacterController(ICharacterService characterService, IInventoryService inventoryService)
    {
        _characterService = characterService;
        _inventoryService = inventoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCharacterRequest? body)
    {
        if(body is null)
            return BadRequest("Model cannot be null.");

        if (!ModelState.IsValid) 
            return BadRequest(ModelState.GetErrors());

        CreateCharacterDTO result = await _characterService.CreateAsync(User.GetId(), body);

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id?}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] ulong? id)
    {
        if (!id.HasValue || id == 0) 
            return BadRequest("Character id can't be null or empty.");

        SuccessDTO result = await _characterService.DeleteAsync(id.Value);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetAsync([FromRoute] ulong? id)
    {
        if (!id.HasValue || id == 0) 
            return BadRequest("Character id can't be null or empty.");

        Character character = await _characterService.GetByIdAsync(id.Value);

        return Ok(character);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result  = await _characterService.GetAllAsync(User.GetId());

        return Ok(result);
    }

    [HttpGet]
    [Route("{id?}/inventory")]
    public async Task<IActionResult> GetCharacterInventoryAsync([FromRoute] ulong? id)
    {
        if (!id.HasValue || id == 0)
            return BadRequest("Character id can't be null or empty.");

        Inventory result = await _inventoryService.GetInventoryByCharacterIdAsync(id.Value);

        return Ok(result);
    }
}
