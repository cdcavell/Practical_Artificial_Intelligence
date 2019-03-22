using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public static class Listing_2_10
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Listing 2-10: Creating Formula (p ∨ q) ∧ (p ∨ ¬q) ∧ (¬p ∨ q) ∧ (¬p ∨ ¬r) and Finding Out If It's Satisfiable Using the DPLL Algorithm");
            Console.WriteLine(string.Empty);


            var p = new Variable(true) { Name = "p" };
            var q = new Variable(true) { Name = "q" };
            var r = new Variable(true) { Name = "r" };

            var f1 = new And(new Or(p, q), new Or(p, new Not(q)));
            var f2 = new And(new Or(new Not(p), q), new Or(new Not(p), new Not(r)));
            var formula = new And(f1, f2);

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
