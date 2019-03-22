using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public class Heuristics
    {
        // Just return first literal found
        public static Formula ChooseLiteral(Cnf cnf)
        {
            foreach (var clause in cnf.Clauses)
                foreach (Formula literal in clause.Literals)
                {
                    return literal;
                }

            return null;
        }
    }
}
