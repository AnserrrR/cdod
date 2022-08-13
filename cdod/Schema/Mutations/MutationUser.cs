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
using System.Security.Cryptography;

namespace cdod.Schema
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationUser
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<TokenResponseType> Login(LoginInput loginInput, [ScopedService] CdodDbContext dbContext, 
            [Service] IOptions<TokenSettings> tokenSettings)
        {
            var result = new TokenResponseType { Message = "Success" };
            
            if (string.IsNullOrEmpty(loginInput.Email)
                || string.IsNullOrEmpty(loginInput.Password))
            {
                result.Message = "Invalid Credentials";
                return result;
            }

            var user = dbContext.Users.Where(_ => _.Email == loginInput.Email).FirstOrDefault();
            if (user == null)
            {
                result.Message = "Invalid Credentials";
                return result;
            }

            //Проверять с хэшированным паролем
            if (user.Password != loginInput.Password)
            {
                result.Message = "Invalid Password";
                return result;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Непонятно что с ролями пока
            string role = user.IsAdmin ? "admin" : "user";

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtToken = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: credentials,
                audience: tokenSettings.Value.Audience,
                issuer: tokenSettings.Value.Issuer,
                claims: claims
            );

            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            result.RefreshToken = GenerateRefreshToken();

            user.RefreshToken = result.RefreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            return result;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async void Registration(LoginInput user, [ScopedService] CdodDbContext dbContext)
        {
            // Здесь должен быть обработчик вхлода + возврат токена

        }

        [UseDbContext(typeof(CdodDbContext))]
        public TokenResponseType RenewAccessToken(RenewTokenInput renewToken, 
            [ScopedService] CdodDbContext dbContext, [Service] IOptions<TokenSettings> tokenSettings)
        {
            var result = new TokenResponseType { Message = "Success" };

            var principal = GetClaimsFromExpiredToken(renewToken.AccessToken, tokenSettings);

            if (principal == null)
            {
                result.Message = "Invalid Token";
                return result;
            }

            string email = principal.Claims.Where(_ => _.Type == ClaimTypes.Email).Select(_ => _.Value).FirstOrDefault();
            if (string.IsNullOrEmpty(email))
            {
                result.Message = "Invalid Token";
                return result;
            }



            var user = dbContext.Users
                    .Where(_ => _.Email == email && _.RefreshToken == renewToken.RefreshToken 
                                                 && _.RefreshTokenExpiration > DateTime.UtcNow)
                    .FirstOrDefault();
            if (user == null)
            {
                result.Message = "Invalid Credentials";
                return result;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Непонятно что с ролями пока
            string role = user.IsAdmin ? "admin" : "user";

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var jwtToken = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: credentials,
                audience: tokenSettings.Value.Audience,
                issuer: tokenSettings.Value.Issuer,
                claims: claims
            );

            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            result.RefreshToken = GenerateRefreshToken();

            user.RefreshToken = result.RefreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            dbContext.Users.Update(user);
            dbContext.SaveChanges();

            return result;

        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetClaimsFromExpiredToken(string accessToken,
            [Service] IOptions<TokenSettings> tokenSettings)
        {
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidIssuer = tokenSettings.Value.Issuer,
                ValidateIssuer = true,
                ValidAudience = tokenSettings.Value.Audience,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Value.Key)),
                ValidateLifetime = false // ignore expiration
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var principal = jwtHandler.ValidateToken(accessToken, tokenValidationParameter, out SecurityToken securityToken);

            var jwtScurityToken = securityToken as JwtSecurityToken;
            if (jwtScurityToken == null)
            {
                return null;
            }

            return principal;
        }
    }
}
