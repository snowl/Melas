using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Melas.Client
{
    public class SavedUser
    {
        private static string MacGuid { get; set; }

        public static void Init()
        {
            string MAC = GetMacAddress();
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(MAC));
                Guid result = new Guid(hash);
                MacGuid = result.ToString();

                if (!File.Exists(MacGuid))
                    File.Create(MacGuid).Dispose();
            }
        }

        public static void SaveUser(User user)
        {
            Init();
            if (user == null)
            {
                File.WriteAllText(MacGuid, "");
            }
            else
            {
                user.Password = DPAPI.Encrypt(DPAPI.KeyType.UserKey,
                                                  user.Password,
                                                  null,
                                                  "@$q");

                File.WriteAllText(MacGuid, JsonConvert.SerializeObject(user));
            }
        }

        public static User GetUser()
        {
            Init();
            string Database = File.ReadAllText(MacGuid);
            if (Database.Length == 0)
            {
                return new User();
            }

            User user = JsonConvert.DeserializeObject<User>(Database);
            user.Password = DPAPI.Decrypt(user.Password);
            return user;
        }

        /// <summary>
        /// Gets the mac address of the user.
        /// </summary>
        /// <returns>The mac address of the user</returns>
        private static string GetMacAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled=true");
            IEnumerable<ManagementObject> objects = searcher.Get().Cast<ManagementObject>();
            string mac = (from o in objects orderby o["IPConnectionMetric"] select o["MACAddress"].ToString()).FirstOrDefault();
            if (string.IsNullOrEmpty(mac))
            {
                mac = "empty";
            }
            return mac.Replace(":", "");
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
            this.Username = "";
            this.Password = "";
        }
    }
}
