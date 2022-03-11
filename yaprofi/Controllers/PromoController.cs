using Microsoft.AspNetCore.Mvc;
using yaprofi.Models;
using yaprofi.Services;

namespace yaprofi.Controllers;

[ApiController]
[Route("[controller]")]
public class PromoController : Controller
{
    private readonly PromoService _service;

    public PromoController(PromoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Получить все промоакции
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(typeof(List<Promotion>), 200)]
    [HttpGet]
    public IActionResult Get()
    {
        return Json(_service.GetAllPromotions());
    }

    /// <summary>
    /// Получить промоакцию по id номеру
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Promotion), 200)]
    public IActionResult GetPromotion([FromRoute] int id)
    {
        var promotion = _service.GetPromotion(id);
        return promotion != null 
            ? Json(_service.GetPromotion(id)) 
            : NotFound();
    }

    /// <summary>
    /// создать промоакцию
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    public IActionResult CreatePromotion([FromBody] CreatePromotionRequest req)
    {
        try
        {
            var id = _service.CreatePromotion(req.Name, req.Description);
            return Json(id);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// изменить промоакцию
    /// </summary>
    /// <param name="id"></param>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    public IActionResult UpdatePromotion([FromRoute] int id, [FromBody] CreatePromotionRequest req)
    {
        try
        {
            _service.UpdatePromotion(id, req.Name, req.Description);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// удалить промоакцию
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    public IActionResult DeletePromotion([FromRoute] int id)
    {
        try
        {
            _service.DeletePromotion(id);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// создать участника промоакции
    /// </summary>
    /// <param name="id"></param>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/participant")]
    [ProducesResponseType(typeof(int), 200)]
    public IActionResult CreateParticipant([FromRoute] int id, [FromBody] CreateParticipantRequest req)
    {
        try
        {
            var participantId = _service.CreateParticipant(id, req.Name);
            return Json(participantId);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// удалить участника промоакции
    /// </summary>
    /// <param name="id"></param>
    /// <param name="prizeId"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/participant/{prizeId:int}")]
    [ProducesResponseType(200)]
    public IActionResult DeleteParticipant([FromRoute] int id, [FromRoute] int prizeId)
    {
        try
        {
            _service.DeletePrize(id, prizeId);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// удалить приз
    /// </summary>
    /// <param name="id"></param>
    /// <param name="prizeId"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}/prize/{prizeId:int}")]
    [ProducesResponseType(200)]
    public IActionResult DeletePrize([FromRoute] int id, [FromRoute] int prizeId)
    {
        try
        {
            _service.DeletePrize(id, prizeId);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    /// <summary>
    /// создать приз
    /// </summary>
    /// <param name="id"></param>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/prize")]
    [ProducesResponseType(typeof(int), 200)]
    public IActionResult CreatePrize([FromRoute] int id, [FromBody] CreatePrizeRequest req)
    {
        try
        {
            var prizeId = _service.CreatePrize(id, req.Description);
            return Json(prizeId);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// получить результаты розыгрыша для промоакции
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/raffle")]
    [ProducesResponseType(typeof(List<Result>), 200)]
    public IActionResult Raffle([FromRoute] int id)
    {
        try
        {
            return Json(_service.Raffle(id));
        }
        catch (Exception)
        {
            return Conflict();
        }
    }
}