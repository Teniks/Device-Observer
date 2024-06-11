using System.Security.Cryptography;
using System.Text;

namespace Device_Observer.Models
{
    internal class Hashing
    {
        public static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder @string = new StringBuilder();
                for(int i = 0; i < hashBytes.Length; i++)
                {
                    @string.Append(hashBytes[i].ToString("x2"));
                }

                return @string.ToString();
            }
        }
    }
}
