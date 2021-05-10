// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
namespace SphreadSheetTests
{
    using System.Collections;
    using System.Collections.Generic;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Class contains tests for expression tree.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Test for Evaluate method in ExpressionTree, for an empty string.
        /// </summary>
        [Test]
        public void NullTest()
        {
            ExpressionTree tree = new ExpressionTree(" ");
            Assert.That(tree.Evaluate(), Is.EqualTo(0));
        }

        /// <summary>
        /// Test Test for Evaluate method in ExpressionTree, if extra paranthesis are added.
        /// </summary>
        [Test]
        public void ExtraParanthesisTest()
        {
            ExpressionTree tree = new ExpressionTree("(((5*2)))");
            Assert.That(tree.Evaluate(), Is.EqualTo(10));
        }

        /// <summary>
        /// Test Test for Evaluate method in ExpressionTree, if single digit is passed.
        /// </summary>
        [Test]
        public void SingleDigitTest()
        {
            ExpressionTree tree = new ExpressionTree("100");
            Assert.That(tree.Evaluate(), Is.EqualTo(100));
        }

        /// <summary>
        /// Test Test for Evaluate method in ExpressionTree, if multiple paranthesis with multiple expression is passed.
        /// </summary>
        [Test]
        public void ExtraParanthesisExpressionTest()
        {
            ExpressionTree tree = new ExpressionTree("((((2+6)*(7-3))))");
            Assert.That(tree.Evaluate(), Is.EqualTo(32));
        }
    }
}
