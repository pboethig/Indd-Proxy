using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InDesignServer;
using ConfigMananger = Indd.Service.Config.Manager;

using Indd.Service.Commandline.Validator.Options;

namespace Indd
{
    class Proxy
    {
        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<GenerateProxy>(args);

            if (result.Errors.Any()) Console.WriteLine("Press any key to continue"); Console.ReadKey();

            string templatePath = result.Value.InputFile;

            string templateStoragePath = ConfigMananger.getStoragePath("templates");
        }  
    }
}
