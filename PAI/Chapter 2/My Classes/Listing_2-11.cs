using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    class Listing_2_11
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Listing 2-11: Creating Formula (p ∨ q ∨ ¬r) ∨ (p ∨ q ∨ r) ∨ (p ∨ ¬q) ∨ ¬p and Finding Out If It's Satisfiable Using the DPLL Algorithm");
            Console.WriteLine(string.Empty);

            var p = new Variable(true) { Name = "p" };
            var q = new Variable(true) { Name = "q" };
            var r = new Variable(true) { Name = "r" };

            var f1 = new Or(p, new Or(q, new Not(r)));
            var f2 = new Or(p, new Or(q, r));
            var f3 = new Or(p, new Not(q));
            var formula = new And(f1, new And(f2, new And(f3, new Not(p))));

            var nnf = formula.ToNnf();
            Console.WriteLine("NNF: " + nnf);

            nnf = nnf.ToCnf();
            var cnf = new Cnf(nnf as And);
            cnf.SimplifyCnf();

            Console.WriteLine("CNF: " + cnf);
            Console.WriteLine("SAT: " + cnf.Dpll());
        }
    }
}
