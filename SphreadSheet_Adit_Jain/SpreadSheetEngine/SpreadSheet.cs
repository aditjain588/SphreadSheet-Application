// <copyright file="SpreadSheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Xml;
    using Cpts321;

    /// <summary>
    /// Class to implement speradsheet application.
    /// </summary>
    public class SpreadSheet
    {
        public SphreadsheetCell[,] cellArray;
        private Dictionary<SphreadsheetCell, List<SphreadsheetCell>> dependencies;
        private Stack<IChanges> undoStack;
        private Stack<IChanges> redoStack;

        // Property change event.
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public int columnCount;

        public int rowCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheet"/> class.
        /// </summary>
        /// <param name="numRows">Total number of rows. </param>
        /// <param name="numColumns">Total number of columns.</param>
        public SpreadSheet(int numRows, int numColumns)
        {
            this.columnCount = numColumns;
            this.rowCount = numRows;
            this.dependencies = new Dictionary<SphreadsheetCell, List<SphreadsheetCell>>();
            this.undoStack = new Stack<IChanges>();
            this.redoStack = new Stack<IChanges>();
            this.cellArray = new SphreadsheetCell[numRows, numColumns];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    this.cellArray[i, j] = new CellImplementation(i, j, string.Empty);
                    this.cellArray[i, j].PropertyChanged += this.OnPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Returns particular cell.
        /// </summary>
        /// <param name="rowNum">Row number. </param>
        /// <param name="colNum">Column number.</param>
        /// <returns>Cell at [row number, column number]. </returns>
        public SphreadsheetCell GetCell(int rowNum, int colNum)
        {
            if (rowNum > this.cellArray.GetLength(0) || colNum > this.cellArray.GetLength(1))
            {
                return null;
            }
            else
            {
                return this.cellArray[rowNum, colNum];
            }
        }

        /// <summary>
        /// Returns a particular cell.
        /// </summary>
        /// <param name="cellName">Name of cell like "A1". </param>
        /// <returns>A cell with name cellName. </returns>
        public SphreadsheetCell GetCell(string cellName)
        {
            int cellColumn = cellName[0] - 'A';
            int cellRow = Convert.ToInt32(cellName.Substring(1)) - 1;
            return this.GetCell(cellRow, cellColumn);
        }

        /// <summary>
        /// Gets total number of rows.
        /// </summary>
        /// <returns>Number of rows. </returns>
        public int RowCount()
        {
            return this.cellArray.GetLength(0);
        }

        /// <summary>
        /// Gets total number of columns.
        /// </summary>
        /// <returns>Number of columns. </returns>
        public int ColumnCount()
        {
            return this.cellArray.GetLength(1);
        }

        /// <summary>
        /// Gets triggered if cell value is changed.
        /// </summary>
        /// <param name="sender">Sender. </param>
        /// <param name="e">e. </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                // if first character is not "=", then just put the value of cell as is.
                if (!((SphreadsheetCell)sender).Text.StartsWith("="))
                {
                    ((SphreadsheetCell)sender).Value = ((SphreadsheetCell)sender).Text;
                }

                // if first character is "=", then evaluate it.
                else
                {
                    try
                    {
                        // if something like "=A1" is entered, then simply copy value from A1 to current cell.
                        string formula = ((SphreadsheetCell)sender).Text.Substring(1);
                        int column = Convert.ToInt16(formula[0]) - 'A';
                        int row = Convert.ToInt16(formula.Substring(1)) - 1;
                        ((SphreadsheetCell)sender).Value = this.GetCell(row, column).Value;
                    }
                    catch (Exception ex)
                    {
                        // Expresion is entered, so expression tree class is used for evaluation.
                        string formula = ((SphreadsheetCell)sender).Text.Substring(1);
                        ExpressionTree tree = new ExpressionTree(formula);
                        string[] variables = tree.GetVariableNames();

                        // check for bad reference
                        if (!this.CheckBadReference(variables))
                        {
                            ((SphreadsheetCell)sender).Text = "!(bad reference)";
                        }

                        // check for self references.
                        else if (!this.CheckSelfReference((SphreadsheetCell)sender, variables))
                        {
                            ((SphreadsheetCell)sender).Text = "!(self reference)";
                        }

                        // no exceptions found.
                        else
                        {
                            foreach (string item in variables)
                            {
                                tree.SetVariable(item, Convert.ToDouble(this.GetCell(item).Value));
                            }

                            ((SphreadsheetCell)sender).Value = tree.Evaluate().ToString();
                        }
                    }
                }

                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Text"));
            }
            else if (e.PropertyName == "BgColor")
            {
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("BgColor"));
            }
            else if (e.PropertyName == "Value")
            {
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
            }

            /*if (e.PropertyName == "Undo empty")
            {
                this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Undo empty"));
            }*/
        }

        /// <summary>
        /// Adds the new change to undo stack.
        /// </summary>
        /// <param name="change">New change that occured. </param>
        public void AddUndo(IChanges change)
        {
            this.undoStack.Push(change);
        }

        /// <summary>
        /// Undo's the last action performed.
        /// </summary>
        public void Undo()
        {
            this.undoStack.Peek().ExecuteUndo();
            this.redoStack.Push(this.undoStack.Pop());
            this.PropertyChanged?.Invoke(this.redoStack.Peek(), new PropertyChangedEventArgs("RedoNotEmpty"));
            if (this.undoStack.Count == 0)
            {
                this.PropertyChanged?.Invoke(this.redoStack.Peek(), new PropertyChangedEventArgs("UndoEmpty"));
            }
            else
            {
                this.PropertyChanged?.Invoke(this.redoStack.Peek(), new PropertyChangedEventArgs("UndoNotEmpty"));
            }
        }

        /// <summary>
        /// Redo's the last action performed.
        /// </summary>
        public void Redo()
        {
            this.redoStack.Peek().ExecuteRedo();
            this.undoStack.Push(this.redoStack.Pop());
            this.PropertyChanged?.Invoke(this.undoStack.Peek(), new PropertyChangedEventArgs("UndoNotEmpty"));
            if (this.redoStack.Count == 0)
            {
                this.PropertyChanged?.Invoke(this.undoStack.Peek(), new PropertyChangedEventArgs("RedoEmpty"));
            }
            else
            {
                this.PropertyChanged?.Invoke(this.undoStack.Peek(), new PropertyChangedEventArgs("RedoNotEmpty"));
            }
        }

        /// <summary>
        /// Saves the contents of sphreadsheet to xml file.
        /// </summary>
        /// <param name="stream">filename. </param>
        public void Save(Stream stream)
        {
            XmlWriter xmlWriter = XmlWriter.Create(stream);
            xmlWriter.WriteStartElement("spreadsheet");
            xmlWriter.WriteAttributeString("columns", this.columnCount.ToString());
            xmlWriter.WriteAttributeString("rows", this.rowCount.ToString());

            foreach (SphreadsheetCell cell in this.cellArray)
            {
                if (cell.Text != string.Empty || cell.BgColor != 0xFFFFFFFF)
                {
                    string location = (char)(cell.ColumnIndex + 'A') + (cell.RowIndex + 1).ToString();
                    xmlWriter.WriteStartElement("cell");
                    xmlWriter.WriteAttributeString("name", location);
                    xmlWriter.WriteElementString("bgcolor", cell.BgColor.ToString());
                    xmlWriter.WriteElementString("text", cell.Text);
                    xmlWriter.WriteEndElement();
                }
            }

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }

        /// <summary>
        /// Loads the contents of sphreadsheet to xml file.
        /// </summary>
        /// <param name="stream">filename. </param>
        public void Load(Stream stream)
        {
            // setting call value and background color to default.
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    this.cellArray[i, j].Value = null;
                    this.cellArray[i, j].BgColor = 0xFFFFFFFF;
                }
            }

            int numCols = 26, numRows = 50;
            XmlDocument document = new XmlDocument();
            document.Load(stream);

            foreach (XmlNode node in document.DocumentElement.Attributes)
            {
                switch (node.Name)
                {
                    case "columns":
                        numCols = int.Parse(node.Value);
                        break;
                    case "rows":
                        numRows = int.Parse(node.Value);
                        break;
                }
            }

            // setting all the initial values of sphreadsheet, as they were set in the constructor.
            this.columnCount = numCols;
            this.rowCount = numRows;
            this.undoStack.Clear();
            this.redoStack.Clear();
            this.cellArray = new SphreadsheetCell[numRows, numCols];
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    this.cellArray[i, j] = new CellImplementation(i, j, string.Empty);
                    this.cellArray[i, j].PropertyChanged += this.OnPropertyChanged;
                }
            }

            SphreadsheetCell cell = this.GetCell("A1");

            // updating the name and background color.
            foreach (XmlNode node in document.DocumentElement)
            {
                if (node.Name == "cell")
                {
                    // get the name.
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (attribute.Name == "name")
                        {
                            cell = this.GetCell(attribute.Value);
                            break;
                        }
                    }

                    // get Background color or text.
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        switch (child.Name)
                        {
                            case "bgcolor":
                                cell.BgColor = uint.Parse(child.InnerText);
                                break;
                            case "text":
                                cell.Text = child.InnerText;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks for bad references.
        /// </summary>
        /// <param name="variables">List of all variables. </param>
        /// <returns>true or false. </returns>
        private bool CheckBadReference(string[] variables)
        {
            bool badRef = true;
            foreach (string item in variables)
            {
                int col = item[0] - 'A';
                if (col < 0 || col > 25)
                {
                    badRef = false;
                }

                int row;

                // if row number is an integer, then check if it is out of bounds for example "+=A12324"
                if (int.TryParse(item.Substring(1), out row))
                {
                    // check row boundaries
                    if ((row - 1) < 0 || (row - 1) > 50)
                    {
                        badRef = false;
                    }
                }

                // if row number is not an integer for example "=Ab".
                else
                {
                    badRef = false;
                }

                // if anything is invalid then we return false.
                if (!badRef)
                {
                    break;
                }
            }

            return badRef;
        }

        /// <summary>
        /// Checks for self references.
        /// </summary>
        /// <param name="self">cell. </param>
        /// <param name="variables">List of variables. </param>
        /// <returns>true or false. </returns>
        private bool CheckSelfReference(SphreadsheetCell self, string[] variables)
        {
            bool selfRef = true;
            SphreadsheetCell cell;
            foreach (string item in variables)
            {
                int col = item[0] - 'A';
                int row;

                // checking for all cells in variables list.
                if (int.TryParse(item.Substring(1), out row))
                {
                    cell = this.GetCell(row - 1, col);

                    // if rowindex and columnindex matches, means self referencing.
                    if (cell.RowIndex == self.RowIndex && cell.ColumnIndex == self.ColumnIndex)
                    {
                        selfRef = false;
                    }
                }

                if (!selfRef)
                {
                    break;
                }
            }

            return selfRef;
        }

        /// <summary>
        /// Performs demo test of spreadsheet application.
        /// </summary>
        public void Demo()
        {
            int i = 0;
            Random rand = new Random();

            while (i < 50)
            {
                int randomCol = rand.Next(0, 25);
                int randomRow = rand.Next(0, 49);

                SphreadsheetCell val = this.GetCell(randomRow, randomCol);
                val.Text = "Hello world!!!";
                this.cellArray[randomRow, randomCol] = val;
                i++;
            }

            for (i = 0; i < 50; i++)
            {
                this.cellArray[i, 1].Text = "This is cell B" + (i + 1);
            }

            for (i = 0; i < 50; i++)
            {
                this.cellArray[i, 0].Text = "=B" + (i + 1);
            }
        }
    }
}
