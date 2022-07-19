using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        class hill_clim
        {
            private double bestRes, best, StepSize;
            private string Funct;
            int count;

            public hill_clim(double step, string funct, int[] bord)
            {
                count = 0;
                Random rnd = new Random();
                this.best = rnd.Next(bord[0], bord[1]);
                this.StepSize = step;
                this.Funct = funct;
            }

            public double Start_Climding()
            {
                bestRes = MathParser.Result(Funct, best);
                double NeighborS, NeighborM;
                NeighborS = best - StepSize;
                NeighborM = best + StepSize;
                bool ent = false;
                if (MathParser.Result(Funct, NeighborS) > bestRes)
                {
                    bestRes = MathParser.Result(Funct, NeighborS);
                    best = NeighborS;
                    ent = true;
                    Console.WriteLine("S" + count);
                }
                if (MathParser.Result(Funct, NeighborM) > bestRes)
                {
                    bestRes = MathParser.Result(Funct, NeighborM);
                    best = NeighborM;
                    ent = true;
                    Console.WriteLine("M" + count);
                }
                if(!ent)
                {
                    StepSize = StepSize * 0.9;
                    count++;
                    Console.WriteLine(count);
                }
                return best;
            }
        }


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

        static void Main(string[] args)
        {
            string s = "0-((x-5)^2+50*sin(x)+50)";
            int[] b = new int[] { -10, 10 };
            hill_clim hc = new hill_clim(10, s, b);
            double max = 0;
            for (int i = 0; i < 200; i++)
                max = hc.Start_Climding();
            Console.WriteLine(max);
            Console.ReadKey();
        }
    }
}
