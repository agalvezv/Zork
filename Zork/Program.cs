using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {

        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescriptions();

            Location = IndexOf(Rooms, "West of House");
            Assert.IsTrue(Location != (-1, 1));

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Uknown Command.");
                        break;
                }
            }
        }

        //Checking if true, then selects proper op
        //=> can use if only one line within the brackets, the return is imp

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN; //THREE OPERATORS: If the first segment is true, it returns the statement before the : otherwise it returns the statement after the :.

        private static readonly Commands[] Directions =
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
        };


        private static readonly Room[,] Rooms =
        {
            {   new Room("Rocky Trail")      , new Room("South of House"),   new Room  ("Canyon View") },
            {   new Room("Forest")           , new Room("West of House"),    new Room  ("Behind House") },
            {   new Room("Dense Woods")      , new Room("North of House"),   new Room  ("Clearing") }
        };

        private static (int Row, int Column) Location;
        private static void SpawnPlayer(string roomName)
        {

            Location = IndexOf(Rooms, roomName);
            if ((Location.Row, Location.Column) == (-1, -1))
            {
                throw new Exception($"Did not find room: {roomName}");
            }

        }

        private static (int Row, int Column) IndexOf(Room[,] values, string valueToFind)
        {
            for (int row = 0; row < Rooms.GetLength(0); row++)
            {
                for (int column = 0; column < Rooms.GetLength(1); column++)
                {
                    if (valueToFind == values[row, column].Name)
                    {

                        return (row, column);
                    }

                }

            }

            return (-1, -1);
        }
        private static bool Move(Commands command)
        {


            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:

                    Location.Row++;
                    break;
                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;
                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;
                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        private static void InitializeRoomDescriptions()
        {
            Rooms[0, 0].Description = "You are on a rock-strewn trail. ";
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred. ";
            Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall. ";

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you. ";
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar ";

            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. to the east, there appears to be sunlight. ";
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
            Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south. ";

        }

    }

}


//private static int LocationColumn = 1;
// private static int LocationRow = 1;
//private static int PlayerPosition = 1;
////C# How To Program, C# for programmers






