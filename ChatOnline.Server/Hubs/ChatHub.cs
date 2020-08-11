using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatOnline.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        //[Authorize]
        //public async Task SendMessage(string message)
        //{
        //    var user = Context.UserIdentifier;
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //}
        
        //[Authorize]
        //public Task SendPrivateMessage(string user, string message)
        //{
        //    return Clients.User(user).SendAsync("ReceiveMessage", message);
        //}

        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Authorize]
        public async Task SendMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage(user, message);
        }

        /// <summary>
        /// 发送消息给所有连接的用户
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Authorize]
        public Task SendMessageToCaller(string message)
        {
            return Clients.All.ReceiveMessage(message);
        }

        /// <summary>
        /// 客户端连接时执行
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 客户端断开连接是执行
        /// </summary>
        /// <param name="exception">正常断开时，exception为null</param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }


    public class UserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 为SignalR创建用户Id的方式，可以给指定用户发送消息
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            return userId;
        }
    }
}
