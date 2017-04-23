using System.Collections.Generic;
using CommandLine;

namespace Indd.Service.Commandline.Validator.Options {


    class GenerateProxy
    {
        [Option('f', "filepath", Required = true,
          HelpText = "Input filepath to be processed.")]
        public string InputFile { get; set; }

        // omitting long name, default --verbose
        [Option(DefaultValue = true,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; } 

        [Value(0)]
        public int Offset { get; set; }
    }
}