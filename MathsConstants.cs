using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorclass
{
    static class MathsConstants
    {
        //bracket has highest priority.
        public const int Bracket_Priority = 3;
        public const int Add_Priority = 1;
        public const int Subtract_Priority = 1;
        public const int Multiplication_Priority = 2;
        public const int Division_Priority = 2;
        public const string Left_Bracket = "(";
        public const string Right_Bracket = ")";
    }
}
