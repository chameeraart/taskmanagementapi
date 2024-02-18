using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using taskmanagementapi.Auth;
using taskmanagementapi.Models;
using taskmanagementapi.Services;

namespace taskmanagementapi.Controllers
{

    public class AuthController : ControllerBase
    {
        private TaskDbContext _context;

        public SessionManager SessionManager { get; }


        public AuthController(TaskDbContext context, SessionManager sessionManager)
        {
            _context = context;
            SessionManager = sessionManager;
        }

        //[HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]  LoginDto loginDto)
        {

            // TODO: Encrypt password
            var user = _context.users
                .Where(t =>
                    t.username == loginDto.Username &&
                    t.password == loginDto.Password &&
                    t.isactive == true
                 )
                 .FirstOrDefault();

            if (user == null)
            {
                return BadRequest("User name or Password Error");
            }


            // admin ?
            var claims = new List<Claim>();
            if (user.UserType == taskmanagementapi.Models.Users.UserTypes.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            // symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(SessionManager.salt);

            // signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // add claims
            claims.Add(new Claim(ClaimTypes.Role, "user"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            // create token
            var token = new JwtSecurityToken
            (
                issuer: SessionManager.Issuer,
                audience: SessionManager.Audiance,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: signingCredentials,
                claims: claims
            );


            return new ObjectResult(new UserDto()
            {
                JWT = new JwtSecurityTokenHandler().WriteToken(token),
                UserID = user?.Id,
                JwtValidFrom = token.ValidFrom,
                JwtValidTo = token.ValidTo,
                TokenType = "bearer",
                userrole = user?.UserType.ToString(),
                username = user?.username,

            });


        }




        public IActionResult myUserLogin(Users user)
        {
            var userdetail = _context.users.Where(x => x.username == user.username).SingleOrDefault();
            if (userdetail != null)
            {
                return new ObjectResult(userdetail.Id);
            }
            else
            {
                return new ObjectResult(0);
            }

        }
    }
}
