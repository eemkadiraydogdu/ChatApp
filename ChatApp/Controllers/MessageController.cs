using ChatApp.Data;
using ChatApp.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController(IMessageService messageService) : Controller
{
    private readonly IMessageService _messageService = messageService;

    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
    {
        var messages = await _messageService.ListAllAsync(paginationModel);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = messages.Adapt<List<MessageModel>>(),
            StatusCode = 200,
            TotalCount = await _messageService.CountAsync()
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id)
    {
        var message = await _messageService.GetByIdAsync(id);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = message.Adapt<MessageModel>(),
            StatusCode = 200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] MessageCreateModel messageModel)
    {
        var message = messageModel.Adapt<Message>();
        var messageResult = await _messageService.AddAsync(message);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = messageResult.Adapt<MessageModel>(),
            StatusCode = 200
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] MessageUpdateModel messageModel)
    {
        var message = messageModel.Adapt<Message>();
        var messageResult = await _messageService.UpdateAsync(message);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = messageResult.Adapt<MessageModel>(),
            StatusCode = 200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id)
    {
        var message = await _messageService.GetByIdAsync(id);
        await _messageService.DeleteAsync(message);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            StatusCode = 200
        };
    }

}
