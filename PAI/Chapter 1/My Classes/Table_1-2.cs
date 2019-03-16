using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Table_1_2
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Table 1-2: Truth Table for the Conjunction Logical Connective (p ^ q) [p AND q]");
            Console.WriteLine(string.Empty);

            Row.Write("p", "q", "p ^ q");

            Evaluate(new Variable(true), new Variable(false));
            Evaluate(new Variable(false), new Variable(true));
            Evaluate(new Variable(false), new Variable(false));
            Evaluate(new Variable(true), new Variable(true));
        }

        private static void Evaluate(Variable p, Variable q)
        {
            Row.Write(p.Value.ToString(), q.Value.ToString(), new And(p, q).Evaluate().ToString());
        }
    }
}
