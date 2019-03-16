using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Table_1_5
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Table 1-5: Truth Table for the Equivalence Logical Connective (p <=> q) [IF AND ONLY IF ((NOT p OR q) AND (NOT q OR p))]");
            Console.WriteLine(string.Empty);

            Row.Write("p", "q", "p => q");

            Evaluate(new Variable(true), new Variable(false));
            Evaluate(new Variable(false), new Variable(true));
            Evaluate(new Variable(false), new Variable(false));
            Evaluate(new Variable(true), new Variable(true));
        }

        private static void Evaluate(Variable p, Variable q)
        {
            Row.Write(p.Value.ToString(), q.Value.ToString(), new And((new Or(new Not(p), q)), (new Or(new Not(q), p))).Evaluate().ToString());
        }
    }
}
