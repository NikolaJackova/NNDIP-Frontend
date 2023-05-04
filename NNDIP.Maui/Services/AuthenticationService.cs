using System.IdentityModel.Tokens.Jwt;

namespace NNDIP.Maui.Services
{
    public static class AuthenticationService
    {
        private static JwtSecurityToken JwtSecurityToken;

        private static string jwtTokenKey = "jwtToken";

        public static void Init()
        {
            bool result = Task.Run(ContainsJwtToken).Result;
            if (result)
            {
                JwtSecurityToken = new JwtSecurityToken(Task.Run(GetJwtToken).Result);
            }
        }

        public async static Task<string> GetJwtToken()
        {
            return await SecureStorage.Default.GetAsync(jwtTokenKey);
        }

        public async static void SetJwtToken(string jwtToken)
        {
            JwtSecurityToken = new JwtSecurityToken(jwtToken);
            await SecureStorage.Default.SetAsync(jwtTokenKey, jwtToken);
        }

        public async static void RemoveJwtToken()
        {
            if (await ContainsJwtToken())
            {
                SecureStorage.Default.Remove(jwtTokenKey);
                JwtSecurityToken = null;
            }
        }

        public static string GetUsername()
        {
            if (JwtSecurityToken is not null)
            {
                return JwtSecurityToken.Claims.SingleOrDefault(c => c.Type == "UserName").Value;
            }
            return null;
        }

        private async static Task<bool> ContainsJwtToken()
        {
            string jwtToken = await SecureStorage.Default.GetAsync(jwtTokenKey);
            return jwtToken != null;
        }
    }
}

