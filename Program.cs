using System.Security.Cryptography;                             //this class provides the srytographic services (secure encoding and decoding of data) including hashing, random number
                                                                //generation, and message authentication.
using System.Text;                                              //This class represents ASCII and ASCII character encodings.  Converts characters to and from blocks of bytes.
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