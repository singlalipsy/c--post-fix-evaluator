using calculatorclass;
using System;
using System.Collections.Generic;


namespace Assignment1
{
    public class Program
    {
        public static string ProcessCommand(string input)
        {
            try
            {
                List<MathExpression> expressions = Postfixcalculator.ConvertToInfix(input);
                expressions = Postfixcalculator.InfixToPostfix(expressions);
                double value = Postfixcalculator.CalulateFromPostFix(expressions);
                return "" + value;
            }
            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }
        }

        static void Main(string[] args)
        {
            string input;
            string result="";
            while ((input = Console.ReadLine()) != "exit")
            {
                if(result.Length!=0 && input.Contains("ans")){
                    input=input.Replace("ans",result);
                }
                result =ProcessCommand(input);
                Console.WriteLine(result);
            }
        }
    }
}

