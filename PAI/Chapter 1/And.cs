using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public class And : BinaryGate
    {
        public And(Formula p, Formula q) : base(p, q)
        { }

        public override bool Evaluate()
        {
            return P.Evaluate() && Q.Evaluate();
        }
    }
}
