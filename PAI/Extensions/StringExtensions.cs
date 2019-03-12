using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class StringExtensions
    {
        public static string CenterString(this string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2) + stringToCenter.Length).PadRight(totalLength);
        }
    }
}
