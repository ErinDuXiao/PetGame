using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using System.Media;

namespace PG10ObjectsAndClasses
{
    class Program
    {
        // Initialize a number const to keep how many actions user can do per a day
        const int ACTIONS_NUM_PER_DAY = 2;

        // Initialize alternative
        static string[] MENU = {

            "Feed",
            "See",
            "Change the water for",
            "Leave",
            "Give a new plant for"
        };

        // Initialize a new random instanse
        static Random random = new Random();

        static void Main(string[] args)
        {
            // Set some infomation to initialize the console
            InitializeConsole();

            // Initialize continue game flg to keep the game until player wanted to stop
            bool continueGameFlg = true;

            // Keep playing until player wanted to stop
            while (continueGameFlg)
            {
                // Execute new game and get if player 
                continueGameFlg = GameExecute();
            }

            // Greeting player to let them play again
            ConsoleAppUtils.WriteMessageCenter("See you, again! <^ >< ~");

            // Keep console can be seen for the player
            ConsoleAppUtils.Pause();
        }

        /// <summary>
        /// Execute a game
        /// </summary>
        /// <returns>boolean that shows player</returns>
        static bool GameExecute()
        {
            // Show the title display
            ShowTitle();
            
            // Hatch fish egg and get the name of it
            string fishName = HatchFish();

            // Create an instance of the pet!
            Pet myPet = new Pet(fishName);

            // Initialize day counter
            int dayCounter = 0;

            // Keep playing until fish died
            while (!Pet.PetLivingStatus.DEAD.Equals(myPet.GetLivingStatus()))
            {
                // Add 1 day
                dayCounter++;

                // Let player do 2 actions per day
                for (int actionCount = 0; actionCount < ACTIONS_NUM_PER_DAY; actionCount++)
                {

                    // Display day counter
                    Console.WriteLine("Day {0}",dayCounter);
                    Console.WriteLine();

                    // Let player take care of the pet
                    TakeCareOf(myPet);

                    // Clear console
                    Console.Clear();

                    if (Pet.PetLivingStatus.DEAD.Equals(myPet.GetLivingStatus()))
                    {
                        break;
                    }
                }

                // Display day infomation
                Console.WriteLine("Day {0} over", dayCounter);

                // Write a report
                WriteReportAbout(myPet);

                // Keep the report being shown
                ConsoleAppUtils.Pause();

                // Clear console
                Console.Clear();
            }
            Console.Clear();

            // Show the message to end
            Console.WriteLine("+++ Day " + dayCounter + " +++");
            Console.WriteLine();
            Console.Write("Because of ");
            // Write every status of the pet
            foreach (Pet.PetDetailedStatus status in myPet.GetDetailedStatus())
            {
                // Evaluate the status to show the report about the pet
                switch (status)
                {
                    case Pet.PetDetailedStatus.FINE:
                        break;

                    case Pet.PetDetailedStatus.hungry:
                        Console.Write("hungry...");
                        break;

                    case Pet.PetDetailedStatus.STRESSED:
                        Console.Write("stress...");
                        break;

                    case Pet.PetDetailedStatus.DIRTY:
                        Console.Write("dirtiness...");
                        break;

                    default:
                        throw new SystemException();
                }
            }


            Console.WriteLine(myPet.GetName() + " have died :c");
            

            // Show fish image
            DispAsciiArt(myPet.GetDeadImage(), 60);

            ConsoleAppUtils.Pause();
            Console.Clear();

            // Ask whether player want to play again
            return PlayerWantToPlayAgain();

        }

