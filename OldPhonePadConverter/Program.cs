using System;
using System.Collections.Generic;
using System.Text;



namespace OldPhonePadConverter
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("33#")); // Output: E
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("227*#")); // Output: B
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("4433555 555666#")); // Output: HELLO
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("8 88777444666*664#")); // Output: ????? (input might have a typo)
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("8  #88777444666**#664##")); // multiple command occurrence check
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("  **8  88777444666*664#")); // start with command check
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("#")); // Empty check
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("  **#")); // only Commands check
            Console.WriteLine(OldPhonePadConverter.OldPhonePad("**  #")); // only Commands check
            //Console.WriteLine(OldPhonePadConverter.OldPhonePad("8 88777444666*6X4#")); // Invalid input check
            //Console.WriteLine(OldPhonePadConverter.OldPhonePad("")); // Empty check
        }
    }

    public class OldPhonePadConverter
    {
        private const char terminateCharacter = '#';
        private const char backSpaceCharacter = '*';
        private const char resetCharacter = ' ';

        public static string OldPhonePad(string input)
        {
            //input validity check
            if (string.IsNullOrEmpty(input) || input[input.Length - 1] != terminateCharacter)
            {
                throw new ArgumentException("Input must not be empty and must end with '#'");
            }

            // Mapping of keys to their corresponding characters
            var keyMapping = new Dictionary<char, string>
        {
            {'1', "&'("},
            {'2', "ABC"},
            {'3', "DEF"},
            {'4', "GHI"},
            {'5', "JKL"},
            {'6', "MNO"},
            {'7', "PQRS"},
            {'8', "TUV"},
            {'9', "WXYZ"},
            {'0', " "},

        };

            var output = new StringBuilder();
            char currentKey = '\0';
            int pressCount = 0;
            bool reset = false;

            foreach (char c in input)
            {
                //check for commands
                if (c == terminateCharacter)
                {
                    // End of input
                    break;
                }
                else if (c == backSpaceCharacter)
                {
                    // Backspace
                    if (output.Length > 0)
                    {
                        output.Remove(output.Length - 1, 1); // Remove the last character
                        reset = true;
                    }
                }
                else if (c == resetCharacter)
                {
                    reset = true;

                }
                //check number map
                else
                {
                    if (reset || c != currentKey)
                    {
                        // Reset press count if there is a reset or a different key is pressed
                        pressCount = 0;
                        reset = false;
                    }

                    currentKey = c;
                    pressCount++;

                    if (keyMapping.ContainsKey(currentKey))
                    {
                        // Calculate the index of the character to append
                        int index = (pressCount - 1) % keyMapping[currentKey].Length;
                        char charToAppend = keyMapping[currentKey][index];

                        // Replace the last character if we are still pressing the same key
                        if (output.Length > 0 & pressCount > 1)
                        {
                            output.Remove(output.Length - 1, 1);
                        }

                        output.Append(charToAppend);
                    }
                    else
                        throw new ArgumentException("Invalid input character - " + currentKey);
                }
            }

            return output.ToString();
        }
    }

}
