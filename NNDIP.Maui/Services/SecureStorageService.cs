using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Services
{
    public static class SecureStorageService
    {
        private static string key = "jwtToken";

        public async static Task<string> Get()
        {
            return await SecureStorage.Default.GetAsync(key);
        }

        public async static Task<bool> Contains()
        {
            string jwtToken = await SecureStorage.Default.GetAsync(key);
            return jwtToken != null;
        }

        public async static void Set(string jwtToken)
        {
            await SecureStorage.Default.SetAsync(key, jwtToken);
        }

        public async static void Remove()
        {
            if (await Contains())
            {
                SecureStorage.Default.Remove(key);
            }
        }
    }
}

