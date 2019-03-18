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
        public abstract Formula ToCnf();

        public Formula DistributeCnf(Formula p, Formula q)
        {
            if (p is And)
                return new And(DistributeCnf((p as And).P, q), DistributeCnf
                ((p as And).Q, q));
            if (q is And)
                return new And(DistributeCnf(p, (q as And).P),
                DistributeCnf(p, (q as And).Q));
            return new Or(p, q);
        }
    }
}
