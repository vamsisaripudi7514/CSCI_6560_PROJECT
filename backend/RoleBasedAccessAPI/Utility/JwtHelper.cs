using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RoleBasedAccessAPI.Utility
{
    public static class JwtHelper
    {
        private static readonly string SecretKey = "MTSU2025!SuperSecureJWTKey2025!ExtraSecure";
        private static readonly byte[] Key = Encoding.ASCII.GetBytes(SecretKey);

        public static string GenerateJwtToken(string username, int userId, string password)
        {
            var claims = new[]
            {
                new Claim("Username", username),
                new Claim("UserId", userId.ToString()),
                new Claim("Password", password)  // ✅ Storing password (Only for development; not recommended in production)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}



#region old code
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;

//namespace RoleBasedAccessAPI.Utility
//{
//    public static class JwtHelper
//    {
//        //private static readonly string SecretKey = "your-super-secret-key";
//        //private static readonly string SecretKey = "MTSU2025!SuperSecureJWTKey2025!";
//        private static readonly string SecretKey = "MTSU2025!SuperSecureJWTKey2025!ExtraSecure";


//        //private static readonly byte[] Key = Encoding.UTF8.GetBytes(SecretKey);
//        private static readonly byte[] Key = Encoding.ASCII.GetBytes(SecretKey);


//        public static string GenerateJwtToken(string Username, string Password)
//        {
//            var claims = new[]
//            {
//                new Claim("Username", Username),
//                new Claim("Password", Password)
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddHours(2),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
#endregion