        /// <summary>
        /// Take care of the pet
        /// </summary>
        /// <param name="myPet"></param>
        static void TakeCareOf(Pet myPet)
        {
            // Show menu for player
            for (int i = 0; i < MENU.Length; i++)
            {
                Console.WriteLine((i + 1) + "." + MENU[i] + " " + myPet.GetName());
            }

            // Print fish image under the menu
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            DispAsciiArt(myPet.GetImage());
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            // Ask player which method to choose
            Console.Write("Choose a number to select what to do:");
            int playerSelectedNum = ConsoleAppUtils.GetPlayerInputInt(1, MENU.Length);

            Console.Clear();

            // Evaluate player choice
            switch (playerSelectedNum)
            {
                case 1:
                    // Feed
                    myPet.Eat();

                    // Show food image
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("                    .      O                            ");
                    Console.WriteLine("                     o .                                ");
                    Console.WriteLine("                    .   O                               ");
                    Console.WriteLine("                           .                            ");
                    Console.WriteLine("                  .                                     ");
                    Console.WriteLine("                     O       o                          ");
                    Console.WriteLine("                         .                              ");
                    Console.WriteLine("                     .                                  ");

                    // Show the fish image
                    DispAsciiArt(myPet.GetFineImage());

                    break;

                case 2:

                    // See
                    myPet.SeenByHuman();

                    // Show the fish animation
                    for (int i = 0; i < 6; i++)
                    {
                        // Show fish image alternately and move the position
                        if (i == 0 || i % 2 == 1)
                        {
                            DispAsciiArt(myPet.GetNomalImage(), 90 - i * 10);
                        }
                        else
                        {
                            DispAsciiArt(myPet.GetTiredImage(), 90 - i * 10);
                        }

                        // Wait for 0.5 second to show the images for player
                        System.Threading.Thread.Sleep(500);

                        // Remove the image
                        Console.Clear();
                    }

                    break;

                case 3:
                    // Change the water
                    myPet.Cleaned();

                    // Show the image of the pet
                    DispAsciiArt(myPet.GetFineImage(), 60);

                    break;

                case 4:
                    // Leave
                    myPet.Relax();

                    // Show fish image alternately and move the position
                    for (int i = 0; i < 6; i++)
                    {
                        // Show fish image alternately and move the position
                        if (i == 0 || i % 2 == 1)
                        {
                            DispAsciiArt(myPet.GetNomalImage(), 90 - i * 10);
                        }
                        else
                        {
                            DispAsciiArt(myPet.GetFineImage(), 90 - i * 10);
                        }

                        // Wait for 0.5 second to show the images for player
                        System.Threading.Thread.Sleep(500);

                        // Remove the image
                        Console.Clear();
                    }

                    break;

                case 5:
                    // Give a new plant
                    myPet.DeepBreathe();

                    // Show the image of the pet
                    DispAsciiArt(myPet.GetFineImage(), random.Next(10, 90));

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");

                    string[] plant = {
                    "___  ",
                    "(  ( ",
                    " )  )",
                    "(  ( ",
                    " )  )",
                    " (  (",
                    "  )  ",
                    " (  ("
                    };

                    DispAsciiArt(plant, random.Next(10, 90));

                    break;

                default:

                    // Impossible
                    throw new SystemException();
            }

            ConsoleAppUtils.Pause();
        }

        /// <summary>
        /// Write the report about the pet
        /// </summary>
        /// <param name="myPet">the instans of the pet</param>
        static void WriteReportAbout(Pet myPet)
        {
            // Get pets detailed status
            List<Pet.PetDetailedStatus> petState = myPet.GetDetailedStatus();

            // Display the image of the pet
            DispAsciiArt(myPet.GetImage(), 20);

            Console.WriteLine("");
            Console.WriteLine("");

            // Write every status of the pet
            foreach (Pet.PetDetailedStatus status in petState)
            {
                // Evaluate the status to show the report about the pet
                switch (status)
                {
                    case Pet.PetDetailedStatus.FINE:
                        Console.WriteLine("{0} is fine :D", myPet.GetName());
                        break;

                    case Pet.PetDetailedStatus.hungry:
                        Console.WriteLine("{0} is hungry...", myPet.GetName());
                        break;

                    case Pet.PetDetailedStatus.STRESSED:
                        Console.WriteLine("{0} is feeling stress...", myPet.GetName());
                        break;

                    case Pet.PetDetailedStatus.DIRTY:
                        Console.WriteLine("{0} is dirty...", myPet.GetName());
                        break;

                    default:
                        throw new SystemException();
                }
            }
        }

        /// <summary>
        /// Display fish image for default place, middle
        /// </summary>
        /// <param name="fishImage">an array that is fulled by fish ascii art</param>
        static void DispAsciiArt(string[] fishImage)
        {
            DispAsciiArt(fishImage, 50);
        }

        /// <summary>
        /// Display fish image for any place 
        /// </summary>
        /// <param name="fishImage">an array that is fulled by fish ascii art</param>
        /// <param name="padLeft">an integer to pad left of the art</param>
        static void DispAsciiArt(string[] fishImage, int padLeft)
        {
            // Show some empty lines
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("");
            }

            // Show the fish image to a specific place
            for (int i = 0; i < fishImage.Length; i++)
            {
                Console.WriteLine(fishImage[i].PadLeft(padLeft));
            }
        }

        /// <summary>
        /// Set the size and title to the console
        /// </summary>
        static void InitializeConsole()
        {
            // Set the size of window
            Console.SetWindowSize(100, 38);

            // Set the title of console
            Console.Title = "Console Aquarium";
        }

        /// <summary>
        /// Show the title display
        /// </summary>
        static void ShowTitle()
        {
            
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                      ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗        ");
            Console.WriteLine("                     ██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝       ");
            Console.WriteLine("                     ██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗           ");
            Console.WriteLine("                     ██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝           ");
            Console.WriteLine("                     ╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗      ");
            Console.WriteLine("                      ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝       ");
            Console.WriteLine("                                                                                              ");
            Console.WriteLine("                    █████╗  ██████╗ ██╗   ██╗ █████╗ ██████╗ ██╗██╗   ██╗███╗   ███╗   ");
            Console.WriteLine("                   ██╔══██╗██╔═══██╗██║   ██║██╔══██╗██╔══██╗██║██║   ██║████╗ ████║ ");
            Console.WriteLine("                   ███████║██║   ██║██║   ██║███████║██████╔╝██║██║   ██║██╔████╔██║  ");
            Console.WriteLine("                   ██╔══██║██║▄▄ ██║██║   ██║██╔══██║██╔══██╗██║██║   ██║██║╚██╔╝██║ ");
            Console.WriteLine("                   ██║  ██║╚██████╔╝╚██████╔╝██║  ██║██║  ██║██║╚██████╔╝██║ ╚═╝ ██║  ");
            Console.WriteLine("                   ╚═╝  ╚═╝ ╚══▀▀═╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝     ╚═╝   ");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            ConsoleAppUtils.WriteMessageCenter("PRESS ENTER TO PLAY");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleAppUtils.WriteMessageCenter("you can always stop playing with Ctrl + C");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.Write("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█");
            Console.ResetColor();
            ConsoleAppUtils.Pause();
            Console.Clear();

        }

