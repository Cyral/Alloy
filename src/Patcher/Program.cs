﻿using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Alloy.Injector;

namespace Alloy.Patcher
{
    internal class Program
    {
        private static AssemblyInjector injector;
        private static void Main(string[] args)
        {
            Console.Title = "Alloy Patcher";

            var config = JObject.Parse(File.ReadAllText("config.json"));
            var source = Environment.ExpandEnvironmentVariables(config["SourceAssembly"].ToString());
            var target = Environment.ExpandEnvironmentVariables(config["TargetAssembly"].ToString());

            if (File.Exists(source))
            {
                injector = new AssemblyInjector(source, target);
                Console.WriteLine($"Patching assembly {source}.");
                injector.Inject();
                Console.WriteLine($"Exporting assembly to {target}.");
                injector.Export();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Assembly {source} not found.");
            }

            Console.ReadLine();
        }
    }
}