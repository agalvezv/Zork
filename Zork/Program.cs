using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[PlayerPosition]);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string response;
                switch (command)
                {
                    case Commands.QUIT:
                        response = "Thank you for playing!";
                        break;
                    case Commands.LOOK:
                        response = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool MoveSuccessfully = Move(command);
                        if (MoveSuccessfully)
                        {
                            response = $"You moved {command}.";
                        }
                        else
                        {
                            response = "There is only Void.";
                        }
                        break;

                    default:
                        response = "Unknown command.";
                        break;

                }

                Console.WriteLine(response);
            }
        }

        private static Commands ToCommand(string commandString) => (Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN); //OP1 ? OP2 : OP 3
        //Checking if true, then selects proper op
        //=> can use if only one line within the brackets, the return is imp
        private static bool Move(Commands command)
        {
            if (Directions.Contains(command) == false)
            {
                throw new ArgumentException();
            }

            bool MoveSuccessfully;

            switch (command)
            {
                case Commands.EAST when PlayerPosition < Rooms.Length - 1:

                    PlayerPosition++;
                    MoveSuccessfully = true;
                    break;
                case Commands.WEST when PlayerPosition > 0:

                    PlayerPosition--;
                    MoveSuccessfully = true;
                    break;

                default:
                    MoveSuccessfully = false;
                    break;

            }

            return MoveSuccessfully;
        }



        }
        private static readonly HashSet<Commands> Directions = new HashSet<Commands>()
        {
        Commands.NORTH,
        Commands.SOUTH,
        Commands.EAST,
        Commands.WEST
        };

        private static string[] Rooms = { "Forest", "West of House", "Behind House", "Clearning", "Canyon View" };
        private static int PlayerPosition = 1;
    }
}







