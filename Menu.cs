#nullable disable
using SimplePasswordHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingDemo
{
    internal class Menu
    {
        private static string _password = "";
        private static string _salt = "";
        private static string _passwordHash = "";

        public static bool SaltsEnabled { get; private set; } = true;

        public static string[] Prompts { get; private set; } = {
            $"saltsEnabled = {SaltsEnabled}",
            "",
            "1. Set secret",
            "2. Verify secret",
            "3. Salting Demo",
            "4. Verify files",
            "",
            "q. Quit"
        };

        /// <summary>
        /// Menu entry point. This method takes the input supplied by the user 
        /// and calls a method that performs the specified action.
        /// </summary>
        /// <param name="input">User input</param>
        public static void ProcSelection(int input)
        {
            switch (input)
            {
                case 1: //Set secret
                    SetSecret();
                    break;
                case 2: //Verify secret
                    VerifySecret();
                    break;
                case 3: //Salting demo!
                    SaltingDemo();
                    break;
                case 4: //Verify files
                    VerifyFiles();
                    break;
            }
        }

        /// <summary>
        /// Sets the "password" and it's corrisponding hash for the purpose of demonstrating how 
        /// hashing is used to store a password without actually needing to know what the password
        /// is in order to verify it.
        /// </summary>
        /// <param name="tooShort">used to reprompt user if the password was too short</param>
        private static void SetSecret(bool tooShort = false)
        {
            Console.Clear();                                            
            if (tooShort) Console.WriteLine("Password must be at " +            //prevent empty passwords
                "least 4 characters long... Try again.");

            Console.Write("Please set the new secret: ");                       //Get the password from the user
            _password = Console.ReadLine();                                     //normally you would not save
                                                                                //this as it would defeat the 
                                                                                //purpose of hashing the password
            
            if (_password.Length < 4) SetSecret(true);                          //don't allow any password less
                                                                                //than 4 characters

            if (SaltsEnabled) _salt = Hasher.GenerateSalt();                    //Generate a salt or reset the salt 
            else _salt = "";                                                    //if salting is disabled

            _passwordHash = Hasher.Compute(_password + _salt);                 //compute the hash from the plaintext
                                                                               //password and the salt (if applied).   

            Console.WriteLine($"The password has been set to '{_password}'");  
            Console.WriteLine($"The hash associated with the password " +
                $"'{_password}' and the salt '{_salt}' is " +
                $"'{_passwordHash}'");
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();                                                                                                                  
        }

        /// <summary>
        /// Asks the user to enter a password. Combines the password with the salt (if enabled when password was set)
        /// then runs the password through Hasher.Compute to get a hash. If the newly computed hash matches the stored
        /// hash for the password, it tells the user access granted. If not it says accesss denied. Reguardless, it will 
        /// show the user the password they entered, the correct password, and the hash values for each. This allows 
        /// users of this demo to "see" how hashing can be used to verify passwords.
        /// </summary>
        private static void VerifySecret()
        {
            Console.Clear();

            if (_password == "")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("You must first set a password (option 1)!");

                Console.Write("\nPress any key to continue...");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ReadKey();

                return;
            }

            Console.Write("Please enter secret: ");
            string input = Console.ReadLine();
            string inputHash = Hasher.Compute(input + _salt);

            Console.WriteLine("\n------------------------------------- A Look at the Data -------------------------------------\n");
            Console.WriteLine($"           Stored Pass: {_password} (this would have been deleted not stored in real app)");
            Console.WriteLine($"           Stored Salt: {_salt}");
            Console.WriteLine($"           Stored Hash: {_passwordHash}\n");

            Console.WriteLine($"          Pass Entered: {input}");
            Console.WriteLine($"Hash From Entered Pass: {inputHash}\n");

            Console.WriteLine($"          Hashes Match: {inputHash == _passwordHash}");

            if (inputHash == _passwordHash)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\nAccess Granted! The hash of the password you entered matches the " +
                    "hash of the correct password");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\nAccess Denied! The hash of the password you entered was '{inputHash}' " +
                    $"the hash of the correct password is '{_passwordHash}'");
            }

            Console.Write("\nPress any key to continue...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey();
        }

        private static void SaltingDemo()
        {
            Console.Clear();
            Console.WriteLine("Welcome! This short demo will demonstrate the importance of salting!\n");

            Console.Write("Please enter a password to demonstrate: ");
            string password = Console.ReadLine();
            string[] blandHash = {
                Hasher.Compute(password),
                Hasher.Compute(password),
                Hasher.Compute(password)
            };

            Console.WriteLine($"\nWithout salting, everyone who used the password '{password}' would have the following hashes...\n");
            
            Console.WriteLine($"     User 1: {blandHash[0]}");
            Console.WriteLine($"     User 2: {blandHash[1]}");
            Console.WriteLine($"     User 3: {blandHash[2]}\n");

            Console.WriteLine("This means if User 3 had their password compromised and an attacker gained access to your \n" +
                "hash tables (stored passwords), they could see that all 3 users had the same password. And, since they \n" +
                "know User 3's password and they know Users 1 and 2 use the same password, they'd also know User 1 and 2s' \n" +
                "passwords!\n");

            //lets compute the hashes again, but this time generate a random salt string to add to the end of the password.
            //This adds randomness that makes significantly changes the resulting hashes. Normally you would save the salt
            //values with the password so you can still validate the resulting hashes.
            string[] saltyHash = {
                Hasher.Compute(password + Hasher.GenerateSalt()),
                Hasher.Compute(password + Hasher.GenerateSalt()),
                Hasher.Compute(password + Hasher.GenerateSalt())
            };

            Console.WriteLine($"If we hash all three passwords, but add some random data to the end of each one (salt) then all \n" +
                $"the users with the password '{password}' would have the following hashes...\n");
            
            Console.WriteLine($"     User 1: {saltyHash[0]}");
            Console.WriteLine($"     User 2: {saltyHash[1]}");
            Console.WriteLine($"     User 3: {saltyHash[2]}\n");
            
            Console.WriteLine($"As you can see, even though all users used '{password}' as their password, all their hashes are \n" +
                $"different. If you store the salt along with the password, you can perform the same verification process as an \n" +
                $"unsalted hash by adding the salt string before you hash the password to verify! This doesn't help users with \n" +
                $"weak/common passwords since an attacker can just calculate the hashes with the salt added, but they need to \n" +
                $"calculate the passwords from scratch instead of using a rainbow table that contains pre-hashed weak passwords. \n" +
                $"Cool isn't it!");

            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void VerifyFiles()
        {
            var filesDirectory = new DirectoryInfo("files");
            var files = filesDirectory.GetFiles();

            Console.Clear();
            Console.WriteLine("Welcome to the file integrety demo! Please enter two files to compare from the files folder.");

            Console.WriteLine("\nHere are the files availabe to check:");
            foreach (var file in files) Console.WriteLine("     " + file.Name);

            string[] filenames = new string[2];
            string[] fileHashes = new string[2];

            Console.WriteLine("\nPlease type the names of the two files you want to compare.\n");

            for (int i = 0; i < filenames.Length; i++)
            {
                Console.Write($"     file {i + 1}: ");
                filenames[i] = Path.Combine("files", Console.ReadLine());

                if (!File.Exists(filenames[i])) 
                {
                    Console.WriteLine($"'{filenames[i]}' not found! Please try again...");
                    i--;
                    continue;
                }

                using (var filestream = new FileStream(filenames[i], FileMode.Open))
                {
                    fileHashes[i] = Hasher.Compute(filestream);
                }
            }

            Console.WriteLine("\n------------------------------------- A Look at the Data -------------------------------------\n");
            Console.WriteLine($"     File 1 name: {filenames[0]}");
            Console.WriteLine($"     File 2 name: {filenames[1]}\n");
            Console.WriteLine($"     File 1 hash: {fileHashes[0]}");
            Console.WriteLine($"     File 2 hash: {fileHashes[1]}\n");

            if (fileHashes[0] == fileHashes[1])
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Both files are exactly the same! Both files share the same hash value.");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Files do not match! One of these files has been altered. Their hash values\n " +
                    "do NOT match!");
            }

            Console.Write("\nPress any key to continue...");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey();
        }
    }
}
