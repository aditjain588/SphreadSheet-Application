// <copyright file="ExpressionTreeNode.cs" company="Adit Jain">
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
    /// Class for implementing node for expression tree.
    /// </summary>
    public abstract class ExpressionTreeNode
        {
            /// <summary>
            /// Method to evaluate the expression of a tree.
            /// </summary>
            /// <returns>Evaluated result. </returns>
            public abstract double Evaluate();
        }
}
