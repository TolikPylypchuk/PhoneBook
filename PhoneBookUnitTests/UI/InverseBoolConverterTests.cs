using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PhoneBook.DAL.Models;
using PhoneBook.UI;

namespace PhoneBook.UnitTests.UI

{
    [TestClass]
    public class InverseBoolConverterTests
    {
        
        InverseBoolConverter inverseBoolConverter =
            new InverseBoolConverter();

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ConvertFromNullTest()
        {
            inverseBoolConverter.Convert(null, typeof(bool), null, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ConvertToNullTest()
        {
            inverseBoolConverter.Convert(typeof(bool), null, null, null);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ConvertWrongDestinationTest()
        {
            inverseBoolConverter.Convert(typeof(bool), typeof(string), null, null);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ConvertWrongSourceTest()
        {
            inverseBoolConverter.Convert(typeof(string), typeof(bool), null, null);
        }

        [TestMethod]
        public void ConvertTest()
        {
            Assert.AreEqual(
                inverseBoolConverter.Convert(true, typeof(bool), null, null),
                false);
        }

        [TestMethod]
        public void BackConvertTest()
        {
            Assert.AreEqual(
                inverseBoolConverter.ConvertBack(false, typeof(bool), null, null),
                true);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ConvertBackFromNullTest()
        {
            inverseBoolConverter.ConvertBack(null, typeof(bool), null, null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void ConvertBackToNullTest()
        {
            inverseBoolConverter.ConvertBack(typeof(bool), null, null, null);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ConvertBackWrongDestinationTest()
        {
            inverseBoolConverter.ConvertBack(typeof(bool), typeof(string), null, null);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ConvertBackWrongSourceTest()
        {
            inverseBoolConverter.ConvertBack(typeof(string), typeof(bool), null, null);
        }
    }
}
