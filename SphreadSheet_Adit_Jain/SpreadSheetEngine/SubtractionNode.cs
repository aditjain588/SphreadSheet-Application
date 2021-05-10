// <copyright file="SubtractionNode.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Node for subtraction.
    /// </summary>
    public class SubtractionNode : BinaryOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        public SubtractionNode()
            : base('-')
        {
        }

        /// <summary>
        /// Gets or sets precedence for subtraction.
        /// </summary>
        public override ushort Precedence { get; set; } = 2;

        /// <summary>
        /// Subtracts the left child from the right child.
        /// </summary>
        /// <returns>double result of evaluation.</returns>
        public override double Evaluate()
        {
            try
            {
                return this.Right.Evaluate() - this.Left.Evaluate();
            }
            catch (Exception)
            {
                Console.WriteLine("---Error applying operator to children---");
                throw new Exception("Left or Right child was not a constant node or Value was not set.");
            }
        }
    }
}
