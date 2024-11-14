using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.CommonUtils
{
    public static partial class CommonRegex
    {
        public static Regex OneOrMoreDigits => GenerateOneOrMoreDigitsRegex();
        [GeneratedRegex(@"\d+")]
        private static partial Regex GenerateOneOrMoreDigitsRegex();


        public static Regex NotDigits => GenerateNotDigitsRegex();
        [GeneratedRegex(@"[^0-9]")]
        private static partial Regex GenerateNotDigitsRegex();


        public static Regex IgnoreCharacter(char character)
        {
            string regexString = "[^" + character + "]";
            return new Regex(regexString);
        }

        public static Regex ExactlyOneCharacter(char character)
        {
            string regexString = @"\" + character;
            return new Regex(regexString);
        }
    }
}
