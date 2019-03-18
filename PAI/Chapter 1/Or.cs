﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public class Or : BinaryGate
    {
        public Or(Formula p, Formula q) : base(p, q)
        { }

        public override bool Evaluate()
        {
            return P.Evaluate() || Q.Evaluate();
        }

        public override Formula ToNnf()
        {
            return new Or(P.ToNnf(), Q.ToNnf());
        }
    }
}
