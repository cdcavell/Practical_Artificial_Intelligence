using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Table_1_3
    {       
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Table 1-3: Truth Table for the Disjunction Logical Connective (p V q) [OR]");
            Console.WriteLine(string.Empty);

            Row.Write("p", "q", "p V q");

            Evaluate(new Variable(true), new Variable(false));
            Evaluate(new Variable(false), new Variable(true));
            Evaluate(new Variable(false), new Variable(false));
            Evaluate(new Variable(true), new Variable(true));
        }

        private static void Evaluate(Variable p, Variable q)
        {
            Row.Write(p.Value.ToString(), q.Value.ToString(), new Or(p, q).Evaluate().ToString());
        }
    }
}
