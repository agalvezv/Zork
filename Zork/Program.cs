using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Zork!");

            string inputString = Console.ReadLine();
            Commands command = ToCommand(inputString.Trim());
            Console.WriteLine(command);
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}

