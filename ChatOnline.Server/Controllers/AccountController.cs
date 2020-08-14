using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChatOnline.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        private readonly List<Account> _accounts = new List<Account>()
        {
            new Account("1","aa","aaa"),
            new Account("2","bb","bbb"),
            new Account("3","cc","ccc")
        };

        public AccountController(ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // [HttpGet]
        // public IActionResult Index()
        // {
        //     return null;
        // }

        /// <summary>
        /// 登录，获取token
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto userDto)
        {
            var res = _accounts.FirstOrDefault(x => x.Name == userDto.Name && x.Password == userDto.Password);
            if (res == null)
            {
                return BadRequest();
            }
            try
            {
                string iss = _configuration["LoginInfo:Issurer"];
                string aud = _configuration["LoginInfo:Audience"];

                IEnumerable<Claim> claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Id,res.UserId.ToString()),
                    new Claim(JwtClaimTypes.Name,res.Name),
                };

                var nbf = DateTime.UtcNow;
                var Exp = DateTime.UtcNow.AddSeconds(2592000);//30天

                string sign = _configuration["LoginInfo:SecretCredentials"];
                var secret = Encoding.UTF8.GetBytes(sign);

                var key = new SymmetricSecurityKey(secret);
                var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims: claims, notBefore: nbf, expires: Exp, signingCredentials: signcreds);
                var JwtHander = new JwtSecurityTokenHandler();
                var token = JwtHander.WriteToken(jwt);

                return Ok(new
                {
                    access_token = token,
                    token_type = "Bearer",
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

    /// <summary>
    /// 账户
    /// </summary>
    public class Account
    {
        public Account(string userId, string name, string password)
        {
            UserId = userId;
            Name = name;
            Password = password;
        }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public User(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<User> Friends { get; private set; }

        /// <summary>
        /// 增加好友
        /// </summary>
        /// <param name="user"></param>
        public void AddFriend(User user)
        {
            Friends.Add(user);
        }
    }

    public class LoginRequestDto
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}