using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Indd.Helper.IO
{
    class Directory
    {
        public static void Copy(string sourceFolderPath, string targetPath)
        {
            System.IO.DirectoryInfo source = new System.IO.DirectoryInfo(sourceFolderPath);

            System.IO.DirectoryInfo target = new System.IO.DirectoryInfo(targetPath);

            // Check if the target directory exists, if not, create it.
            if (System.IO.Directory.Exists(target.FullName) == false)
            {
                System.IO.Directory.CreateDirectory(target.FullName);
            }
            // Copy each file into it’s new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }
        }
    }
}
