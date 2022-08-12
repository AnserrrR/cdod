using cdod.Schema.InputTypes;
using cdod.Schema.Mutations;
using cdod.Services;
using cdod.Models;
using Microsoft.Extensions.Options;
using cdod.Schema.OutputTypes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace cdod.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationUser
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<string> Login(LoginInput user, [ScopedService] CdodDbContext dbContext, 
            [Service] IOptions<TokenSettings> tokenSettings)
        {
            
            // ДОБАВИТЬ ШИФРОВКУ ЮЗЕРОВ пароль всё такое
            var currentUser = dbContext.Users.Where(u => u.Email == user.Email &&
                        u.Password == user.Password).FirstOrDefault();
            
            if (currentUser != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Непонятно что с ролями пока
                string role = currentUser.IsAdmin ? "admin" : "user";

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, role)
                };

                var jwtToken = new JwtSecurityToken
                (
                    expires : DateTime.Now.AddHours(1),
                    signingCredentials : credentials,
                    audience : tokenSettings.Value.Audience,
                    issuer : tokenSettings.Value.Issuer,
                    claims : claims
                );
                

                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            return "Not found";
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async void Registration(LoginInput user, [ScopedService] CdodDbContext dbCOntext)
        {
            // Здесь должен быть обработчик вхлода + возврат токена

        }
    }
}
