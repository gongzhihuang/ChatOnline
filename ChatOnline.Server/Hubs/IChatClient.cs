using System;
using System.Threading.Tasks;

namespace ChatOnline.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(SendMessage message);
    }
}
