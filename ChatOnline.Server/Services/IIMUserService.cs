using System.Collections.Generic;
using System.Threading.Tasks;
using ChatOnline.Server.Models;
using ChatOnline.Server.ViewModels;

namespace ChatOnline.Server.Services
{
    public interface IIMUserService
    {
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <returns></returns>
        List<IMUserDto> GetFriends(long iMNumber);

        /// <summary>
        /// 用户上线
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        Task Online(long iMNumber, string connectionId);

        /// <summary>
        /// 用户下线
        /// </summary>
        /// <param name="iMNumber"></param>
        /// <returns></returns>
        Task Offline(long iMNumber);

    }
}