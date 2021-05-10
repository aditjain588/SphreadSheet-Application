// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;
    using SpreadSheetEngine;

    /// <summary>
    /// Form class for spreadsheet application.
    /// </summary>
    public partial class Form1 : Form
    {
        private SpreadSheet newSheet = new SpreadSheet(50, 26);

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Function is executed when the form loads, it initializes the ros an columns.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">e. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.newSheet.PropertyChanged += this.OnCellPropertyChanged;

            this.dataGridView1.Columns.Clear();
            for (char i = 'A'; i <= 'Z'; i++)
            {
                this.dataGridView1.Columns.Add(char.ToString(i), char.ToString(i));
            }

            this.dataGridView1.Rows.Add(50);
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }

        /// <summary>
        /// Function is executed when property change is triggered.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">event. </param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // if value is changed.
            if (e.PropertyName == "Value")
            {
                this.dataGridView1.Rows[((SphreadsheetCell)sender).RowIndex].Cells[((SphreadsheetCell)sender).ColumnIndex].Value = this.newSheet.cellArray[((SphreadsheetCell)sender).RowIndex, ((SphreadsheetCell)sender).ColumnIndex].Value;
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo Text Changes";
            }

            // if background color is changed.
            if (e.PropertyName == "BgColor")
            {
                this.dataGridView1.Rows[((SphreadsheetCell)sender).RowIndex].Cells[((SphreadsheetCell)sender).ColumnIndex].Style.BackColor = Color.FromArgb((int)this.newSheet.cellArray[((SphreadsheetCell)sender).RowIndex, ((SphreadsheetCell)sender).ColumnIndex].BgColor);
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo Background Color Changes";
            }

            // enable or disable redo and undo buttons based on stack counts.
            if (e.PropertyName == "UndoNotEmpty")
            {
                this.undoToolStripMenuItem.Enabled = true;
            }
            else if (e.PropertyName == "UndoEmpty")
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }

            if (e.PropertyName == "RedoNotEmpty")
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo Text Changes";
            }
            else if (e.PropertyName == "RedoEmpty")
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
            }
        }

        /// <summary>
        /// Function is executed when "Demo" button is clicked.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">e. </param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.newSheet.Demo();
        }

        /// <summary>
        /// Sets the data grid value to value of correspondong cell.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">e. </param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.newSheet.cellArray[e.RowIndex, e.ColumnIndex].Text;
        }

        /// <summary>
        /// Sets the data grid value to the value of corresponsing cell.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">e. </param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int cellRow = e.RowIndex;
            int cellColumn = e.ColumnIndex;
            string oldText = string.Empty;

            // get the actual cell
            SphreadsheetCell cellToUpdate = this.newSheet.GetCell(cellRow, cellColumn);

            if (cellToUpdate != null)
            {
                oldText = cellToUpdate.Text;
                cellToUpdate.Text = this.dataGridView1.Rows[cellRow].Cells[cellColumn].Value.ToString();
                this.dataGridView1.Rows[cellRow].Cells[cellColumn].Value = cellToUpdate.Value;
            }

            SphreadsheetCell cell = this.newSheet.GetCell(cellRow, cellColumn);
            TextChanges command = new TextChanges(oldText, cellToUpdate.Text, cell);
            this.newSheet.AddUndo(command);
        }

        /// <summary>
        /// Function is executed when "Change background color.." is clicked on "Cell" menu.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">e. </param>
        private void ChangeBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SphreadsheetCell> cells = new List<SphreadsheetCell>();
            List<uint> oldColors = new List<uint>();

            ColorDialog myDialog = new ColorDialog();
            myDialog.AllowFullOpen = true;
            myDialog.ShowHelp = true;
            myDialog.Color = this.dataGridView1.SelectedCells[0].Style.BackColor;
            if (myDialog.ShowDialog() == DialogResult.OK)
            {
                for (int index = 0; index < this.dataGridView1.SelectedCells.Count; index++)
                {
                    int rowIndex = this.dataGridView1.SelectedCells[index].RowIndex;
                    int columnIndex = this.dataGridView1.SelectedCells[index].ColumnIndex;
                    cells.Add(this.newSheet.GetCell(rowIndex, columnIndex));
                    oldColors.Add(this.newSheet.GetCell(rowIndex, columnIndex).BgColor);
                    this.newSheet.GetCell(rowIndex, columnIndex).BgColor = this.ColorToUInt(myDialog.Color);
                }

                ColorChanges command = new ColorChanges(cells, oldColors, this.ColorToUInt(myDialog.Color));
                this.newSheet.AddUndo(command);
            }
        }

        /// <summary>
        /// Convert from color to uint.
        /// </summary>
        /// <param name="color">color. </param>
        /// <returns>uint color. </returns>
        private uint ColorToUInt(Color color)
        {
            // http://www.vcskicks.com/color-uint.php
            return (uint)((color.A << 24)
                            | (color.R << 16)
                            | (color.G << 8)
                            | (color.B << 0));
        }

        /// <summary>
        /// Function is executed when "Undo" is clicked on "Edit" menu.
        /// </summary>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.newSheet.Undo();
        }

        /// <summary>
        /// Function is executed when "Redo" is clicked on "Edit" menu.
        /// </summary>
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.newSheet.Redo();
        }

        /// <summary>
        /// Function is executed when "Save" is clicked on "File" menu.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">event. </param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml";
            saveFileDialog1.Title = "Save an XML File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != string.Empty)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                this.newSheet.Save(fs);
                fs.Close();
            }
        }

        /// <summary>
        /// Function is executed when "Load" is clicked on "File" menu.
        /// </summary>
        /// <param name="sender">sender. </param>
        /// <param name="e">event. </param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml";
            openFileDialog1.Title = "Open an XML File";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != string.Empty)
            {
                System.IO.FileStream fs = (System.IO.FileStream)openFileDialog1.OpenFile();
                this.newSheet.Load(fs);
                fs.Close();
            }
        }
    }
}
