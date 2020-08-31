using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatOnline.Server.Models;

namespace ChatOnline.Server.Services
{
    public interface IFriendsRelationService
    {
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="chatOnlineUserSelf">己方</param>
        /// <param name="chatOnlineUserFriend">对方</param>
        /// <returns></returns>
        Task AddFriendAsync(ChatOnlineUser chatOnlineUserSelf, ChatOnlineUser chatOnlineUserFriend);

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="chatOnlineUser"></param>
        /// <returns></returns>
        Task<List<ChatOnlineUser>> GetAllFriendsAsync(ChatOnlineUser chatOnlineUser);
    }
}
