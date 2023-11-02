using System.Security.Cryptography;                             //this class provides the srytographic services (secure encoding and decoding of data) including hashing, random number
                                                                //generation, and message authentication.
using System.Text;                                              //This class represents ASCII and ASCII character encodings.  Converts characters to and from blocks of bytes.

//This function returns a string and the name of the function

//string hashPassword(string password) {                          //will accept the password
//    var sha = SHA512.Create();                                   //used to hash the password and helps to instantiate

//    var asByteArray = Encoding.Default.GetBytes(password);      //Converts the password into the bytes needed to turn the password into hash text
//    var hashedPassword = sha.ComputeHash(asByteArray);          //Takes the sha variable, uses the ComputeHash function to pass in the Byte array to give us the hashed password 

//    return Convert.ToBase64String(hashedPassword);              //returns a string that was created from the hashed password.
//}

////string password = hashPassword("m#P52s@ap$V");                //pick a password to see it converted
////Console.WriteLine("Hashed Password: " + password);            //shows the password as a hashed password


////DATABASE
//string storedPassword = hashPassword("m#P52s@ap$V");            //Password thats stored 
//Console.WriteLine("Please enter your password: ");              //Prompts the user for a password entry
//string userPassword = Console.ReadLine();                       //Will produce the users password

//while (true) {
//    userPassword = hashPassword(userPassword);                  //the password entered by the user is turned into a hashed password
//    if (userPassword.Equals(storedPassword)) {                  //checks if hashed password is equal to the userPassword
//        Console.WriteLine("You have access to the system");     //if the passwords are equal you get this message
//        break;
//    }
//    Console.WriteLine("Invalid password. Please try again:");   //if the passwords are not equal you will be prompted to try again.
//    userPassword = Console.ReadLine();                          //Gives the user the opportunity to re-enter the password
//}

using HashingDemo;

string input = "";
while (input.ToLower() != "q" && input.ToLower() != "quit")
{
    Console.Clear();
    Console.WriteLine("Welcome!\n");

    foreach (string propmt in Menu.Prompts)
        Console.WriteLine(propmt);

    Console.Write("\nPlease make a selection: ");
    input = Console.ReadLine() ?? "";

    if (int.TryParse(input, out var intParsedInput))
        Menu.ProcSelection(intParsedInput);
}