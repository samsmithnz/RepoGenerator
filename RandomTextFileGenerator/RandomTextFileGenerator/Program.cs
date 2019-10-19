using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace RandomTextFileGenerator
{
    //Simulates text files for testing
    class Program
    {
        private static char[] legalChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        static void Main(string[] args)
        {
            //getting the data folder
            string folder = @"C:\Users\samsmit\source\repos\ReallyBigRepo\RandomTextFileGenerator\Data";
            if (Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }

            long currentTotalSize = 0;
            long totalTargetSize = 6000000000; //loop until we generate this many bytes
            do
            {
                int rows = GenerateRandomNumber(1, 100); //set the number of rows to add to our file
                int size = GenerateRandomNumber(1, 1000); //set the number of characters to add to each row
                int folderNumber = GenerateRandomNumber(1, 100);

                //creating a unique file name
                string fileName = GenerateFileName(folder + @"\" + folderNumber.ToString());
                int legalCharsLength = legalChars.Length - 1;

                using (FileStream fs = File.OpenWrite(fileName))
                {
                    using (StreamWriter w = new StreamWriter(fs))
                    {
                        for (int i = 1; i < rows; i++)
                        {
                            string line = $"{i}. {GetRandomText(size, legalCharsLength)}";
                            currentTotalSize += line.Length;
                            w.WriteLine(line);
                        }
                    }
                }
                Console.WriteLine("Current total bytes: " + currentTotalSize + "/" + totalTargetSize);
            } while (currentTotalSize < totalTargetSize);
            Console.WriteLine("Finished!");
        }

        public static string GetRandomText(int size, int legalCharsLength)
        {
            StringBuilder builder = new StringBuilder();
            
            //generate a number of characters from an array
            for (int i = 0; i < size; i++)
            {
                char character = legalChars[GenerateRandomNumber(0, legalCharsLength)];
                builder.Append(character);
            }

            return builder.ToString();
        }
        private static int GenerateRandomNumber(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        //generate a random file name. Why did this part get so complicated?
        private static string GenerateFileName(string folderPath)
        {
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }

            // selected characters
            string chars = "2346789ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz";
            // create random generator
            Random rnd = new Random();
            string name;
            do
            {
                // create name
                name = string.Empty;
                while (name.Length < 5)
                {
                    name += chars.Substring(rnd.Next(chars.Length), 1);
                }
                // add extension
                name += ".txt";
                // check against files in the folder
            } while (File.Exists(Path.Combine(folderPath, name)));

            return Path.Combine(folderPath, name);
        }

    }
}
