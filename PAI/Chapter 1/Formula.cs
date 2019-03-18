using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public abstract class Formula
    {
        public abstract bool Evaluate();
        public abstract IEnumerable<Variable> Variables();
        public abstract Formula ToNnf();
    }
}
