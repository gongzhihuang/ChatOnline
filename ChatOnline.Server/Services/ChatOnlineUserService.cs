using System;
using System.Threading.Tasks;
using ChatOnline.Server.DTOs;
using ChatOnline.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatOnline.Server.Services
{
    public class ChatOnlineUserService : IChatOnlineUserService
    {
        private readonly IMDbContext _dbContext;

        public ChatOnlineUserService(IMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatOnlineUser> GetChatOnlineUserAsync(long id)
        {
            var chatOnlineUser = await _dbContext.ChatOnlineUsers.FirstOrDefaultAsync(x => x.Id == id);
            return chatOnlineUser;
        }

        public async Task ChatOnlineUserOffline(ChatOnlineUser chatOnlineUser)
        {
            chatOnlineUser.SetConnection(null);

            //_dbContext.Update(chatOnlineUser);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ChatOnlineUserOnline(ChatOnlineUser chatOnlineUser, string connectionId)
        {
            chatOnlineUser.SetConnection(connectionId);

            //_dbContext.Update(chatOnlineUser);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ChatOnlineUser> CreateChatOnlineUserAsync(long id, CreateChatOnlineUserDto chatOnlineUserDto)
        {
            ChatOnlineUser chatOnlineUser = new ChatOnlineUser(id, chatOnlineUserDto.ActualName, chatOnlineUserDto.Nickname, chatOnlineUserDto.Avatar, "");

            _dbContext.Add(chatOnlineUser);

            await _dbContext.SaveChangesAsync();

            return chatOnlineUser;
        }

        public async Task<ChatOnlineUser> UpdateChatOnlineUserAsync(UpdateChatOnlineUserDto chatOnlineUserDto)
        {
            ChatOnlineUser chatOnlineUser = new ChatOnlineUser(chatOnlineUserDto.Id, chatOnlineUserDto.ActualName, chatOnlineUserDto.Nickname, chatOnlineUserDto.Avatar, "");

            _dbContext.Update(chatOnlineUser);

            await _dbContext.SaveChangesAsync();

            return chatOnlineUser;
        }
    }
}
