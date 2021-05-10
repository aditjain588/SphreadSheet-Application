// <copyright file="TextChanges.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Cpts321;

    /// <summary>
    /// Class for implementing undo and redo for text change of a cell.
    /// </summary>
    public class TextChanges : IChanges
    {
        private string oldText;
        private string newText;
        private SphreadsheetCell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChanges"/> class.
        /// </summary>
        /// <param name="oldText">Previous value of cell. </param>
        /// <param name="newText">New value of cell. </param>
        /// <param name="cell">Particular cell. </param>
        public TextChanges(string oldText, string newText, SphreadsheetCell cell)
        {
            this.oldText = oldText;
            this.newText = newText;
            this.cell = cell;
        }

        /// <summary>
        /// Redo function execution.
        /// </summary>
        public void ExecuteRedo()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Undo function execution.
        /// </summary>
        public void ExecuteUndo()
        {
            this.cell.Text = this.oldText;
        }
    }
}
