using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Core.Token;
using seecreativa_backend.Users.Entities;
using seecreativa_backend.Users.Models;
using seecreativa_backend.Users.Persistance;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace seecreativa_backend.Users.Repositories
{
    public interface IAuthRepository
    {
        public Task<AuthResponseDto?> LogIn(AuthLogInDto logInDto);

        public User? Logged(string token);
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly IMongoCollection<User> _collection;

        public AuthRepository(IOptions<MongoDbSettings> settings, IOptions<TokenSettings> tokenSettings)
        {
            _collection = new UsersContext(settings).Users;
            _tokenSettings = tokenSettings;
        }

        public async Task<AuthResponseDto?> LogIn(AuthLogInDto logInDto)
        {
            var user = await _collection.Find(x => x.Username == logInDto.Username).FirstOrDefaultAsync();
            if (user != null)
            {
                if (User.ComparePasswords(user.PasswordHash, logInDto.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_tokenSettings.Value.TokenKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return new AuthResponseDto
                    {
                        Token = tokenHandler.WriteToken(token),
                        User = user.ToResponse(),
                    };
                }
            }
            return null;
        }

        public User? Logged(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_tokenSettings.Value.TokenKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(c => c.Type == "nameid").Value;

                var user = _collection.Find(x => x.Id.ToString() == userId).FirstOrDefault();

                if (user != null) return user;
            }
            catch (Exception) { }
            return null;
        }
    }
}
