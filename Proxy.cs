using System;
using System.Collections.Generic;
using System.Linq;
using ConfigMananger = Indd.Service.Config.Manager;
using Indd.Service.IndesignServerWrapper;
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

            ApplicationMananger manager = new ApplicationMananger();

            InDesignServer.Application app = manager.createInstance();
        }  
    }
}
