using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CommonUtils
{
    public static class FileReader
    {
        public static List<string> ReadAllLines(string Day) => File.ReadAllLines(GetFilePath(Day)).ToList();

        private static string GetFilePath(string Day)
            => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + $"/Days/{Day}/Input.txt";
    }
}