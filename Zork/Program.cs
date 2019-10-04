using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {

        static void Main(string[] args)
        {
            const string defaultGameFilename = "Room.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

            Game game = Game.Load(gameFilename);
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thanks for playing! Go away now.");
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }

    }

}



