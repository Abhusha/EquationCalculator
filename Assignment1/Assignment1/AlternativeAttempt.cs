using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    //This method not called any where. 
    //Please ignore this class. It was an alternative search for solution.
    class AlternativeAttempt
    {
        static void AlternativeMain() 
        {
            string equation = "";
            string[] equationArray;
            do
            {
                try
                {
                    start:
                   
                        Console.Write("Enter Equation (q for quit) :");
                        equation = Console.ReadLine();
                    if (equation == "q" || equation == "Q" || equation == "")
                    break;                    
                   
                            
                    equationArray = equation.Split("x".ToCharArray());                      
                    equation = CleanEquation(equation);
                    Console.WriteLine("The equation after cleaning is: ", equation);
                    bool validationCheckVariable = ValidationCheck(equation);
                    if (validationCheckVariable == false)
                    {
                        ErrorMsg("The Equation is not valid");
                        goto start;
                    }

                    else
                    {
                        Char[] arrayOfGivenEquation = equation.ToCharArray(); ;
                        Console.WriteLine("Array of Given Equation is: ");
                        for (int i = 0; i < equation.Length; i++)
                        {
                            Console.WriteLine("Array [{0}]:  {1}", i, arrayOfGivenEquation[i]);
                        }

                        int a = equation.IndexOfAny("(".ToCharArray());
                        //Console.WriteLine("a= {0}", a);
                        if (a == -1) Console.WriteLine("There is no opening bracket in the given equation");
                        int b = equation.IndexOf(")");
                        //Console.WriteLine  ("b = {0}",b);
                        if (b == -1) Console.WriteLine("There is no closing bracket in the given equation");
                        if ((a == -1 && b > 0) || (a > 0 && b == -1))
                        {
                            ErrorMsg("Invalid Equation, it doesnot have either of the opening or closing Bracket");
                        }

                        char characterBeforeOpeningBracket;
                        if (a != -1)
                        {
                            characterBeforeOpeningBracket = arrayOfGivenEquation[a - 1];
                            //Console.WriteLine(characterBeforeOpeningBracket);
                            if (characterBeforeOpeningBracket == '+' || characterBeforeOpeningBracket == '-' || characterBeforeOpeningBracket == '=')
                                equation = equation.Replace('(', ' ');
                            equation = equation.Replace(" ", "");
                            Console.WriteLine(equation);

                            char digitBeforeOperator;
                            if (characterBeforeOpeningBracket == '*')
                            {
                                digitBeforeOperator = arrayOfGivenEquation[a - 2];
                                Console.WriteLine("this is the number before * : {0}", digitBeforeOperator);
                                for (int i = a + 1; i <= b - 1; i = i + 2)
                                {
                                    Console.WriteLine("i= {0}", i);


                                    //to be continued
                                }
                            }
                        }

                        if (b != -1)
                        {
                            char characterAfterClosingBracket = arrayOfGivenEquation[b + 1];
                            //Console.WriteLine(characterAfterClosingBracket);
                            if (characterAfterClosingBracket == '+' || characterAfterClosingBracket == '-' || characterAfterClosingBracket == '=')
                                equation = equation.Replace(')', ' ');
                            equation = equation.Replace(" ", "");
                            Console.WriteLine(equation);
                        }


                        //divide equation using left bracket
                        //String[] partOfEquation = equation.Split("(".ToCharArray());
                        //  for (int i=0; i<partOfEquation.Length; i++)
                        //{
                        //    Console.WriteLine(partOfEquation[i]);
                        //}
                        Console.ReadLine();
                    }
                }

                catch (Exception ex)
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

                    Console.ReadLine();
                }
            }
            while (equation != "q");

        }

        public static void ErrorMsg(string msg)
        {
            Console.WriteLine(msg);
        }

        //This method:
        //cleans the equation if it ends with operators like +, -
        //removes double operators like ++ , --
        static string CleanEquation(string equation)
        {
            string[] equationArray = equation.Split("x".ToCharArray());
            if (equation.EndsWith("+") || equation.EndsWith("-"))
            {
                equation = equation.TrimEnd("+-".ToCharArray());
            }

            for (int i = 0; i < equationArray.Length; i++)
            {
                //Checks repeating operations and remove one repeating element: x + + 2 => x + 2
                if (i < equation.Length - 1 && IsItAnOperator(equationArray[i]) && (equationArray[i] == equationArray[i + 1]))
                {
                    equation.ToString().Remove(i);
                    i--;
                }
                // Checks if there are more than one operator: x - * 2 
                else if (i < equation.Length - 1 && IsItAnOperator(equationArray[i]) && IsItAnOperator((equationArray[i + 1])))
                    ErrorMsg("Equation cannot be solved with two different Operators together. Please enter a valid equation.");
            }

            //checks if multiple operator exists and replace that with single operator
            // The following replacement of repeated operator is lengthy. 
            //Need to comment later before submission
            while (equation.IndexOf("++") >= 0)
            {
                equation = equation.Replace("++", "+");
            }

            while (equation.IndexOf("--") >= 0)
            {
                equation = equation.Replace("--", "-");
            }
            while (equation.IndexOf("**") >= 0)
            {
                equation = equation.Replace("**", "*");
            }
            while (equation.IndexOf("%%") >= 0)
            {
                equation = equation.Replace("%%", "%");
            }

            while (equation.IndexOf("+++") >= 0)
            {
                equation = equation.Replace("+++", "+");
            }
            while (equation.IndexOf("---") >= 0)
            {
                equation = equation.Replace("---", "-");
            }
            while (equation.IndexOf("***") >= 0)
            {
                equation = equation.Replace("***", "*");
            }
            return equation;
        }


        //verify if any of the basic operators in array exists in the equation
        static bool IsItAnOperator(string sentOperator)
        {
            bool temp = false;
            string[] operators = { "+", "-", "*", "/", "%" };
            if (Array.IndexOf(operators, sentOperator) != -1) // Operator doesnot exists if index is -1
                temp = true;
            return temp;
        }


        //check if the equation is valid
        static bool ValidationCheck(string equation)
        {
            if (equation.Split("=".ToCharArray()).Length > 2)
            {
                ErrorMsg("Invalid Equation. A Equation cannot have more than one '=' Sign.");
                return false;
            }

            else if ((equation.IndexOf("x")) == -1)
            {
                ErrorMsg("Variable doesnot exist in the equation. " +
                         "Please enter a valid equation.");
                return false;
            }

            else if (equation.IndexOf("=") == -1) //input doens't contain "="
            {
                ErrorMsg("Equation doesnot have '=' or equals to sign. " +
                             "Please enter a valid equation.");
                return false;
            }

            else
                return true;
        }
    }
}
