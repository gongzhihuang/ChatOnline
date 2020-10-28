using System;
using System.Threading.Tasks;

namespace ChatOnline.Server.Hubs
{
    public interface IChatClient
    {
        /// <summary>
        /// 客户端收到消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ReceiveMessage(MessageContent message);
    }
}
