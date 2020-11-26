using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorclass
{
    public interface MathExpression
    {
        string GetStringValue();
    }

    //operators + - * / ( )
    //math values: numbers eg: 1, 2 ,3.....

    class MathValue : MathExpression
    {
        double value;
        public MathValue(double _value)
        {
            this.value = _value;
        }

        public MathValue(string _value)
        {
            this.value = Double.Parse(_value);
        }
        public string GetStringValue()
        {
            return "" + this.value;
        }
        public double GetValue()
        {
            return this.value;
        }
    }

        abstract class MathOperation : MathExpression
        {
            public string Value { get; set; }
            public int Priority { get; set; }
            public MathOperation()
            {

            }

            public abstract MathValue Computevalue(MathValue value1, MathValue value2);

            public abstract MathValue Computevalue(MathValue value1);

            public string GetStringValue()
            {
                return this.Value;
            }
        }

        class AddOperation : MathOperation
        {
            public AddOperation()
            {
                this.Priority = MathsConstants.Add_Priority;
                this.Value = "+";
            }
            public override MathValue Computevalue(MathValue value1, MathValue value2)
            {
                return new MathValue(value2.GetValue() + value1.GetValue());
            }

            public override MathValue Computevalue(MathValue value1)
            {
                return value1;
            }
        }

        class SubtractOperation : MathOperation
        {
            public SubtractOperation()
            {
                this.Priority = MathsConstants.Subtract_Priority;
                this.Value = "-";
            }
            public override MathValue Computevalue(MathValue value1, MathValue value2)
            {
                return new MathValue(value2.GetValue() - value1.GetValue());
            }

            public override MathValue Computevalue(MathValue value1)
            {
                return new MathValue(-value1.GetValue() * 1.0);
            }
        }

        class MultiplicationOperation : MathOperation
        {
            public MultiplicationOperation()
            {
                this.Priority = MathsConstants.Multiplication_Priority;
                this.Value = "*";
            }
            public override MathValue Computevalue(MathValue value1, MathValue value2)
            {
                return new MathValue(value2.GetValue() * value1.GetValue());
            }

            public override MathValue Computevalue(MathValue value1)
            {
                return value1;
            }
        }

        class DivisionOperation : MathOperation
        {
            public DivisionOperation()
            {
                this.Priority = MathsConstants.Division_Priority;
                this.Value = "/";
            }
            public override MathValue Computevalue(MathValue value1, MathValue value2)
            {
                return new MathValue(value2.GetValue() / value1.GetValue());
            }

            public override MathValue Computevalue(MathValue value1)
            {
                return value1;
            }
        }
}



/*
 
Design and implement a program to evaluate mathematical expressions typed by the user. For
example, if the user typed:

5 + 2 * -3 + (12.4 – 7.6) * 10 / 2

Your program would print the result:
23
 
 */


