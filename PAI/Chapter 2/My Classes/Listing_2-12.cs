using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    class Listing_2_12
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Listing 2-12: Creating Formula (p ∨ q ∨ r) ∧ (p ∨ q ∨ ¬r) ∧ (p ∨ ¬q ∨ r) ∧ (p ∨ ¬q ∨ ¬r) ∧ (¬p ∨ q ∨ r) ∧ (¬p ∨ q ∨ ¬r) ∧ (¬p ∨ ¬q ∨ r) and Finding Out If It's Satisfiable Using the DPLL Algorithm");
            Console.WriteLine(string.Empty);

            var p = new Variable(true) { Name = "p" };
            var q = new Variable(true) { Name = "q" };
            var r = new Variable(true) { Name = "r" };

            var f1 = new Or(p, new Or(q, r));
            var f2 = new Or(p, new Or(q, new Not(r)));
            var f3 = new Or(p, new Or(new Not(q), r));
            var f4 = new Or(p, new Or(new Not(q), new Not(r)));
            var f5 = new Or(new Not(p), new Or(q, r));
            var f6 = new Or(new Not(p), new Or(q, new Not(r)));
            var f7 = new Or(new Not(p), new Or(new Not(q), r));
            var formula = new And(f1, new And(f2, new And(f3, new And(f4, new And(f5, new And(f6, f7))))));

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
