using System;
namespace ChatOnline.Server.Models
{
    public class ChatOnlineUser
    {
        public ChatOnlineUser(long id, string actualName, string nickname, string avatar, string connectionId)
        {
            Id = id;
            ActualName = actualName;
            Nickname = nickname;
            Avatar = avatar;
            ConnectionId = connectionId;
        }

        public ChatOnlineUser(string actualName, string nickname, string avatar, string connectionId)
        {
            ActualName = actualName;
            Nickname = nickname;
            Avatar = avatar;
            ConnectionId = connectionId;
        }

        public long Id { get; private set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        /// <value></value>
        public string ActualName { get; private set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <value></value>
        public string Nickname { get; private set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <value></value>
        public string Avatar { get; private set; }

        /// <summary>
        /// SignalR连接ID
        /// </summary>
        /// <value></value>
        public string ConnectionId { get; private set; }

        /// <summary>
        /// 设置连接
        /// </summary>
        /// <param name="connectionId"></param>
        public void SetConnection(string connectionId)
        {
            this.ConnectionId = connectionId;
        }
    }
}
