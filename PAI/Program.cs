using System;

namespace PAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            Console.Clear();

            Console.WriteLine(" Main Menu");
            Console.WriteLine(" --------------------");
            Console.WriteLine(" [1] Chapter 1 - Logical Connectives");
            Console.WriteLine(" [2] Chapter 2");
            Console.WriteLine(" [3] Chapter 3");
            Console.WriteLine(" [4] Chapter 4");
            Console.WriteLine(" --------------------\n");
            Console.WriteLine(" Please select an option or 0 to exit\n");

            string choice = Console.ReadLine();
            int number;
            bool result = Int32.TryParse(choice, out number);
            if (result)
            {
                switch (number)
                {
                    default:
                        MainMenu();
                        break;

                    case 0:
                        break;

                    case 1:
                        Chapter1.Display();
                        break;
                }
            }
            else
            {
                MainMenu();
            }
        }

        public static void PauseReturnMain()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Press <Enter> key to return to main menu.");
            Console.ReadLine();

            MainMenu();
        }
    }
}
