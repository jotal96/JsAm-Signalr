using Microsoft.AspNetCore.SignalR;

namespace JsAm_Signalr.Services.SignalR;

public class SignalrService : Hub
{
    private readonly ILogger<SignalrService> _logger;
    public SignalrService(ILogger<SignalrService> logger)
    {
        _logger = logger;
    }
    
    
    public override async Task OnConnectedAsync()
    {
        // envia ao cliente o seu ID
        await Clients.Client(Context.ConnectionId)
            .SendAsync("OnConnectedAsyncPrivate", "Entraste e o teu ID é: " + Context.ConnectionId);
        
        await Clients.All
            .SendAsync("OnConnectedAsync", 
                "Temos um novo amigo! "+Context.ConnectionId);
    }

    public async Task BroadCast(string message){
        Clients.All.SendAsync("BroadCast", Context.ConnectionId+": "+message);
    }
    
    public async Task BroadCastPrivate(string message, string userId){
        Clients.Client(userId).SendAsync("BroadCastPrivate", "Mensagem privada de:"+ Context.ConnectionId+":"+message);
    }
}