        /// <summary>
        /// Show the animation and ask player to give the pet a name
        /// </summary>
        /// <returns>fish name</returns>
        static string HatchFish()
        {
            Console.WriteLine();
            Console.WriteLine("    An egg is hatching...           ");
            ConsoleAppUtils.Pause();

            Console.WriteLine("                                                          o                ");
            Console.WriteLine("                                                           o               ");
            Console.WriteLine("                                                          o                ");
            Console.WriteLine();                                                                         
            ConsoleAppUtils.Pause();                                                                     
            Console.WriteLine("                                                           O               ");
            Console.WriteLine("                                                           o               ");
            Console.WriteLine("                                                          ○                ");
            Console.WriteLine("                                                          O                ");
            ConsoleAppUtils.Pause();
            Console.WriteLine("                                        |\\   \\\\__     o                 ");
            Console.WriteLine("                                        | \\_/    o \\    o                ");
            Console.WriteLine("                                        > _   (( <_  oo                    ");
            Console.WriteLine("                                        | / \\__+___/                      ");
            Console.WriteLine("                                        |/     |/                          ");
            Console.WriteLine();                                                       
            ConsoleAppUtils.Pause();                                                   
            Console.WriteLine();                                                       
            Console.WriteLine();                                                       
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();                                               
            Console.WriteLine("                                                                        **             ");
            Console.WriteLine("                                                                       *   *           ");
            Console.WriteLine("                                                                       *   *            ");
            Console.WriteLine("                                                                         **          ");
            ConsoleAppUtils.Pause();
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                             ****                                      ");
            Console.WriteLine("                                                           *     *                                  ");
            Console.WriteLine("                                                          *       *                                 ");
            Console.WriteLine("                                                          *       *                                ");
            Console.WriteLine("                                                           *     *                                  ");
            Console.WriteLine("                                                             ***                                     ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            ConsoleAppUtils.Pause();                                               
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                          ****                                      ");
            Console.WriteLine("                                                          *     *                                  ");
            Console.WriteLine("                                                         *       *                                 ");
            Console.WriteLine("                                                         *        *                                 ");
            Console.WriteLine("                                                           *     *                                  ");
            Console.WriteLine("                                                             ****                                     ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            Console.WriteLine("                                                                                                  ");
            ConsoleAppUtils.Pause();                                            
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                              ****                                      ");
            Console.WriteLine("                                                            *     *                                  ");
            Console.WriteLine("                                                           *       *                                 ");
            Console.WriteLine("                                                           *       *                                ");
            Console.WriteLine("                                                            *     *                                  ");
            Console.WriteLine("                                                              ***                                     ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            ConsoleAppUtils.Pause();                                                
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                              ****                                      ");
            Console.WriteLine("                                                            *     *                                  ");
            Console.WriteLine("                                                           */\\/\\//\\*                                 ");
            Console.WriteLine("                                                           *       *                                ");
            Console.WriteLine("                                                            *     *                                  ");
            Console.WriteLine("                                                              ***                                     ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            Console.WriteLine("                                                                                                   ");
            ConsoleAppUtils.Pause();                                            
            Console.Clear();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                                                       /\\                    ");
            Console.WriteLine("                                                                    _ /./                    ");
            Console.WriteLine("                                                                 ,-'    `-:.,-' /            ");
            Console.WriteLine("                                                                 > O )<)    _(                ");
            Console.WriteLine("                                                                 `-._  _.:' `-.\\             ");
            Console.WriteLine("                                                                     `` \\;                   ");
            Console.WriteLine("       You've got a new fish!!");
            Console.Write("       Give him a name:");
            string fishname = ConsoleAppUtils.GetPlayerInput();
            Console.Clear();
            return fishname;
        }

        /// <summary>
        /// Ask player whether play the game again or not
        /// </summary>
        /// <returns>true: continue the game, false: finish the game</returns>
        static bool PlayerWantToPlayAgain()
        {
            // Ask player whether want to play again
            Console.WriteLine("Would you like to play again?(y/n)");

            // Get player input
            string playerAnswer = ConsoleAppUtils.GetPlayerInput();

            // Check if player want to play again or not. Unless player say y or yes, finish the game
            if (string.Equals(playerAnswer, "Yes", StringComparison.OrdinalIgnoreCase)
                || string.Equals(playerAnswer, "Y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

    }
}
