using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CurrencyExchangeRate.WebApi.Core;
using CurrencyExchangeRate.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyExchangeRate.WebApi.Controllers;

public class AccountController: Controller
{
    private List<User> testUsers = new List<User>
    {
        new User
        {
            Login = "admin@test.ru", Password = "test_123", Role = "admin"
        },
        new User
        {
            Login = "user@test.ru", Password = "test_321", Role = "user"
        }
    };

    [HttpPost("/token")]
    public IActionResult GetToken(string username, string password)
    {
        var identity = GetIdentity(username, password);
        if (identity == null)
        {
            return BadRequest("Not Validate");
        }
 
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.PUBLISHER,
            audience: AuthOptions.CLIENT,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
 
        return Json(response);
    }

    private ClaimsIdentity GetIdentity(string username, string password)
    {
        User user = testUsers.FirstOrDefault(u => u.Login == username && u.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
 
        // если пользователя не найдено
        return null;
    }
}