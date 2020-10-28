using System;
namespace ChatOnline.Server.DTOs
{
    public class CreateChatOnlineUserDto
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        /// <value></value>
        public string ActualName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <value></value>
        public string Nickname { get; set; }

        /// <summary>
        /// 头像 (路径)
        /// </summary>
        /// <value></value>
        public string Avatar { get; set; }
    }
}
