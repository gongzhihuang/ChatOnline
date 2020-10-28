using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatOnline.Server.DTOs;
using ChatOnline.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatOnline.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatOnlineController : Controller
    {
        public readonly IChatOnlineUserService _chatOnlineUserService;
        public readonly IChatRecordService _chatRecordService;
        public readonly IFriendsRelationService _friendsRelationService;

        public ChatOnlineController(
            IChatOnlineUserService chatOnlineUserService,
            IChatRecordService chatRecordService,
            IFriendsRelationService friendsRelationService)
        {
            _chatOnlineUserService = chatOnlineUserService;
            _chatRecordService = chatRecordService;
            _friendsRelationService = friendsRelationService;
        }

        /// <summary>
        /// 查询账号是否存在
        /// </summary>
        /// <returns></returns>
        [HttpGet("chatOnlineUser")]
        public async Task<IActionResult> GetChatOnlineUserAsync()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var chatOnlineUser = await _chatOnlineUserService.GetChatOnlineUserAsync(long.Parse(userId));

            if (chatOnlineUser != null)
            {
                return Ok(chatOnlineUser);
            }
            return NotFound();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="chatOnlineUserDto"></param>
        /// <returns></returns>
        [HttpPost("chatOnlineUser")]
        public async Task<IActionResult> CreateChatOnlineUserAsync(CreateChatOnlineUserDto chatOnlineUserDto)
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;
            var chatOnlineUser = await _chatOnlineUserService.CreateChatOnlineUserAsync(long.Parse(userId), chatOnlineUserDto);

            if (chatOnlineUser != null)
            {
                return Ok(chatOnlineUser);
            }
            return BadRequest();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="chatOnlineUserDto"></param>
        /// <returns></returns>
        [HttpPut("chatOnlineUser")]
        public async Task<IActionResult> UpdateChatOnlineUserAsync(UpdateChatOnlineUserDto chatOnlineUserDto)
        {
            var chatOnlineUser = await _chatOnlineUserService.UpdateChatOnlineUserAsync(chatOnlineUserDto);

            if (chatOnlineUser != null)
            {
                return Ok(chatOnlineUser);
            }
            return BadRequest();
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="chatOnlineUserFriendId"></param>
        /// <returns></returns>
        [HttpPost("chatOnlineUser/addFriend")]
        public async Task<IActionResult> AddFriendAsync(long chatOnlineUserFriendId)
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var chatOnlineUserSelf = await _chatOnlineUserService.GetChatOnlineUserAsync(long.Parse(userId));

            var chatOnlineUserFriend = await _chatOnlineUserService.GetChatOnlineUserAsync(chatOnlineUserFriendId);

            await _friendsRelationService.AddFriendAsync(chatOnlineUserSelf, chatOnlineUserFriend);

            return Ok();
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("chatOnlineUser/friends")]
        public async Task<IActionResult> GetAllFriendsAsync()
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var chatOnlineUserSelf = await _chatOnlineUserService.GetChatOnlineUserAsync(long.Parse(userId));

            var friends = await _friendsRelationService.GetAllFriendsAsync(chatOnlineUserSelf);

            if (friends != null)
            {
                return Ok(friends);
            }

            return NotFound();
        }

        /// <summary>
        /// 查询与好友的所有聊天记录
        /// </summary>
        /// <param name="chatOnlineUserFriendId"></param>
        /// <returns></returns>
        [HttpGet("chatOnlineUser/friend/{chatOnlineUserFriendId}/records")]
        public async Task<IActionResult> GetAllFriendsAsync(long chatOnlineUserFriendId)
        {
            var userId = User.Claims.Where(x => x.Type == "id").FirstOrDefault().Value;

            var records = await _chatRecordService.GetChatRecordsAsync(long.Parse(userId), chatOnlineUserFriendId);

            if (records != null)
            {
                return Ok(records);
            }

            return NotFound();
        }
    }
}
