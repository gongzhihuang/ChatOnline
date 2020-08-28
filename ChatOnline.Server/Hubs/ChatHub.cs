using System;
using System.Linq;
using System.Threading.Tasks;
using ChatOnline.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatOnline.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IIMUserService _iMUserService;

        public ChatHub(ILogger<ChatHub> logger, IIMUserService iMUserService)
        {
            _logger = logger;
            _iMUserService = iMUserService;
        }

        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Authorize]
        public async Task SendMessage(MessageContent message)
        {
            _logger.LogInformation($"给{message.IMNumber}发送消息:【{message.Message}】");
            await Clients.User(message.IMNumber).ReceiveMessage(message);
        }

        /// <summary>
        /// 客户端连接时执行
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"用户上线了");
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 客户端断开连接时执行
        /// </summary>
        /// <param name="exception">正常断开时，exception为null</param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (exception == null)
            {
                _logger.LogInformation($"用户下线了");
            }
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
            var userId = connection.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return userId;
        }
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    public class MessageContent
    {
        public string IMNumber { get; set; }

        public string Message { get; set; }
    }
}
