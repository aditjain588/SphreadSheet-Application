// <copyright file="SphreadsheetCell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Cpts321;

    /// <summary>
    /// Implement a cell in spreadsheet application.
    /// </summary>
    public abstract class SphreadsheetCell : INotifyPropertyChanged
    {
        /// <summary>
        /// Fields:
        ///     value: value of a cell.
        ///     rowIndex: row index of a cell.
        ///     columnIndex: column index of a cell.
        ///     text: text in a cell.
        ///     callName: name of cell(for example A1)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected string value;
        protected int rowIndex;
        protected int columnIndex;
        protected string text;
        protected string cellName;
        protected ExpressionTree tree;
        protected uint bgColor = 0xFFFFFFFF;

        /// <summary>
        /// Initializes a new instance of the <see cref="SphreadsheetCell"/> class.
        /// </summary>
        public SphreadsheetCell()
        {
        }

        /// <summary>
        /// Gets get value of rowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets get value of columnIndex.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets or sets value of text, sets it equal to value if not text not equal to value.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets the value of name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Name;
            }
        }

        /// <summary>
        /// Gets value of Value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            internal set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        /// <summary>
        /// Gets or sets value of background color.
        /// </summary>
        public uint BgColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor != value)
                {
                    this.bgColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("BgColor"));
                }
            }
        }
    }
}
