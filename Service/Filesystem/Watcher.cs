﻿using System;
using System.IO;
using ConfigManager = Indd.Service.Config.Manager;
using Indd.Service.Log;
using Indd.Service.Commands;
using CommandResponse = Indd.Service.Commands.Response;

namespace Indd.Service.Filesystem
{
    public class Watcher
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
        /// watcher
        /// </summary>
        public FileSystemWatcher watcher;
        
        /// <summary>
        /// Watcher
        /// </summary>
        /// <param name="path"></param>
        public Watcher(string path, bool attachListener = true)
        {
            this.initQueues();

            if (attachListener)
            { 
                this.watcher = new FileSystemWatcher();

                watcher.NotifyFilter = NotifyFilters.Attributes |
                NotifyFilters.CreationTime |
                NotifyFilters.FileName |
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.Size |
                NotifyFilters.Security;

                watcher.InternalBufferSize = 2621440;
                watcher.Path = path;
                watcher.Created += new FileSystemEventHandler(watcher_Created);
                watcher.Changed += new FileSystemEventHandler(watcher_Changed);
                watcher.EnableRaisingEvents = true;
                watcher.Error += new ErrorEventHandler(WatcherError);
            }
        }

        // The error event handler
        private static void WatcherError(object source, ErrorEventArgs e)
        {
            System.Exception watchException = e.GetException();

            Indd.Service.Log.Syslog.log("Watcher Error"+watchException.Message);
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
            //observ(e);
        }
        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            observ(e);
        }
        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
           // observ(e);
        }

        /// <summary>
        /// Watches for new incomming files, moves it to process and triggers ticket execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            observ(e);
        }

        static void observ(FileSystemEventArgs e) {

        string targetPath = processQueue + "\\" + e.Name;

        try
        {
            processTicket(targetPath, e.Name, e.FullPath);
        }
        catch(System.Exception ex)
        {
                Indd.Service.Log.Syslog.log("Watcher Error" + ex.Message);
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