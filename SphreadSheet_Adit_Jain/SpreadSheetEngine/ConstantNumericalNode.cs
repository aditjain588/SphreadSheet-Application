// <copyright file="ConstantNumericalNode.cs" company="Adit Jain">
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
        /// Node for constant value.
        /// </summary>
        public class ConstantNumericalNode : ExpressionTreeNode
        {
            private double value;

            /// <summary>
            /// Initializes a new instance of the <see cref="ConstantNumericalNode"/> class.
            /// </summary>
            /// <param name="val">Value of the node. </param>
            public ConstantNumericalNode(double val)
            {
                this.value = val;
            }

            /// <summary>
            /// Evaluates the node.
            /// </summary>
            /// <returns>Value of node. </returns>
            public override double Evaluate()
            {
                return this.value;
            }
        }
}
