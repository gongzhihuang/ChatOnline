using System;
namespace ChatOnline.Server.Hubs
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public class MessageContent
    {
        public string Id { get; set; }

        public string Message { get; set; }
    }
}
