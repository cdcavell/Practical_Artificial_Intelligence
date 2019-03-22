using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    class Listing_2_13
    {
        public static void Display()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Listing 2-13: Pigeonhole Principle Modeled in Our Program for the Case Where m = 3, n = 2; i.e., m pigeons, n pigeonholes");
            Console.WriteLine(string.Empty);

            // Pigeonhole Principle m = 3, n = 2
            var p11 = new Variable(true) { Name = "p11" };
            var p12 = new Variable(true) { Name = "p12" };

            var p21 = new Variable(true) { Name = "p21" };
            var p22 = new Variable(true) { Name = "p22" };

            var p31 = new Variable(true) { Name = "p31" };
            var p32 = new Variable(true) { Name = "p32" };

            var f1 = new Or(p11, p12);
            var f2 = new Or(p21, p22);
            var f3 = new Or(p31, p32);

            var f4 = new Or(new Not(p11), new Not(p21));
            var f5 = new Or(new Not(p11), new Not(p31));
            var f6 = new Or(new Not(p21), new Not(p31));

            var f7 = new Or(new Not(p12), new Not(p22));
            var f8 = new Or(new Not(p12), new Not(p32));
            var f9 = new Or(new Not(p22), new Not(p32));

            var formula = new And(f1, new And(f2, new And(f3, new And(f4,
            new And(f5, new And(f6, new And(f7, new And(f8, f9))))))));
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
