// <copyright file="ColorChanges.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Cpts321;

    /// <summary>
    /// Class for implementing undo and redo for background color change of a cell.
    /// </summary>
    public class ColorChanges : IChanges
    {
        private uint newColor;
        private List<uint> oldColor;
        private List<SphreadsheetCell> cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChanges"/> class.
        /// </summary>
        /// <param name="cells">Cells whose color was changed. </param>
        /// <param name="oldColor">Before color of cells. </param>
        /// <param name="newColor">After color of cells. </param>
        public ColorChanges(List<SphreadsheetCell> cells, List<uint> oldColor, uint newColor)
        {
            this.oldColor = oldColor;
            this.newColor = newColor;
            this.cells = cells;
        }

        /// <summary>
        /// Redo function execution.
        /// </summary>
        public void ExecuteRedo()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].BgColor = this.newColor;
            }
        }

        /// <summary>
        /// Undo function execution.
        /// </summary>
        public void ExecuteUndo()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].BgColor = this.oldColor[i];
            }
        }
    }
}
