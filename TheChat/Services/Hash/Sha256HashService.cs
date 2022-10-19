using System.Security.Cryptography;
using System.Text;

namespace TheChat.Services.Hash
{
    public class Sha256HashService : IHashService
    {
        private SHA256 _sha256dEncoder;

        public Sha256HashService()
        {
            _sha256dEncoder = SHA256.Create();
        }


        /// <summary>
        /// Determine if the hashes are equal
        /// </summary>
        /// <returns>True if values are equal. Otherwise false</returns>
        public bool CheckEquality(string firtsHash, string secondHash)
        {
            return SHA256.Equals(firtsHash, secondHash);
        }


        /// <summary>
        /// Generate salt. Result depends on DateTime
        /// </summary>
        public string GenerateSalt()
        {
            return DateTime.Now.ToString();
        }


        /// <summary>
        /// Compute hash of given string
        /// </summary>
        /// <exception cref="ArgumentNullException">toHash argument is null</exception>
        public string Hash(string toHash)
        {
            if (toHash is null)
                throw new ArgumentNullException(nameof(toHash));

            var bytes = Encoding.Default.GetBytes(toHash);

            var hash = _sha256dEncoder.ComputeHash(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
