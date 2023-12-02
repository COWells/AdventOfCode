using System;
using System.Collections.Generic;

namespace CommonUtils
{
    public static class StringFormatter
    {
        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string[] SplitAndRemovingTrailingWhitespace(string s, char splitCharacter)
        {
            List<string> stringList = new List<string>() { };
            var splitString = s.Split(splitCharacter);
            foreach (var split in splitString)
            {
                stringList.Add(split.Trim());
            }
            return stringList.ToArray();
        }
    }
}