using System;
using System.Threading.Tasks;

namespace ChatOnline.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
    }
}
