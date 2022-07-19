using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//это алгоритм математического парсера, по сути сортировачная станция, модифицированная для работы с функциями 
namespace Hill_Climbing
{
    public static class MathParser
    {
        private static int OperatorPreced(string token)
        {
            switch (token)
            {

                case "^":
                    return 3;

                case "*":
                case "/":
                    return 2;

                case "+":
                case "-":
                    return 1;

            }
            return 0;
        }
        private static bool Function(string token)
        {
            switch (token)
            {
                case "cos":
                case "sin":
                case "tan":
                case "ctg":
                    return true;
            }
            return false;
        }

        private static string[] Separator(string input, double x)
        {
            string inputSep = input;
            string[] output;
            if (x < 0)
            {
                inputSep = inputSep.Replace("x", $"(0{x})");
            }
            else
            {
                inputSep = inputSep.Replace("x", $"{x}");

            }
            inputSep = inputSep.Replace("+", " + ");
            inputSep = inputSep.Replace("-", " - ");
            inputSep = inputSep.Replace("*", " * ");
            inputSep = inputSep.Replace("/", " / ");
            inputSep = inputSep.Replace("(", "( ");
            inputSep = inputSep.Replace(")", " )");
            inputSep = inputSep.Replace("^", " ^ ");
            inputSep = inputSep.Replace("sin", "sin ");
            inputSep = inputSep.Replace("cos", "cos ");
            inputSep = inputSep.Replace("tan", "tan ");
            inputSep = inputSep.Replace("ctg", "ctg ");
            output = inputSep.Split(' ');
            return output;
        }

        private static Stack<string> ConvertToPostfixNotation(string input, double x)
        {
            double num;
            string[] separeteInput = Separator(input, x);
            Stack<string> stack = new Stack<string>();
            Stack<string> outStack = new Stack<string>();

            foreach (string token in separeteInput)
            {
                if (double.TryParse(token, out num))
                {
                    outStack.Push(token);
                }
                else if (OperatorPreced(token) > 0)
                {
                    while (stack.Any() && (OperatorPreced(stack.Peek()) >= OperatorPreced(token)) && OperatorPreced(stack.Peek()) > 0)
                    {
                        outStack.Push(stack.Pop());
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Peek() != "(")
                    {
                        outStack.Push(stack.Pop());
                    }
                    stack.Pop();
                    if (stack.Any() && Function(stack.Peek()))
                    {
                        outStack.Push(stack.Pop());
                    }
                }
                else if (Function(token))
                {
                    stack.Push(token);
                }


            }
            while (stack.Any())
            {
                outStack.Push(stack.Pop());
            }
            outStack.Reverse();
            return outStack;
        }

        //осторожно с этой штукой, может выпасть в ошибку из-за переполнения дабла
        public static double Result(string input, double x)
        {
            Stack<string> func = new Stack<string>(ConvertToPostfixNotation(input, x));
            Stack<string> stack = new Stack<string>();
            string str = func.Pop();
            while (func.Count >= 0)
            {
                if (OperatorPreced(str) == 0 && !Function(str))
                {
                    stack.Push(str);
                    str = func.Pop();
                }
                else
                {
                    double summ = 0;
                    switch (str)
                    {
                        case "+":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                summ = a + b;
                                break;
                            }
                        case "-":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                summ = b - a;
                                break;
                            }
                        case "*":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                summ = b * a;
                                break;
                            }
                        case "/":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                summ = b / a;
                                break;
                            }
                        case "^":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                double b = Convert.ToDouble(stack.Pop());
                                summ = Math.Pow(b, a);
                                break;
                            }
                        case "cos":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                summ = Math.Cos(a);
                                break;
                            }
                        case "sin":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                summ = Math.Sin(a);
                                break;
                            }
                        case "tan":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                summ = Math.Tan(a);
                                break;
                            }
                        case "ctg":
                            {
                                double a = Convert.ToDouble(stack.Pop());
                                summ = 1.0 / Math.Tan(a);
                                break;
                            }

                    }
                    stack.Push(summ.ToString());
                    if (func.Any())
                    {
                        str = func.Pop();
                    }
                    else
                        break;

                }
            }
            return Convert.ToDouble(stack.Pop());


        }




    }
}
