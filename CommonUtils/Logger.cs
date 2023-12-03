using System;
using System.Collections.Generic;
using System.IO;

namespace CommonUtils
{
    public static class Logger
    {
        public static void OutputToFile(string output, string Day)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(GetFilePath(Day), "Output.txt"), append: true))
            {
                outputFile.WriteLine(output);
            }
        }

        private static string GetFilePath(string Day)
            => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + $"/Days/{Day}";
    }
}