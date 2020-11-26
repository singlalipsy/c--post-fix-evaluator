using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorclass
{
    public interface MathBracket
    {
        string GetStringValue();
    }

    class MathLeftBracket : MathOperation, MathExpression
    {
        public MathLeftBracket()
        {
            this.Value = "(";
            this.Priority = MathsConstants.Bracket_Priority;
        }
        public override MathValue Computevalue(MathValue value1, MathValue value2)
        {
           return new MathValue(0);
        }

        public override MathValue Computevalue(MathValue value1)
        {
            return value1;
        }
    }

    class MathRightBracket : MathOperation,MathExpression
    {
        public MathRightBracket()
        {
            this.Value = ")";
            this.Priority = MathsConstants.Bracket_Priority;
        }
        public override MathValue Computevalue(MathValue value1, MathValue value2)
        {
            return new MathValue(0);
        }

        public override MathValue Computevalue(MathValue value1)
        {
            return value1;
        }
    }
}
