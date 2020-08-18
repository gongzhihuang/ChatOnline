using System.Collections.Generic;
using System.Threading.Tasks;
using ChatOnline.Server.Models;

namespace ChatOnline.Server.Services
{
    public interface IIMUserService
    {
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <returns></returns>
        List<IMUser> GetFriends(long iMNumber);

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <returns></returns>
        Task Online(long iMNumber);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <returns></returns>
        Task Offline(long iMNumber);

    }
}