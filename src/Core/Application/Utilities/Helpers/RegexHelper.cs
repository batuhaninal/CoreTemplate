using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Utilities.Helpers
{
    public static class RegexHelper
    {
        public static bool CheckWhiteSpaceDuplicate(string args) => !Regex.IsMatch(args, @"\s{3,}");
        public static bool CheckWhiteSpaceExist(string args) => !Regex.IsMatch(args, @"(\s)");
        public static bool CheckOnlyNumbers(string args) => Regex.IsMatch(args, @"^\d+$");
    }
}
