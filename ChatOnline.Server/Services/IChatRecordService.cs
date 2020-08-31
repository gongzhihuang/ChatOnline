using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatOnline.Server.Hubs;
using ChatOnline.Server.Models;

namespace ChatOnline.Server.Services
{
    public interface IChatRecordService
    {
        /// <summary>
        /// 增加聊天记录
        /// </summary>
        /// <param name="chatOnlineUserSelfId"></param>
        /// <param name="messageContent"></param>
        /// <returns></returns>
        Task CreateChatRecordAsync(long chatOnlineUserSelfId, MessageContent messageContent);

        /// <summary>
        /// 查询自己和指定好友的聊天记录
        /// </summary>
        /// <param name="chatOnlineUserSelfId"></param>
        /// <param name="chatOnlineUserFriendId"></param>
        /// <returns></returns>
        Task<List<ChatRecord>> GetChatRecordsAsync(long chatOnlineUserSelfId, long chatOnlineUserFriendId);

    }
}
