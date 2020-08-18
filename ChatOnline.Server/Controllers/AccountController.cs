using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ChatOnline.Server.Models;
using ChatOnline.Server.ViewModels;
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

        private readonly IMDbContext _dbContext;

        public AccountController(ILogger<AccountController> logger,
            IConfiguration configuration,
            IMDbContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
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
            var res = _dbContext.IMUsers.FirstOrDefault(x => x.IMNumber == userDto.IMNumber && x.Password == userDto.Password);
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
                    new Claim(JwtClaimTypes.Id,res.IMNumber.ToString()),
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

                return Ok(new LoginResponseDto()
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
}