using System;
using System.IO;

namespace Indd.Service.Filesystem
{
    class Watcher
    {

        public Watcher(string path)
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

        static void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine(e.OldName + " is now: " + e.Name);
        }
        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + " has changed");
        }
        static void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name + " file has been deleted");
        }
        static void watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath + " file has been created.");
        }
    }
}
