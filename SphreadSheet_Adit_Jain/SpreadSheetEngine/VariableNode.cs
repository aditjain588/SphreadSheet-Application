// <copyright file="VariableNode.cs" company="Adit Jain">
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
    /// Node for variable.
    /// </summary>
    public class VariableNode : ExpressionTreeNode
    {
        private readonly Dictionary<string, double> variables;
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="nam">Name of variable. </param>
        /// <param name="variables">Dictionary for variable name and variable value. </param>
        public VariableNode(string nam, ref Dictionary<string, double> variables)
        {
            this.name = nam;
            this.variables = variables;
        }

        /// <summary>
        /// gets or sets value of value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Lookup value from dict table.
        /// </summary>
        /// <returns>the value of the node.</returns>
        public override double Evaluate()
        {
            double value = 0.0;
            if (this.variables.ContainsKey(this.name))
            {
                value = this.variables[this.name];
            }

            return value;
        }
    }
}