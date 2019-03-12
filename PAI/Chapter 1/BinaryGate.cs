using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAI
{
    public abstract class BinaryGate : Formula
    {
        public Formula P { get; set; }
        public Formula Q { get; set; }

        public BinaryGate(Formula p, Formula q)
        {
            P = p;
            Q = q;
        }
        public override IEnumerable<Variable> Variables()
        {

            return P.Variables().Concat(Q.Variables());
        }
    }
}
