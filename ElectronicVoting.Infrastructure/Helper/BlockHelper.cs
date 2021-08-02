using System.Security.Cryptography;
using System.Text;
using ElectronicVoting.Common.Model.Blockchain;
using Newtonsoft.Json;

namespace ElectronicVoting.Infrastructure.Helper
{
    public class BlockHelper
    {
        public static byte[] ObjectToByteArray(Block block)
        {
            string json = JsonConvert.SerializeObject(block);
            return Encoding.UTF8.GetBytes(json);
        }

        public static string GetSha256Hash(Block block)
        {
            var sha256 = new SHA256Managed();
            var hashBuilder = new StringBuilder();
            
            byte[] bytes = ObjectToByteArray(block);
            byte[] hash = sha256.ComputeHash(bytes);

            foreach (byte x in hash)
            {
                hashBuilder.Append($"{x:x2}");
            }
            
            return hashBuilder.ToString();
        }
    }
}