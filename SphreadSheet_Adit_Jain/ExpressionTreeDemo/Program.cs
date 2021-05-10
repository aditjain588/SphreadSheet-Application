// <copyright file="Program.cs" company="Adit Jain">
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
    /// Program to demo functionality of expression tree.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Starting point of program.
        /// </summary>
        /// <param name="args">args. </param>
        private static void Main(string[] args)
        {
            int menuChoice = 0;
            string expression = string.Empty;
            expression = "A1+10+B1";

            ExpressionTree tree = new ExpressionTree(expression);

            while (menuChoice != 4)
            {
                Console.WriteLine("Menu (current expression= " + expression + ")");
                Console.WriteLine(" 1 = Enter a new Expression");
                Console.WriteLine(" 2 = Set a variable value");
                Console.WriteLine(" 3 = Evaluate tree");
                Console.WriteLine(" 4 = Quit");

                menuChoice = int.Parse(Console.ReadLine());

                switch (menuChoice)
                {
                    case 1:
                        Console.WriteLine(" 1 = Enter new Expression: ");
                        expression = Console.ReadLine();
                        tree = new ExpressionTree(expression);
                        break;
                    case 2:
                        Console.WriteLine(" 2 = Set a variable value");
                        Console.WriteLine("Enter variable whose value is to be set");
                        string variable = Console.ReadLine();
                        Console.WriteLine("Enter variable value ");
                        double value = Convert.ToDouble(Console.ReadLine());
                        tree.SetVariable(variable, value);
                        break;
                    case 3:
                        Console.WriteLine(" 3 = Evaluate tree");
                        Console.WriteLine(tree.Evaluate());
                        break;
                    case 4:
                        break;
                }
            }
        }
    }
}
