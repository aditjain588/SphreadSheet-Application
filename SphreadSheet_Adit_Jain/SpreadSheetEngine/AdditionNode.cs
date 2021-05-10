// <copyright file="AdditionNode.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Node for addition.
    /// </summary>
    public class AdditionNode : BinaryOperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        public AdditionNode()
            : base('+')
        {
        }

        /// <summary>
        /// Gets or sets precedence for addition.
        /// </summary>
        public override ushort Precedence { get; set; } = 2;

        /// <summary>
        /// Adds the right child to the left child.
        /// </summary>
        /// <returns>double result of evaluation.</returns>
        public override double Evaluate()
        {
            try
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
            catch (Exception)
            {
                Console.WriteLine("---Error applying operator to children---");
                throw new Exception("Left or Right child was not a constant node or Value was not set.");
            }
        }
    }
}
