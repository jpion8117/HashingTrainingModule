using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimplePasswordHash
{
    internal class Hasher
    {
        /// <summary>
        /// Compute a hash as a string 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Compute(byte[] data)
        {
            string hash;                                    //Create a variable to store the computed hash

            using (var sha256 = SHA256.Create())            //Create an sha512 instance to compute the hash
            {
                var hashData = sha256.ComputeHash(data);    //Take the input data and compute it's hash as an array
                                                            //of bytes

                hash = ConvertBytes(hashData);              //Encode the byte array as a string to make it easier to 
                                                            //save the data.
            }

            return hash;                                    //Return the computed hash.
        }

        /// <summary>
        /// Compute a hash as a string 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Compute(Stream data)
        {
            string hash;                                    //Create a variable to store the computed hash

            using (var sha256 = SHA256.Create())            //Create an sha512 instance to compute the hash
            {
                var hashData = sha256.ComputeHash(data);    //Take the input data and compute it's hash as an array
                                                            //of bytes

                hash = ConvertBytes(hashData);              //Encode the byte array as a string to make it easier to 
                                                            //save the data.
            }

            return hash;                                    //Return the computed hash.
        }


        public static byte[] ConvertString(string str)
        {
            byte[] strBytes = new byte[0];

            strBytes = Encoding.ASCII.GetBytes(str);    //Using the ASCII encoding method, convert the input string into 
                                                        //an array of bytes

            return strBytes;
        }

        public static string ConvertBytes(byte[] bytes)
        {
            string str = "";

            str = Convert.ToHexString(bytes);       //Using the ASCII encoding method, convert an array of bytes to into
                                                    //a string.

            return str;
        }

        public static string GenerateSalt()
        {
            string salt = "";                                           //create a string to store the salt.

            using (var random = RandomNumberGenerator.Create())         //generate a random salt using System.Cryptography
            {
                int length = 32;                                        //length of the salt array

                byte[] randomBytes = new byte[length];                  //byte array to store random bytes that will make
                                                                        //up the salt

                random.GetBytes(randomBytes, 0, length);                //using the Random Number Generator, get the random
                                                                        //bytes that will form the salt

                salt = Hasher.ConvertBytes(randomBytes);                //convert the random bytes array to a string.
            }

            return salt;
        }
    }
}
