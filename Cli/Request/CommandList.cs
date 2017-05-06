using System;
using System.Linq;
using Indd.Service.Commands;
using CommandLine;
using Indd.Service.Log;

namespace Indd.Cli.Request
{
    class CommandList
    {
        [Option('f', "filepath", Required = true,
     HelpText = "Input filepath to be processed.")]
        public string InputFile { get; set; }

        [Option('s', "silent", Required = false, DefaultValue = false,
     HelpText = " suppresses userinput")]
        public bool Silent { get; set; }


        // omitting long name, default --verbose
        [Option(DefaultValue = true,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Value(0)]
        public int Offset { get; set; }

        /// <summary>
        /// Validates Request
        /// </summary>
        /// <param name="args"></param>
        /// <returns>dynamic</returns>
        public static dynamic validate(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<CommandList>(args);
            if (result.Errors.Any()) Console.WriteLine("Press any key to continue");

            string root = Indd.Service.Config.Manager.getRootDirectory().Replace("bin\\Debug", "");

            string jsonCommandFilePath = result.Value.InputFile;

            jsonCommandFilePath = jsonCommandFilePath.Replace("$root", root);
            
            if (!System.IO.File.Exists(jsonCommandFilePath))
            {
                string message = "input file not found: " + jsonCommandFilePath; 

                Syslog.log(message); 

                if (result.Value.Silent == false)
                {
                    Console.WriteLine(message);
                    Console.ReadKey();
                }

                Environment.Exit(0);
            }
            
            result.Value.InputFile = jsonCommandFilePath;

            return result;
        }
    }
}
