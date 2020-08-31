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

        private readonly IChatOnlineUserService _chatOnlineUserService;
        private readonly IChatRecordService _chatRecordService;

        public ChatHub(ILogger<ChatHub> logger, IChatOnlineUserService chatOnlineUserService, IChatRecordService chatRecordService)
        {
            _logger = logger;
            _chatOnlineUserService = chatOnlineUserService;
            _chatRecordService = chatRecordService;
        }

        /// <summary>
        /// 发送消息给指定用户
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Authorize]
        public async Task SendMessage(MessageContent message)
        {
            _logger.LogInformation($"{Context.UserIdentifier}给{message.Id}发送消息:【{message.Message}】");

            await _chatRecordService.CreateChatRecordAsync(long.Parse(Context.UserIdentifier), message);

            await Clients.User(message.Id).ReceiveMessage(message);
        }

        /// <summary>
        /// 客户端连接时执行
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"用户{Context.UserIdentifier}上线了,连接ID:{Context.ConnectionId}");

            var chatOnlineUser = await _chatOnlineUserService.GetChatOnlineUserAsync(long.Parse(Context.UserIdentifier));

            await _chatOnlineUserService.ChatOnlineUserOnline(chatOnlineUser, Context.ConnectionId);

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
                _logger.LogInformation($"用户{Context.UserIdentifier}正常下线了,连接ID:{Context.ConnectionId}");
            }

            _logger.LogInformation($"用户{Context.UserIdentifier}异常下线了,连接ID:{Context.ConnectionId}");

            var chatOnlineUser = await _chatOnlineUserService.GetChatOnlineUserAsync(long.Parse(Context.UserIdentifier));
            await _chatOnlineUserService.ChatOnlineUserOffline(chatOnlineUser);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
