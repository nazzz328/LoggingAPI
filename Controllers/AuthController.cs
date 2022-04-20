using LoggingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        IMongoCollection<User> _usersCollection;
        IConfiguration _configuration;

        public authController(IMongoDatabase database, IConfiguration configuration)
        {
            _usersCollection = database.GetCollection<User>("users");
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
      public async Task <ActionResult<string>> Login (UserDto request)
        {
            string token;
            try
            {
                User user = await _usersCollection.Find(u => u.Username == request.Username).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }
                token = CreateToken(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(token);
        }

        private string CreateToken (User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpPost]
        [Route("createAndAuthAdmin")]
        public async Task<ActionResult<User>> CreateAndLoginAdmin(UserDto request)
        {
            string token;
            try
            {
                var users = await _usersCollection.Find(_ => true).ToListAsync();
                if (!users.Count().Equals(0))
                {
                    return BadRequest("Admin is already registered.");
                }
                User user = new User();
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Username = request.Username;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _usersCollection.InsertOneAsync(user);
                token = CreateToken(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
