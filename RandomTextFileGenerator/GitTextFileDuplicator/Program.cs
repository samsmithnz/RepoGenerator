using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GitTextFileDuplicator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string sourceFolder = @"C:\Users\samsmit\source\repos\Big10GBRepo";
            string destinationFolder = @"C:\Users\samsmit\source\repos\Big20GBRepo";

            //Repeat for all files
            for (int i = 11; i <= 53; i++)
            {
                //Copy file to folder 1
                if (Directory.Exists(destinationFolder + @"\01\" + i) == false)
                {
                    Directory.CreateDirectory(destinationFolder + @"\01\" + i);
                }
                foreach (string file in Directory.GetFiles(sourceFolder + @"\" + i))
                {
                    File.Copy(file, destinationFolder + @"\01\" + i + @"\" + Path.GetFileName(file), true);
                }
                Console.WriteLine("Updating 01 folder for " + i.ToString());

                //Copy file to folder 2
                if (Directory.Exists(destinationFolder + @"\02\" + i) == false)
                {
                    Directory.CreateDirectory(destinationFolder + @"\02\" + i);
                }
                foreach (string file in Directory.GetFiles(sourceFolder + @"\" + i))
                {
                    File.Copy(file, destinationFolder + @"\02\" + i + @"\" + Path.GetFileName(file), true);
                }
                Console.WriteLine("Updating 02 folder for " + i.ToString());

                ////50% chance of copying file to folder 3
                //if (i % 2 == 0)
                //{
                //    if (Directory.Exists(destinationFolder + @"\03\" + i) == false)
                //    {
                //        Directory.CreateDirectory(destinationFolder + @"\03\" + i);
                //    }
                //    foreach (string file in Directory.GetFiles(sourceFolder + @"\" + i))
                //    {
                //        File.Copy(file, destinationFolder + @"\03\" + i + @"\" + Path.GetFileName(file), true);
                //    }
                //    Console.WriteLine("Updating 03 folder for " + i.ToString());
                //}

                //Git Add
                RunCmd("/C git add .");
                Console.WriteLine("git add for " + i.ToString());

                //Git Commit
                RunCmd("/C git commit -m\"Adding files from folder " + i + "\"");
                Console.WriteLine("git commit -m\"Adding files from folder " + i + " for " + i.ToString());

                //Git Push
                RunCmd("/C git push");
                Console.WriteLine("git push for " + i.ToString());
            }
        }

        private static bool RunCmd(string cmd)
        {
            Directory.SetCurrentDirectory(@"C:\Users\samsmit\source\repos\Big20GBRepo");
            Process cmdProcess = Process.Start("CMD.exe", cmd);
            cmdProcess.WaitForExit();

            return true;
        }

    }
}
