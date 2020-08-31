using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace ChatOnline.Server.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        /// <summary>
        /// 为SignalR创建用户Id的方式，可以给指定用户发送消息
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var userId = connection.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return userId;
        }
    }
}
