using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class OperatorParser
    {
        string str;

        internal IDictionary<string, Operators> operators { get; set; } = new Dictionary<string, Operators>
        {
            ["+"] = new Operators { Name = "+", Precedence = 1, RightAssociative =false},
            ["-"] = new Operators { Name = "-", Precedence = 1, RightAssociative = false},
            ["*"] = new Operators { Name = "*", Precedence = 2, RightAssociative =false},
            ["/"] = new Operators { Name = "/", Precedence = 2, RightAssociative =false},
            ["^"] = new Operators { Name = "^", Precedence = 3, RightAssociative = true}
        };

        

    }
}

