using System;
using System.IO;
using ConfigManager = Indd.Service.Config.Manager;
using Indd.Service.Log;
using Indd.Service.Commands;
using CommandResponse = Indd.Service.Commands.Response;

namespace Indd.Service.Filesystem
{
    class Watcher
    {
        /// <summary>
        /// InQueue
        /// </summary>
        public static string inQueue;

        /// <summary>
        /// OutQueue
        /// </summary>
        public static string outQueue;

        /// <summary>
        /// ProcessQueue
        /// </summary>
        public static string processQueue;

        /// <summary>
        /// ErrorQueue
        /// </summary>
        public static string errorQueue;

        /// <summary>
        /// Watcher
        /// </summary>
        /// <param name="path"></param>
        public Watcher(string path, bool attachListener = true)
        {
            this.initQueues();

            if (attachListener)
            { 
                FileSystemWatcher watcher = new FileSystemWatcher();

                watcher.Path = path;
                watcher.EnableRaisingEvents = true;
                watcher.Created += new FileSystemEventHandler(watcher_Created);
                watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
                watcher.Changed += new FileSystemEventHandler(watcher_Changed);
                watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
                Console.WriteLine("FileSystemWatcher ready and listening to changes in :" + path);
                Console.WriteLine("Press any key to stop listener");
                Console.ReadKey();
            }
        }
        
        /// <summary>
        /// Inits queues
        /// </summary>
        public void initQueues()
        {
            processQueue = ConfigManager.getJobQueuePath("process");

            outQueue = ConfigManager.getJobQueuePath("out");

            errorQueue = ConfigManager.getJobQueuePath("error");
        }

        static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            //Console.WriteLine(e.OldName + " is now: " + e.Name);
        }
        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine(e.Name + " has changed");
        }
        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine(e.Name + " file has been deleted");
        }

        /// <summary>
        /// Watches for new incomming files, moves it to process and triggers ticket execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath + " file has been created.");

            string targetPath = processQueue + "\\" + e.Name;

            try
            {
                processTicket(targetPath, e.Name, e.FullPath);
            }
            catch (System.Exception exception)
            {
                Syslog.log("Watcher-Created Event fails: " + exception.Message + " targetPath:" + targetPath);                
            }
        }

        /// <summary>
        /// Process ticket
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="e"></param>
        public static void processTicket(string targetPath, string fileName, string filePath)
        {

            if (System.IO.File.Exists(targetPath))
            {
                System.IO.File.Delete(targetPath);
            }
        
            /// Move infile to process
            System.IO.File.Move(filePath, targetPath);
            
            //prepare outFIle
            string outPath = outQueue + "\\" + fileName;
            
            Factory commandFactory = new Factory();

            try
            {
                dynamic ticket = commandFactory.convertJsonTicket(targetPath);

                CommandResponse response= commandFactory.processTicket(ticket);

                if (response.errors.Count > 0)
                {
                    string errorPath = errorQueue + "\\" + fileName;

                    commandFactory.writeErrorFile(commandFactory, response, targetPath, errorPath);
                }
                else
                {
                    commandFactory.writeOutFile(commandFactory, response, targetPath, outPath);
                }
            }
            catch (System.Exception ex)
            {
                System.IO.File.Move(targetPath, errorQueue + "\\" + fileName);

                string message = "Watcher-Created Ticket error: " + commandFactory.jsonTicket + " Inner Exception: " + ex.Message;

                Syslog.log(message);

                throw new System.Exception(message);
            }
        }
    }
}