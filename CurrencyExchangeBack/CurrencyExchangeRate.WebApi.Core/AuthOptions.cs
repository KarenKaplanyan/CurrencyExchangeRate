using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyExchangeRate.WebApi.Core;

public class AuthOptions
{
    public const string PUBLISHER = "TestAuthServer";
    public const string CLIENT = "TestAuthClient";
    const string KEY = "abracadabra_key!123";
    public const int LIFETIME = 1;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}