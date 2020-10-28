using System;
namespace ChatOnline.Server.Models
{
    /// <summary>
    /// 聊天记录
    /// </summary>
    public class ChatRecord
    {
        public ChatRecord(long userId, long friendId, DateTime timeAt, string messageContent)
        {
            UserId = userId;
            FriendId = friendId;
            TimeAt = timeAt;
            MessageContent = messageContent;
        }

        public long Id { get; private set; }

        public long UserId { get; private set; }

        public long FriendId { get; private set; }

        public DateTime TimeAt { get; private set; }

        public string MessageContent { get; private set; }
    }
}
