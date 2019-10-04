using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
            //InitializeRoomDescriptions("Rooms.txt");
            const string defaultRoomsFileName = "Rooms.txt";
            string roomsFileName = args.Length > 0 ? args[0] : defaultRoomsFileName;

            Location = IndexOf(Rooms, "West of House");
            Assert.IsTrue(Location != (-1, 1));

            InitializeRoomDescriptions(roomsFileName);


            Room previousRoom = null;

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);

                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }


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
                            Console.WriteLine("Fuck off.");
                        }
                        break;

                    default:
                        Console.WriteLine("Uknown Command 0jnjnjnkjbhgvhgv0.");
                        break;
                }
            }
        }
        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN; 

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

        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            Dictionary<string, Room> roomMap = new Dictionary<string, Room>();


            foreach (Room room in Rooms)
            {
                roomMap.Add(room.Name, room);

            }

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                const string fieldDelimiter = "##";
                const int expectedFieldCount = 2;

                string[] fields = line.Split(fieldDelimiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name]; 
                string description = fields[(int)Fields.Description];

                roomMap[name].Description = description;
            }




        }

        private enum Fields
        {
            Name = 0,
            Description = 1
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0,
            UseLinq
        }
    }

}




