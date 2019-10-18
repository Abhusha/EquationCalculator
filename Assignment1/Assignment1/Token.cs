using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
   
    class Token
    {
       public enum TokenType
        {
            Number,
            Variable,
            Parenthesis,
            Operator,
            Null,
            LBracket,
            RBracket
        };

       public enum Associativity
        {
            Left,
            Right
        };

        TokenType tokenType = TokenType.Null;
        char operators = ' ';
        Associativity assoc = Associativity.Left;
        int precendence = 0;
        int operandCount = 0;
        int parameterCount = 0;

        double val = 0.0;
        public TokenType Type { get; }


        public Token() { }

        public TokenType GetTokenType
        {
            get { return tokenType; }
        }

        public double Val
        {
            get { return val; }
            set { val = value; }
        }
        public Associativity Assoc
        {
            get { return assoc; }
        }

        public char Operator
        {
            get { return operators; }
        }

        public int Precedence
        {
            get { return precendence; }
        }

        public int ParaCount
        {
            get { return operandCount; }
        }


        public Token StringToToken(string sentString)
        {
            Token tokn = new Token();

            if (Double.TryParse(sentString, out tokn.val))
                tokn.tokenType = Token.TokenType.Number;
            else if (sentString == "(")
                tokn.tokenType = Token.TokenType.LBracket;
            else if (sentString == ")")
                tokn.tokenType = TokenType.RBracket;
            else
            {
                switch (sentString)
                {
                    case "+":
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '+';
                        tokn.assoc = Associativity.Left;
                        tokn.precendence = 1;
                        tokn.operandCount = 2;
                        break; 
                    case "-": //its binary Operator, not the unary one. 
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '-';
                        tokn.assoc = Associativity.Left;
                        tokn.precendence = 1; // precedence same as "+"
                        tokn.operandCount = 2;
                        break;
                    case "*":
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '*';
                        tokn.assoc = Associativity.Left;
                        tokn.precendence = 2;
                        tokn.operandCount = 2;
                        break;
                    case "/":
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '/'; 
                        tokn.assoc = Associativity.Left;
                        tokn.precendence = 2; // precedence same as "*" & "%"
                        tokn.operandCount = 2;
                        break;
                    case "%":
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '%';
                        tokn.assoc = Token.Associativity.Left;
                        tokn.precendence = 2;  // precedence same as "*" & "/" 
                        tokn.operandCount = 2;
                        break;
                    case "^":
                        tokn.tokenType = TokenType.Operator;
                        tokn.operators = '^';
                        tokn.assoc = Associativity.Right;
                        tokn.precendence = 4;
                        tokn.operandCount = 2;
                        break;
                    default:
                        tokn.tokenType = TokenType.Null;
                        break;
                }
            }
            return tokn;
        }
    }
}
