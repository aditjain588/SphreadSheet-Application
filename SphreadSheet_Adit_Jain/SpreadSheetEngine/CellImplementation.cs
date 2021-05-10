// <copyright file="CellImplementation.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class to implement a single cell.
    /// </summary>
    public class CellImplementation : SphreadsheetCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellImplementation"/> class.
        /// </summary>
        /// <param name="rows">Row index of a cell. </param>
        /// <param name="columns">Column index of a cell. </param>
        /// <param name="t">Text in a cell. </param>
        public CellImplementation(int rows, int columns, string t)
        {
            this.rowIndex = rows;
            this.columnIndex = columns;
            this.text = t;
            this.value = this.text;
        }
    }
}
