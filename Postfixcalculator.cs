using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace calculatorclass
{
    static class Postfixcalculator
    {
        public static List<MathExpression> ConvertToInfix(string input)
        {
            // list to collect infix expression.
            List<MathExpression> expressions = new List<MathExpression>();

            //for removing whitespace in the expresion
            input = input.Replace(" ", "");

            //If input length is 0, return immediately.
            if (input.Length == 0)
            {
                Console.WriteLine("InValid expression");
                return null;
            }

            //convert inout string to char array to perfomr operations on each character.
            char[] inputCharArray = input.ToCharArray();
            StringBuilder sb = new StringBuilder();
            string operators = "()*/+-";
            string operatorsWithoutBracket = "*/+-";
            for (int i = 0; i < inputCharArray.Length; i++)
            {

                if (operators.IndexOf(inputCharArray[i]) >= 0)
                {
                    if (sb.Length > 0)
                    {
                        //this means there's digit stored in string builder.Hence add equivalent math value to expression.
                        expressions.Add(GetMathValue(sb.ToString()));
                        sb.Length = 0;
                    }

                    if (inputCharArray[i] == '-' && expressions.Count == 0)
                    {
                        //This means first character of input string is -
                        sb.Append(inputCharArray[i]);
                    }
                    else if (inputCharArray[i] == '-' && operatorsWithoutBracket.IndexOf(expressions[expressions.Count - 1].GetStringValue()) >= 0)
                    {
                        //if we have an operator, and then we have a -, then that is part of number
                        sb.Append(inputCharArray[i]);
                    }
                    else
                    {
                        //Add equivalent math value to expression.
                        expressions.Add(GetMathValue(inputCharArray[i].ToString()));
                    }
                }
                else
                {
                    //if we have number, check after to see if it's another number, and keep appending 
                    //these together until we have an operator.
                    sb.Append(inputCharArray[i]);
                }
            }
            if ((sb.Length > 0))
            {
                //this means there's digit stored in string builder.
                //this is to incorporate last digit in input.
                expressions.Add(GetMathValue(sb.ToString()));
                sb.Length = 0;
            }
            return expressions;
        }

        private static MathExpression GetMathValue(string input)
        {
            if (Double.TryParse(input, out double value))
            {
                return new MathValue(value);
            }
            else if (input == "+")
            {
                return new AddOperation();
            }
            else if (input == "-")
            {
                return new SubtractOperation();
            }
            else if (input == "*")
            {
                return new MultiplicationOperation();
            }
            else if (input == "/")
            {
                return new DivisionOperation();
            }
            else if (input == "(")
            {
                return new MathLeftBracket();
            }
            else if (input == ")")
            {
                return new MathRightBracket();
            }
            else return new MathValue(0);
        }


        public static List<MathExpression> InfixToPostfix(List<MathExpression> expressions)
        {
            Stack<MathExpression> operatorStack = new Stack<MathExpression>();
            List<MathExpression> postFixExpressions = new List<MathExpression>();
            foreach (MathExpression value in expressions)
            {
                if (value is MathValue)
                {
                    //If expression is operand, simply add it to list.
                    postFixExpressions.Add(value);
                }
                else if (value is MathRightBracket)
                {
                    //If right bracket, solve that first. Keep on adding operators from stack to post fix expression untill left bracket is encountered.
                    while (operatorStack.Count != 0 && operatorStack.Peek().GetStringValue() != "(")
                    {
                        postFixExpressions.Add(operatorStack.Pop());
                    }

                    if (operatorStack.Peek().GetStringValue() != "(")
                    {
                        //If there's a mismatch in brackets, we cannot evaluate.
                        Console.WriteLine("Invalid string. Unpaired paranthesis present");
                        throw new Exception("Invalid string. Unpaired paranthesis present");

                    }
                    else
                    {
                        //Pop out "(" bracket. Postfix expression shouldn't contain brackets.
                        operatorStack.Pop();
                    }
                }
                else
                {
                    if (operatorStack.Count == 0)
                    {
                        //If stack empty, simply push operator in stack.
                        operatorStack.Push(value);
                    }
                    else
                    {
                        //when not empty, we will check precedence.
                        MathOperation currentOperand = (MathOperation)value;
                        int currentOperandPriority = currentOperand.Priority;
                        while (operatorStack.Count != 0 && ((MathOperation)operatorStack.Peek()).Priority >= currentOperandPriority && !(operatorStack.Peek() is MathLeftBracket))
                        {
                            postFixExpressions.Add(operatorStack.Pop());

                        }
                        operatorStack.Push(value);
                    }
                }
            }
            while (operatorStack.Count > 0)
            {
                postFixExpressions.Add(operatorStack.Pop());
            }
            return postFixExpressions;
        }

        public static double CalulateFromPostFix(List<MathExpression> expressions)
        {
            Stack<MathValue> values = new Stack<MathValue>();
            foreach (MathExpression expression in expressions)
            {
                //Console.WriteLine(expression);
                if (expression is MathValue)
                {
                    values.Push((MathValue)expression);
                }
                else if (expression is MathOperation)
                {
                    if (values.Count > 1)
                    {
                        MathValue newValue = ((MathOperation)expression).Computevalue(values.Pop(), values.Pop());
                        values.Push(newValue);
                    }
                    else if (values.Count == 1)
                    {
                        MathValue newValue = ((MathOperation)expression).Computevalue(values.Pop());
                        values.Push(newValue);
                    }
                }
            }
            return values.Pop().GetValue();
        }
    }
}
