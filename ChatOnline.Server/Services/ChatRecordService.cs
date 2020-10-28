using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatOnline.Server.Hubs;
using ChatOnline.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatOnline.Server.Services
{
    public class ChatRecordService : IChatRecordService
    {
        private readonly IMDbContext _dbContext;

        public ChatRecordService(IMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateChatRecordAsync(long chatOnlineUserSelfId, MessageContent messageContent)
        {
            ChatRecord chatRecord = new ChatRecord(chatOnlineUserSelfId, long.Parse(messageContent.Id), DateTime.Now, messageContent.Message);

            _dbContext.Add(chatRecord);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChatRecordByDate(DateTime timeAt)
        {
            var records = _dbContext.ChatRecords.Where(x => x.TimeAt < timeAt);

            _dbContext.RemoveRange(records);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ChatRecord>> GetChatRecordsAsync(long chatOnlineUserSelfId, long chatOnlineUserFriendId)
        {
            var records = await _dbContext.ChatRecords.Where(x => (x.Id == chatOnlineUserSelfId && x.FriendId == chatOnlineUserFriendId) || (x.Id == chatOnlineUserFriendId && x.FriendId == chatOnlineUserSelfId))
                .OrderBy(x => x.TimeAt)
                .ToListAsync();

            return records;
        }
    }
}
