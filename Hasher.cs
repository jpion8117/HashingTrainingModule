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
        /// Compute a hash and return a string representing the hash 
        /// value as a string of hexadecimal characters. This overload 
        /// takes in a string to be hashed.
        /// </summary>
        /// <param name="data">Plaintext string you are hashing</param>
        /// <returns>String of hexadecimal characters representing the 
        /// hashing algorithm output</returns>
        public static string Compute(string data)
        { 
            //Create a variable to store the computed hash
            string skillet = "";

            using (var sha256 = SHA256.Create())                //Create an sha512 instance to compute the hash
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);   //strings can not be directly passed to the SHA256
                                                                //ComputeHash method. They must first be converted
                                                                //to a data type the method can accept. In this case
                                                                //we chose to convert them into an array of bytes,
                                                                //but there are other conversions that would also
                                                                //work. Such as converting the string to some type
                                                                //of stream object, but that is beyond the scope of
                                                                //this demo.

                var hashData = sha256.ComputeHash(dataBytes);   //Take the input data and compute it's hash as an array
                                                                //of bytes

                skillet = Convert.ToHexString(hashData);        //Encode the byte array as a string to make it easier to 
                                                                //save the data.
            }
            return skillet;
        }

        /// <summary>
        /// Compute a hash and return a string representing the hash 
        /// value as a string of hexadecimal characters. This overload 
        /// takes in a Stream to be hashed.
        /// </summary>
        /// <param name="data">Data stream you are hashing</param>
        /// <returns>String of hexadecimal characters representing the 
        /// hashing algorithm output</returns>
        public static string Compute(Stream data)
        {
            //Create a variable to store the computed hash
            string skillet = "";

            /******************** your code goes here! ********************/

            return skillet; 
        }

        /// <summary>
        /// Generates a random hexadecimal string to use as a salt. Make sure to store 
        /// this salt somewhere with the hashed password or you won't be able to generate 
        /// the correct hash to validate the password!
        /// </summary>
        /// <param name="size">Number of random bytes to generate, default is 32</param>
        /// <returns>randomly generated hexadecimal string</returns>
        public static string GenerateSalt(int size = 32)
        {
            //create a string to hold the generated salt.
            string saltShaker = "";

            /******************** your code goes here! ********************/

            return saltShaker;
        }
    }
}
