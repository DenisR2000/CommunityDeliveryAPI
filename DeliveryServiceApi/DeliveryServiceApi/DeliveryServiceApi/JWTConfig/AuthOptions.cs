using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeliveryServiceApi.JWTConfig
{
    public class AuthOptions
    {
        public const string ISSURER = "MVCDiaryLine";
        public const string AUDIENCE = "SomeClient";
        const string KEY = "MyKeyWith_256_BIT_Information";
        public const int LIFETIME = 65;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.Default.GetBytes(KEY));
        }
    }
}
