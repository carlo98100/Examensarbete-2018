using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel
{
    class Encrypter
    {
        
        public static void MD5Hash(string input)    //Hashar lösenordet.
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));   //x2 är för att skriva koden i hexadecimal.
            }
            Form1.hashed = hash.ToString();            
        }
    }
}
