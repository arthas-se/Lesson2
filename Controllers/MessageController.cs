using Lesson2.Models;
using Lesson2.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("messages")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _productService;

    public MessageController(IMessageService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await _productService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        var message = await _productService.GetMessageByIdAsync(id);
        if (message == null) return NotFound();
        return Ok(message);
    }

    [HttpGet("sender")]
    public async Task<IActionResult> GetMessagesBySender([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest();

        var messages = await _productService.GetMessagesBySenderAsync(email);
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] Message newMessage)
    {
        if (newMessage == null || string.IsNullOrWhiteSpace(newMessage.Email))
            return BadRequest();

        await _productService.AddMessageAsync(newMessage);
        return Created($"/messages", newMessage);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var success = await _productService.DeleteMessageAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}