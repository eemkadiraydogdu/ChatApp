using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp;

[Route("api/[controller]")]
[ApiController]
public class HubController(ISignalrConnection signalrConnection) : Controller
{
    private readonly ISignalrConnection _signalrConnection = signalrConnection;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message){
        var connnect = _signalrConnection.StartConnection();
        await connnect.InvokeAsync("SendMessageToAll", "Admin", message);
        return Ok();
    }
}
