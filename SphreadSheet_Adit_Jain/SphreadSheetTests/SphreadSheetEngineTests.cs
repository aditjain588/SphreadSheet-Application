// <copyright file="SphreadSheetEngineTests.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SphreadSheetTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SpreadSheetEngine;

    /// <summary>
    /// Class for testing sphread sheet engine class methods.
    /// </summary>
    [TestFixture]
    internal class SphreadSheetEngineTests
    {
       /* /// <summary>
        /// General test for cell update.
        /// </summary>
        [Test]
        public void CellUpdateGeneralTest()
        {
            SpreadSheet sheet = new SpreadSheet(2, 2);
            var cell1 = sheet.GetCell(1, 1);
            cell1.Text = "Hello";
            Assert.AreEqual("Hello", cell1.Text);
        }

        /// <summary>
        /// General test for reference cell update.
        /// </summary>
        [Test]
        public void CellReferenceUpdateGeneralTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell(1, 1);
            var cell2 = sheet.GetCell(2, 2);
            cell1.Text = "100";
            cell2.Text = "=A1";
            Assert.AreEqual(cell1.Value, cell2.Value);
        }

        /// <summary>
        /// Null test for reference cell update.
        /// </summary>
        [Test]
        public void CellReferenceUpdateNullTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell(1, 1);
            var cell2 = sheet.GetCell(2, 2);
            cell1.Text = "";
            cell2.Text = "=A1";
            Assert.AreEqual(cell1.Value, cell2.Value);
        }

        /// <summary>
        /// Expression evaluate test for reference cell update.
        /// </summary>
        [Test]
        public void CellReferenceUpdateExpTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell(1, 1);
            cell1.Text = "=4*10";
            Assert.AreEqual("40", cell1.Value);
        }

        /// <summary>
        /// Expression evaluate test with extra paranthesis test for reference cell update.
        /// </summary>
        [Test]
        public void CellReferenceUpdateExpParanthesisTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell(1, 1);
            cell1.Text = "=((((4*10)))+(((5+7))))";
            Assert.AreEqual("52", cell1.Value);
        }

        /// <summary>
        /// General test for cell reference.
        /// </summary>
        [Test]
        public void CellReferenceTest()
        {
            SpreadSheet sheet = new SpreadSheet(5, 5);
            var cell1 = sheet.GetCell("A1");
            var cell2 = sheet.GetCell("A2");
            cell1.Text = "100";
            cell2.Text = "=A1+20";
            Assert.AreEqual("120", cell2.Value);
        }

        /// <summary>
        /// General test for cell reference.
        /// </summary>
        [Test]
        public void CellReferenceUpdateTest()
        {
            SpreadSheet sheet = new SpreadSheet(5, 5);
            var cell1 = sheet.GetCell("A1");
            var cell2 = sheet.GetCell("A2");
            cell1.Text = "100";
            cell2.Text = "=A1+20";
            cell1.Text = "200";
            Assert.AreEqual("220", cell2.Value);
        }

        /// <summary>
        /// General test for undo function.
        /// </summary>
        [Test]
        public void UndoTest()
        {
            SpreadSheet sheet = new SpreadSheet(5, 5);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "100";
            sheet.Undo();
            Assert.AreEqual(cell1.Value, string.Empty);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void UndoTestSecond()
        {
            SpreadSheet sheet = new SpreadSheet(5, 5);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "100";
            var cell2 = sheet.GetCell("B1");
            cell2.Text = "=A1";
            sheet.Undo();
            Assert.AreEqual(cell2.Value, string.Empty);
            sheet.Undo();
            Assert.AreEqual(cell1.Value, string.Empty);
        }

        /// <summary>
        /// If nothing to undo, then there should not be any change.
        /// </summary>
        [Test]
        public void UndoNullTest()
        {
            SpreadSheet sheet = new SpreadSheet(2, 2);
            sheet.Undo();

            // going through entire sphreadsheet and making sure that all fields are empty.
            for (int i = 0; i < sheet.cellArray.Length; i++)
            {
                for (int j = 0; j < sheet.cellArray.Length; j++)
                {
                    Assert.AreEqual(sheet.GetCell(i, j), string.Empty);
                }
            }
        }

        /// <summary>
        /// General test for Redo.
        /// </summary>
        [Test]
        public void RedoTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "100";
            sheet.Undo();
            sheet.Redo();
            Assert.AreEqual(cell1.Value, "100");
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void RedoTestSecond()
        {
            SpreadSheet sheet = new SpreadSheet(5, 5);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "100";
            var cell2 = sheet.GetCell("B1");
            cell2.Text = "=A1+100";
            sheet.Undo();
            sheet.Redo();
            Assert.AreEqual(cell2.Value, "100");
            sheet.Undo();
            sheet.Redo();
            Assert.AreEqual(cell2.Value, "200");
        }

        /// <summary>
        /// If nothing to Redo, then there should not be any change.
        /// </summary>
        [Test]
        public void RedoNullTest()
        {
            SpreadSheet sheet = new SpreadSheet(2, 2);
            sheet.Redo();

            // going through entire sphreadsheet and making sure that all fields are empty.
            for (int i = 0; i < sheet.cellArray.Length; i++)
            {
                for (int j = 0; j < sheet.cellArray.Length; j++)
                {
                    Assert.AreEqual(sheet.GetCell(i, j), string.Empty);
                }
            }
        }
       */

        /// <summary>
        /// General test for bad reference cell value.
        /// </summary>
        [Test]
        public void BadReferenceGeneralTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=6+Cell*27";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// Test for bad reference.
        /// </summary>
        [Test]
        public void BadReferenceEmptyCellTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=Aa";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// Test for bad reference when out of range cell value is entered.
        /// </summary>
        [Test]
        public void BadReferenceOutOfRangeCellTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=A1112";
            Assert.AreEqual(cell1.Value, "!(bad reference)");
        }

        /// <summary>
        /// General test for self reference.
        /// </summary>
        [Test]
        public void SelfReferenceGeneralTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=A1+5";
            Assert.AreEqual(cell1.Value, "!(self reference)");
        }

        /// <summary>
        /// Test for self reference.
        /// </summary>
        [Test]
        public void SelfReferenceNullTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            cell1.Text = "=(100+(5/A1))*2";
            Assert.AreEqual(cell1.Value, "!(self reference)");
        }

        /// <summary>
        /// General test for circular reference.
        /// </summary>
        [Test]
        public void CircularReferenceGeneralTest()
        {
            SpreadSheet sheet = new SpreadSheet(3, 3);
            var cell1 = sheet.GetCell("A1");
            var cell2 = sheet.GetCell("B1");
            cell1.Text = "100";
            cell2.Text = "50";
            cell1.Text = "=100+B1";
            cell2.Text = "=200-A1";
            Assert.AreEqual(cell2.Value, "!(circular reference)");
        }
    }
}
