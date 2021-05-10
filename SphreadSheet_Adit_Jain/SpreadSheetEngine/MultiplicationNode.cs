// <copyright file="MultiplicationNode.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Node for multiplication.
    /// </summary>
    public class MultiplicationNode : BinaryOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        public MultiplicationNode()
            : base('*')
        {
        }

        /// <summary>
        /// Gets or sets precedence for multiplication.
        /// </summary>
        public override ushort Precedence { get; set; } = 3;

        /// <summary>
        /// Multiplies the right child by the left child.
        /// </summary>
        /// <returns>double result of evaluation.</returns>
        public override double Evaluate()
        {
            try
            {
                return this.Right.Evaluate() * this.Left.Evaluate();
            }
            catch (Exception)
            {
                Console.WriteLine("---Error applying operator to children---");
                throw new Exception("Left or Right child was not a constant node or Value was not set.");
            }
        }
    }
}
