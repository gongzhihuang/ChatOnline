using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatOnline.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatOnline.Server.Services
{
    public class FriendsRelationService : IFriendsRelationService
    {
        private readonly IMDbContext _dbContext;

        public FriendsRelationService(IMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFriendAsync(ChatOnlineUser chatOnlineUserSelf, ChatOnlineUser chatOnlineUserFriend)
        {
            List<FriendsRelation> friendsRelations = new List<FriendsRelation>();

            FriendsRelation friendsRelationSelf = new FriendsRelation(chatOnlineUserSelf.Id, chatOnlineUserFriend.Id);
            FriendsRelation friendsRelationFriend = new FriendsRelation(chatOnlineUserFriend.Id, chatOnlineUserSelf.Id);

            friendsRelations.Add(friendsRelationSelf);
            friendsRelations.Add(friendsRelationFriend);

            _dbContext.AddRange(friendsRelations);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ChatOnlineUser>> GetAllFriendsAsync(ChatOnlineUser chatOnlineUser)
        {
            var friendsRelations = await _dbContext.FriendsRelations.Where(x => x.Id == chatOnlineUser.Id)
                .ToListAsync();

            var friendIds = friendsRelations.Select(x => x.Id);

            var chatOnlineUsers = await _dbContext.ChatOnlineUsers.Where(x => friendIds.Contains(x.Id))
                .ToListAsync();

            return chatOnlineUsers;
        }
    }
}
