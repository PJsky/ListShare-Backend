using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ListCoreApp.Helpers
{
    public static class PasswordHasher
    {
        public static (string, byte[]) Hash(string password) {
            var salt = Salt();
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return (hashedPassword, salt);
        }

        private static byte[] Salt() {
            byte[] salt = new byte[128 / 8];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        
    }
}
