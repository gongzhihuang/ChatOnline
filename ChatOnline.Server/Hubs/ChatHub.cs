using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatOnline.Server.Hubs
{
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task SendMessage(string message)
        {
            var user = Context.UserIdentifier;
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        
        [Authorize]
        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }
    }
    
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            return userId;
        }
    }
}
