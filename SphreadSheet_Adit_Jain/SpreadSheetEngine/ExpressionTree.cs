// <copyright file="ExpressionTree.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for implementing expression tree.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Dictionary for variable name and variable value.
        /// </summary>
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private string infixExpression = string.Empty;
        private ExpressionTreeNode root;
        private OperatorNodeFactory operatorNodeFactory = new OperatorNodeFactory();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">Expression. </param>
        public ExpressionTree(string expression)
        {
            // this.infixExpression = expression;
            this.root = this.BuildTree(expression);
        }

         /// <summary>
         /// Builds the tree from infix expression.
         /// </summary>
         /// <param name="expression">infix expression. </param>
          /// <returns>Expression tree. </returns>
        public ExpressionTreeNode BuildTree(string expression)
        {
            Stack<ExpressionTreeNode> nodeStack = new Stack<ExpressionTreeNode>();
            List<string> postfixExpression = this.BuildPostfixExpression(expression);

            int i = 0;
            while (i < postfixExpression.Count)
            {
                if (postfixExpression[i].Length == 1 && this.IsOperator(char.Parse(postfixExpression[i])))
                {
                    BinaryOperatorNode newNode = this.operatorNodeFactory.CreateOperatorNode(char.Parse(postfixExpression[i]));
                    newNode.Left = nodeStack.Pop();
                    newNode.Right = nodeStack.Pop();
                    nodeStack.Push(newNode);
                    i++;
                }
                else
                {
                    try
                    {
                        ConstantNumericalNode newNode = new ConstantNumericalNode(Convert.ToDouble(postfixExpression[i]));
                        nodeStack.Push(newNode);
                        i++;
                    }
                    catch (Exception)
                    {
                        VariableNode newNode = new VariableNode(postfixExpression[i], ref this.variables);
                        this.variables.Add(postfixExpression[i], 0);
                        nodeStack.Push(newNode);
                        i++;
                    }
                }
            }

            return nodeStack.Pop();
        }

        /// <summary>
        /// Sets the value of variable.
        /// </summary>
        /// <param name="variableName">Variable whose value is to be set. </param>
        /// <param name="variableValue">Value of variabe. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            // this.variables.Add(variableName, variableValue);
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluates the expression.
        /// </summary>
        /// <returns>Evaluated result. </returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Checks if a character is operator or not.
        /// </summary>
        /// <param name="c">Character to be checked. </param>
        /// <returns>True or False. </returns>
        private bool IsOperator(char c)
        {
            if (c == '+' || c == '-' || c == '*'
                || c == '/' || c == '^')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if a character is paranthesis or not. 
        /// </summary>
        /// <param name="c">Character to be checked. </param>
        /// <returns>True or False. </returns>
        private bool IsParanthesis(char c)
        {
            if (c == '(' || c == ')')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Builds postfix expression from infix.
        /// </summary>
        /// <param name="infixExp">Infix expression. </param>
        /// <returns>Postfix expression. </returns>
        private List<string> BuildPostfixExpression(string infixExp)
        {
            List<string> postfixExpression = new List<string>();
            Stack<char> operators = new Stack<char>();
            int i = 0;

            while (i < infixExp.Length)
            {
                // char c = infixExp[i];
                // if an operand or variable is encountered.
                if ((this.IsOperator(infixExp[i]) == false) && this.IsParanthesis(infixExp[i]) == false)
                {
                    string num = string.Empty;
                    while (this.IsOperator(infixExp[i]) == false && this.IsParanthesis(infixExp[i]) == false)
                    {
                        num += infixExp[i];
                        i++;
                        if (i == infixExp.Length)
                        {
                            break;
                        }
                    }

                    postfixExpression.Add(num);
                }
                else if (infixExp[i].ToString() == "(")
                {
                    operators.Push(infixExp[i]);
                    i++;
                }
                else if (infixExp[i].ToString() == ")")
                {
                    while (operators.Count > 0 && (operators.Peek() != '('))
                    {
                        postfixExpression.Add(operators.Pop().ToString());
                    }

                    if (operators.Count > 0 && operators.Peek() != '(')
                    {
                        Console.WriteLine("Invalid expression");
                    }
                    else
                    {
                        operators.Pop();
                    }

                    i++;
                }

                // handles operator.
                else
                {
                    while (operators.Count > 0 && this.Prec(infixExp[i]) <=
                                  this.Prec(operators.Peek()))
                    {
                        postfixExpression.Add(operators.Pop().ToString());
                    }

                    operators.Push(infixExp[i]);
                    i++;
                }
            }

            while (operators.Count > 0)
            {
                postfixExpression.Add(operators.Pop().ToString());
            }

            return postfixExpression;
        }

        /// <summary>
        /// List of all variable names.
        /// </summary>
        /// <returns>List. </returns>
        public string[] GetVariableNames()
        {
            return this.variables.Keys.ToArray();
        }

        /// <summary>
        /// Determines the precedence of each character.
        /// </summary>
        /// <param name="ch">Character whose precedence is needed. </param>
        /// <returns>integer. </returns>
        private int Prec(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }

            return -1;
        }
    }
}
