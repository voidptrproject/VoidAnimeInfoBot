using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VoidAnimeInfoBot
{
    public class Users
    {
        public static async Task Initialize()
        {
            if (!File.Exists("Users.json"))
                await File.WriteAllTextAsync("Users.json", "{ \"admins\" : [] }");
        }

        public static async Task<string> ReadFile()
        {
            return await File.ReadAllTextAsync("Users.json");
        }

        public static async Task<bool> IsAdmin(int id)
        {
            dynamic data = JsonConvert.DeserializeObject(await ReadFile())!;
            List<int> admins = data.Admins;

            return admins.Contains(id);
        }

        public static async Task AddAdmin(int id)
        {
            dynamic data = JsonConvert.DeserializeObject(await ReadFile())!;
            List<int> admins = data.Admins;

            if (!admins.Contains(id))
            {
                admins.Add(id);
                data.Admins = admins;
                
                await File.WriteAllTextAsync("Users.json", JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }
    }
}
