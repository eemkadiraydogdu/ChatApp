using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp;
[Authorize]
public class ChatHub :Hub
{
    private readonly IDictionary<string, string> _connectedUsers;

    public ChatHub(IDictionary<string, string> connectedUsers)
    {
        _connectedUsers = connectedUsers;
    }

    public override async Task OnConnectedAsync()
    {
        HttpContext context = Context.GetHttpContext();
        var token = context.Request.Headers["Authorization"];
        var user = Context.User.Identity.Name;
        if(!_connectedUsers.ContainsKey(user)){
            _connectedUsers.TryAdd(user, Context.ConnectionId);
        }
        else{
            _connectedUsers[user] = Context.ConnectionId;
        }

        await Clients.Caller.SendAsync("UserConnected",Context.ConnectionId);
    }

    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendMessageToUser(string connectionId, string message)
    {
        await Clients.User(connectionId).SendAsync("ReceiveMessage", message);
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

}
