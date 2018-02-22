using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    class ConsoleAppUtils
    {

        /// <summary>
        ///  Get Player input until Player inputs integer
        /// </summary>
        /// <returns>Player input (int)</returns>
        public static int GetPlayerInputInt()
        {
            // Initialize a integer valuable for checking
            int parsedInput;

            // Get Player input
            string PlayerInput = GetPlayerInput();

            // Check Player input is integer or not
            if (!int.TryParse(PlayerInput, out parsedInput))
            {
                // It is not integer
                // Show error message for Player
                WriteErrorMessage("Please input integer");

                // Get Player input again
                return GetPlayerInputInt();
            }

            // Return the Player input as int
            return parsedInput;
        }

        /// <summary>
        ///  Get Player input until Player inputs integer
        /// </summary>
        /// <returns>Player input (int)</returns>
        public static int GetPlayerInputInt(int rangeStart, int rangeEnd)
        {
            // Initialize a integer valuable for checking
            int parsedInput = GetPlayerInputInt();

            // if player input was not in the range
            if (parsedInput < rangeStart || rangeEnd < parsedInput)
            {
                Console.WriteLine("Please type the number from {0} to {1}", rangeStart, rangeEnd);
                // Get Player input again
                return GetPlayerInputInt(rangeStart, rangeEnd);
            }

            // Return the Player input as int
            return parsedInput;
        }

        /// <summary>
        ///  Get Player input until Player inputs something
        /// </summary>
        /// <returns>Player input (string)</returns>
        public static string GetPlayerInput()
        {

            SetColorForInput();

            // Read Player input
            string PlayerInput = Console.ReadLine();

            // Check Player input has string or empty
            if (string.IsNullOrEmpty(PlayerInput))
            {
                // It is empty
                // Show error message for Player
                WriteErrorMessage("Please type something and press ENTER");

                // Get Player input again
                PlayerInput = GetPlayerInput();
            }

            Console.ResetColor();

            // Return the Player input as string
            return PlayerInput;
        }

        /// <summary>
        /// Set the text color for the player input
        /// </summary>
        public static void SetColorForInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        /// <summary>
        /// Set the text color for the error message
        /// </summary>
        public static void SetColorForError()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }

        /// <summary>
        /// Show a error message for the user 
        /// </summary>
        /// <param name="message">a error message</param>
        public static void WriteErrorMessage(string message)
        {
            SetColorForError();
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Show a message for the user by centered style
        /// </summary>
        /// <param name="message">a message which want to be shown in centered style</param>
        public static void WriteMessageCenter(string message)
        {
            // Find center by the console size and subtract the message length from it
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            Console.WriteLine(message);
        }

        /// <summary>
        ///  Pause the program for showing console
        /// </summary>
        public static void Pause()
        {
            // Pause the program
            Console.ReadLine();
        }

    }
}
