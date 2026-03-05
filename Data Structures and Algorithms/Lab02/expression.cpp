#ifndef EXPRESSION
#define EXPRESSION

#include "stack_linkedListBased.cpp"
#include <sstream>

class Expression
{
    int precedence(char op)
    {
        if (op == '+' || op == '-')
            return 1;
        else if (op == '*' || op == '/')
            return 2;
        return 0; // (, )
    }
    bool is_operator(char ch) { return ch == '+' || ch == '-' || ch == '*' || ch == '/'; }
    bool is_digit(char ch) { return ch >= '0' && ch <= '9'; }

    std::string infix_to_postfix(std::string infix) // 322 + 3232
    {
        Stack_LinkedList_Based<std::string> operators;
        std::string postfix("");
        std::string number("");
        int infix_size(infix.length());
        for (int i(0); i < infix_size; ++i)
        {
            char ch(infix[i]);
            if (ch == ' ')
                continue;

            if (is_digit(ch))
                number += ch;
            else // op or ()
            {
                if (!number.empty()) // 342 54 +
                {
                    if (!postfix.empty())
                        postfix += ' ';
                    postfix += number;
                    number = "";
                }
                if (ch == '(')
                    operators.push(std::string{ch});
                else if (ch == ')')
                {
                    while (!operators.is_empty() && operators.peek() != std::string("("))
                        postfix += " " + operators.pop();

                    if (!operators.is_empty()) // to remove '('
                        operators.pop();
                }
                else if (is_operator(ch))
                {
                    while (!operators.is_empty() && operators.peek() != std::string("(") && precedence(operators.peek()[0]) >= precedence(ch))
                        postfix += " " + operators.pop();

                    operators.push(std::string{ch});
                }
            }
        }

        if (!number.empty()) // + 23
        {
            if (!postfix.empty())
                postfix += ' ';
            postfix += number;
        }

        while (!operators.is_empty()) // ++ +
            postfix += " " + operators.pop();

        return postfix;
    }

public:
    int eval(std::string infix)
    {
        if (infix.empty())
        {
            std::cout << "Error: Empty expression!" << edl;
            return 0;
        }

        std::string postfix_expr(infix_to_postfix(infix));
        std::cout << "Postfix: " << postfix_expr << edl;
        Stack_LinkedList_Based<int> values;
        std::stringstream ss(postfix_expr);
        std::string token;

        while (ss >> token)
        {
            if (is_operator(token[0]) && token.length() == 1)
            {
                if (values.get_size() < 2)
                {
                    std::cout << "Error: Invalid expression - insufficient operands!" << edl;
                    return 0;
                }

                int right(values.pop()),
                    left(values.pop()),
                    result(0);

                switch (token[0])
                {
                case '+':
                    result = left + right;
                    break;
                case '-':
                    result = left - right;
                    break;
                case '*':
                    result = left * right;
                    break;
                case '/':
                    if (right == 0)
                    {
                        std::cout << "Error: Division by zero!" << edl;
                        return 0;
                    }
                    result = left / right;
                    break;
                }
                values.push(result);
            }
            else
                values.push(std::stoi(token));
        }

        // if (values.get_size() != 1)
        // {
        //     std::cout << "Error: Invalid expression - too many operands!" << edl;
        //     return 0;
        // }

        return values.pop();
    }
};
#endif