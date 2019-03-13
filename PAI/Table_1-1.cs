using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Table_1_1
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Table 1-1: Truth Table for Negation Logical Connective [NOT p]");
            Console.WriteLine(string.Empty);

            Row.Write("p", "NOT p", "");

            Evaluate(new Variable(true));
            Evaluate(new Variable(false));
        }

        private static void Evaluate(Variable p)
        {
            Row.Write(p.Value.ToString(), new Not(p).Evaluate().ToString(), string.Empty);
        }
    }
}
