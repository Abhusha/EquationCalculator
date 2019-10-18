using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{

    class ShuntingYardAlgorithm
    {
        public IEnumerable<Token> ShuntingYard(IEnumerable<Token> sentToken)
        {
            OperatorParser opParser = new OperatorParser();
            Queue<Token> outputQueue = new Queue<Token>();
            var stack = new Stack<Token>();
            foreach (var token in sentToken)
            {
                switch (token.Type)
                {
                    case Token.TokenType.Number:
                        outputQueue.Enqueue(token); //  push it to the output queue.
                        break;

                    case Token.TokenType.Variable:
                        yield return token; 
                        break;
                    
                    case Token.TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == Token.TokenType.Operator)
                            yield return stack.Pop();
                        stack.Push(token);
                        break;

                    case Token.TokenType.Parenthesis:

                        if (Token.TokenType.Parenthesis == Token.TokenType.LBracket)
                        {
                            outputQueue.Enqueue(token);
                        }
                        else if (Token.TokenType.Parenthesis == Token.TokenType.RBracket)
                        {
                            //If code have LeftBracket, outputQueue.Enqueue(token);    
                        }                       

                        break;
                    default:
                        throw new Exception("Wrong token");
                }
            }
            while (stack.Any())
            {
                var token = stack.Pop();
                if (token.Type == Token.TokenType.Parenthesis)
                    throw new Exception("Mismatched parentheses");
                yield return token;
            }
        }

        
    }
}

// https://en.wikipedia.org/wiki/Shunting-yard_algorithm

//The algorithm in detail
//Important terms: Token, Function, Operator associativity, Precedence

///* This implementation does not implement composite functions,functions with variable number of arguments, and unary operators. */

//while there are tokens to be read:
//    read a token.
//    if the token is a number, then:
//        push it to the output queue.
//    if the token is a function then:
//        push it onto the operator stack
//    if the token is an operator, then:
//        while ((there is a function at the top of the operator stack)
//               or(there is an operator at the top of the operator stack with greater precedence)
//               or(the operator at the top of the operator stack has equal precedence and is left associative))
//              and(the operator at the top of the operator stack is not a left bracket) :
//            pop operators from the operator stack onto the output queue.
//        push it onto the operator stack.
//    if the token is a left bracket (i.e. "("), then:
//        push it onto the operator stack.
//    if the token is a right bracket (i.e. ")"), then:
//        while the operator at the top of the operator stack is not a left bracket:
//            pop the operator from the operator stack onto the output queue.
//        pop the left bracket from the stack.
//        /* if the stack runs out without finding a left bracket, then there are mismatched parentheses. */
//if there are no more tokens to read:
//    while there are still operator tokens on the stack:
//        /* if the operator token on the top of the stack is a bracket, then there are mismatched parentheses. */
//        pop the operator from the operator stack onto the output queue.
//exit.


