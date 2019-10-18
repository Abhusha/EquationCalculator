using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {

        //last updated 16/09/2018
        static void Main(string[] args)
        {
            try
            {
                string[] equation = new string[100];
                string tempEquation = "";
                //string keyword = "calc"; 
                do
                {                    
                    
                    if (args.Length == 0)
                    {
                        Console.Write("Enter Equation (q for quit) :");
                        tempEquation = Console.ReadLine();
                        if (tempEquation == "q" || tempEquation == "Q" || tempEquation == "")
                        {
                            Console.WriteLine("You have just Quit the Application");
                        }
                        else
                        {
                            Console.WriteLine("Equation:  ", tempEquation);
                            for (int i = 0; i < tempEquation.Count(); i++)
                            {
                                equation[i] += tempEquation[i];
                                Console.WriteLine(equation[i]);
                            }
                        }
                    }

                    else
                    {

                        equation = args;

                        List<string> element = equation.ToList();
                        Console.Write("The Given Equation is :  ");
                        for (int i = 0; i < element.Count(); i++)
                        {
                            Console.Write( element[i]);
                        }
                        Console.WriteLine();
                        Console.WriteLine("The Count of Element in the given Equation is:  {0}", element.Count);
                        Console.WriteLine();
                        Console.WriteLine("The equation split into element  : ");
                        for (int i = 0; i < element.Count(); i++)
                        {
                            Console.WriteLine(element[i]);
                        }
                    }

                    if (ValidationCheck(equation) == true)
                    {
                        equation = CleanEquation(equation);
                        Console.Write("The equation after cleaning is:   ");
                        for (int i = 0; i < equation.Count(); i++)
                        {
                            Console.Write(equation[i]);
                            tempEquation += equation[i];
                        }
                        Console.WriteLine();

                        string leftPartOFEquation = "";
                        string rightPartOFEquation ="";
                        List<string> spacing = CorrectSpacing(tempEquation);



                        //divide equation using '=' sign
                        Console.WriteLine("Divide the given equation into two parts using '=' sign");
                        for (int i = 0; i < Array.IndexOf(equation, "="); i++)
                            leftPartOFEquation += equation[i];
                        Console.WriteLine("leftPart of Equation : {0}",leftPartOFEquation);
                        
                        for (int i = Array.IndexOf(equation, "=") + 1; i < equation.Length; i++)
                            rightPartOFEquation += equation[i];
                        Console.WriteLine("RightPart  of Equation : {0}", rightPartOFEquation);

                        double coefficientOfX;
                        char varType = 'x'; // must make changes. variable is not constant now
                        
                        coefficientOfX = CoefficientCalculation(varType, leftPartOFEquation) - CoefficientCalculation(varType, rightPartOFEquation);
                        //Console.WriteLine("Coefficient of x:  {0}", coefficientOfX);
                        Console.ReadKey();
                    }
                    else
                        ErrorMsg("Please enter a Valid Equation. ");
                }
                while (args.Length != 0 || tempEquation== "");

                
            }
            catch(Exception ex)
            {
                if (ex is DivideByZeroException)
                {
                    ErrorMsg("Division by Zero error");
                }

                else if (ex is OverflowException)
                    {
                        ErrorMsg("Integer out of range error");
                    }
                else if (ex is Exception)
                {
                    ErrorMsg(ex.Message);
                }
             Console.ReadKey();
            }
        }


        public static double CoefficientCalculation ( char varType, string stack)
        {
            
            double coefficient = 0;            
            for (int i = 0; i < stack.Length; i++)
            {
                if (stack[i].Equals(varType)) // for example:   12-x
                {
                    //Console.WriteLine("x:  {0}",varType);
                    if (i > 0 && stack[i - 1].Equals('-'))
                        coefficient--;// makes coefficent negative by decreasing value
                                      
                    else if(i > 0 && stack[i - 1].Equals('+'))
                        coefficient++; //since variable is independent, coefficient becomes +1
                }
                else if (stack[i].ToString().EndsWith(varType.ToString())) // If Var has a number before x: 5x
                {
                    if (i > 0 && stack[i - 1].Equals('-'))
                    {
                        //stack[i].ToString().Length - varType.Length ==> separate -10 from -10x
                        coefficient -= Double.Parse(stack[i].ToString().Substring(0, stack[i].ToString().Length - varType.ToString().Length));
                    }                
                    else if (i < stack.Length - 1 && stack[i + 1].Equals("/"))
                        coefficient += 1 / (Double.Parse((stack[i + 2]).ToString()));

                    else if (i > 0 && stack[i - 1].Equals("*"))
                        coefficient += Double.Parse(stack[i].ToString().Substring(0, stack[i].ToString().Length - varType.ToString().Length - 1));

                    else if (i < stack.Length - 1 && stack[i + 1].Equals("/"))
                        coefficient *= 1 / (Double.Parse(stack[i + 2].ToString()));

                    else
                    {
                        coefficient += Double.Parse(stack[i].ToString().Substring(0, stack[i].ToString().Length - varType.ToString().Length));
                        if (i < stack.Length - 1 && stack[i + 1].Equals("/"))
                            coefficient *= 1 / (Double.Parse(stack[i + 2].ToString()));
                    }
                }
            }
            
            return coefficient;
        }


        public static bool ValidationCheck(string[] equation)
        {
            //equation doesnot contain "=" / equals to sign
            if (Array.IndexOf(equation, "=") == -1)
            {
                ErrorMsg("The Given Equation doesnot have '=' sign.");
                return false;
            }

            //equation doesnot contain "X" variable
            if (!Array.Exists(equation, x => x.ToLower().Contains("x")))             
            {
                ErrorMsg("Equation doesnot cotain variable. ");
                return false;
            }                          
            else 
            return true;
        }


        public static void ErrorMsg(string msg)
        {
            Console.WriteLine(msg);
        }


        static string[] CleanEquation(string[] equation)
        {
            List<string> element = equation.ToList();
            //Console.WriteLine(element.Count);
            if (element[0]=="calc")
            {
                element.RemoveAt(0); //to remove calc keyword
            }
           
            Console.Write("Equation after removing keyword:    ");
            for (int i = 0; i < element.Count; i++)
            {
                Console.Write(element[i]);
            }           
            Console.WriteLine();


            for (int i = 0; i < element.Count; i++)
            {
                //Console.WriteLine(element.Count);
                //Console.WriteLine(element[i]);

                //Checks repeating Operators and remove the repeating element: x + + 2 => x + 2
                //I have used element.Count-1 to avoid exception error in last loop
                if (i < element.Count-1  && IsItAnOperator(element[i]) && (element[i] == element[i + 1]))
                {
                    //Console.WriteLine(element[0]);
                    element.RemoveAt(i);
                    i--; 
                }
                // Checks if there are more than one operator: x - * 2 
                //I have used element.Count-1 to avoid exception error in last loop
                else if (i < element.Count-1 && IsItAnOperator(element[i]) && IsItAnOperator(element[i + 1]))
                    {
                    ErrorMsg("Equation cannot be solved with two different Operators together. Please enter a valid equation.");
                    }
            }


            //Checks if the equation ends with operator, add 0
            if (IsItAnOperator(element[(element.Count) - 1]))
            {
                element.Add("0");
            }
                return element.ToArray();
        }


        
        static bool IsItAnOperator(string sentOperator)
        {
            bool operatorCheck = false;
            string[] operators = { "+", "-", "*", "/", "%" };
            if (Array.IndexOf(operators, sentOperator) != -1) // Operator doesnot exists if index is -1
                operatorCheck = true;
            return operatorCheck;
        }


        // if there is no space, element.count doesnot split
        private static List<string> CorrectSpacing(string sentString) 
        {
            sentString = sentString.Replace("=", " = ");
            sentString = sentString.Replace("+", " + ");
            sentString = sentString.Replace("-", " - ");
            sentString = sentString.Replace("*", " * ");
            sentString = sentString.Replace("/", " / ");
            sentString = sentString.Replace("%", " % ");
            sentString = sentString.Replace("(", " ( ");
            sentString = sentString.Replace(")", " ) ");
            sentString = sentString.Trim();
            while (sentString.Contains("  "))
            {
                sentString = sentString.Replace("  ", " ");
            }              
            return sentString.Split(' ').ToList();
        }


    }
}
