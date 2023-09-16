using RpgGame.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace RpgGame.Services
{
    public class CryptographyService : ICryptographyService
    {
        public string Sha256Hash(string text)
        {
            StringBuilder Sb = new();

            using (var hash = SHA256.Create())
                hash.ComputeHash(Encoding.UTF8.GetBytes(text))
                    .ToList()
                    .ForEach(b => Sb.Append(b.ToString("x2")));

            return Sb.ToString();
        }
    }
}