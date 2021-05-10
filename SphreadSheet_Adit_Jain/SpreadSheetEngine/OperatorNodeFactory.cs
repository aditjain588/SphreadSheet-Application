// <copyright file="OperatorNodeFactory.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Operator node factory.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Dictionary of available operators.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>
        {
            { '+', typeof(AdditionNode) },
            { '-', typeof(SubtractionNode) },
            { '*', typeof(MultiplicationNode) },
            { '/', typeof(DivisionNode) }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
        }

        /// <summary>
        /// Creates an operator note from a character.
        /// </summary>
        /// <param name="c">the operator character.</param>
        /// <returns>an operator node.</returns>
        public BinaryOperatorNode CreateOperatorNode(char c)
        {
            if (this.operators.ContainsKey(c))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[c]);
                if (operatorNodeObject is BinaryOperatorNode)
                {
                    return (BinaryOperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandeled operator");
        }
    }
}
