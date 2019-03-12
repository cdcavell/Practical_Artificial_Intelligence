using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Row
    {
        public static void Write(string p, string q, string pq)
        {
            string row = " ";
            row += "|";
            row += p.CenterString(7);
            row += "|";
            row += q.CenterString(7);
            row += "|";
            row += pq.CenterString(7);
            row += "|";

            string rowBreak = " ";
            while (rowBreak.Length < row.Length)
                rowBreak += "-";

            Console.WriteLine(row);
            Console.WriteLine(rowBreak);
        }
    }
}
