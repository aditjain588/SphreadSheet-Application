// <copyright file="BinaryOperatorNode.cs" company="Adit Jain">
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
        /// Node for binary operator.
        /// </summary>
    public abstract class BinaryOperatorNode : ExpressionTreeNode
        {
            /// <summary>
            /// Operation to be performed.
            /// </summary>
            private char operation;
            private ExpressionTreeNode left;
            private ExpressionTreeNode right;

            /// <summary>
            /// Initializes a new instance of the <see cref="BinaryOperatorNode"/> class.
            /// </summary>
            /// <param name="op">Operation to be performed. </param>
            public BinaryOperatorNode(char op)
            {
                this.operation = op;
                this.left = null;
                this.right = null;
            }

            /// <summary>
            /// Gets or sets the left node.
            /// </summary>
            public ExpressionTreeNode Left
            {
                get { return this.left; }
                set { this.left = value; }
            }

            /// <summary>
            /// Gets or sets the right node.
            /// </summary>
            public ExpressionTreeNode Right
            {
                get { return this.right; }
                set { this.right = value; }
            }

        /// <summary>
        /// Gets or sets determines the order of operations applied to the expression.
        /// </summary>
            public abstract ushort Precedence { get; set; }

        /// <summary>
        /// Evaluates the node.
        /// </summary>
        /// <returns>Evaluated expression. </returns>
            public abstract override double Evaluate();
        }
}
