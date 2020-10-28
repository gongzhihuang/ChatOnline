using System;
using System.Threading.Tasks;
using ChatOnline.Server.DTOs;
using ChatOnline.Server.Models;

namespace ChatOnline.Server.Services
{
    public interface IChatOnlineUserService
    {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChatOnlineUser> GetChatOnlineUserAsync(long id);

        /// <summary>
        /// 创建 ChatOnlineUser 用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chatOnlineUserDto"></param>
        /// <returns></returns>
        Task<ChatOnlineUser> CreateChatOnlineUserAsync(long id, CreateChatOnlineUserDto chatOnlineUserDto);

        /// <summary>
        /// 更新 ChatOnlineUser 用户
        /// </summary>
        /// <param name="chatOnlineUserDto"></param>
        /// <returns></returns>
        Task<ChatOnlineUser> UpdateChatOnlineUserAsync(UpdateChatOnlineUserDto chatOnlineUserDto);

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="chatOnlineUser"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task ChatOnlineUserOnline(ChatOnlineUser chatOnlineUser, string connectionId);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="chatOnlineUser"></param>
        /// <returns></returns>
        Task ChatOnlineUserOffline(ChatOnlineUser chatOnlineUser);
    }
}
