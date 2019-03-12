using System;
using System.Collections.Generic;
using System.Text;

namespace PAI
{
    public class Variable : Formula
    {
        public bool Value { get; set; }

        public Variable(bool value)
        {
            Value = value;
        }
        public override bool Evaluate()
        {
            return Value;
        }

        public override IEnumerable<Variable> Variables()
        {
            return new List<Variable>() { this };
        }
    }
}
