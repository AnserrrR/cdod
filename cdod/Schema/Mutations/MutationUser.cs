using cdod.Schema.InputTypes;
using cdod.Schema.Mutations;
using cdod.Services;
using cdod.Models;
using Microsoft.Extensions.Options;
using cdod.Schema.OutputTypes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace cdod.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationUser
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<string> Login(ParentLoginInput user, [ScopedService] CdodDbContext dbCOntext, [Service] IOptions<TokenSettings> tokenSettings)
        {
            
            // ДОБАВИТЬ ШИФРОВКУ ЮЗЕРОВ пароль всё такое
            var currentUser = dbCOntext.Users.Where(u => u.Email == user.email &&
                        u.Password == user.password).FirstOrDefault();
            
            if (currentUser != null)
            {
                tokenSettings.Value.isAdmin = currentUser.IsAdmin;
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var jwtToken = new SecurityTokenDescriptor
                {
                    Expires = DateTime.Now.AddMinutes(20),
                    SigningCredentials = credentials,
                    Audience = tokenSettings.Value.Audience,
                    Issuer = tokenSettings.Value.Issuer,
                };
                
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateJwtSecurityToken(jwtToken);

                // Куда лучше добавлять админа либо в хеддер либо в пейлоад.
                token.Header.Add("isAdmin", currentUser.IsAdmin);

                return tokenHandler.WriteToken(token);
            }
            return string.Empty;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async void Registration(ParentLoginInput user, [ScopedService] CdodDbContext dbCOntext)
        {
            // Здесь должен быть обработчик вхлода + возврат токена

        }
    }
}
