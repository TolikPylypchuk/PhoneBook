using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PhoneBook.UI;

namespace PhoneBook.UnitTests.UI
{
	[TestClass]
	public class InverseBoolConverterTests
	{
		InverseBoolConverter inverseBoolConverter =
			new InverseBoolConverter();

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertFromNullTest()
		{
			this.inverseBoolConverter.Convert(null, typeof(bool), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertToNullTest()
		{
			this.inverseBoolConverter.Convert(true, null, null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertWrongDestinationTest()
		{
			this.inverseBoolConverter.Convert(
				true, typeof(string), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertWrongSourceTest()
		{
			this.inverseBoolConverter.Convert(
				new object(), typeof(bool), null, null);
		}

		[TestMethod]
		public void ConvertTest()
		{
			Assert.AreEqual(
				this.inverseBoolConverter.Convert(
					true, typeof(bool), null, null),
				false);
		}

		[TestMethod]
		public void ConvertBackTest()
		{
			Assert.AreEqual(
				this.inverseBoolConverter.ConvertBack(
					false, typeof(bool), null, null),
				true);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertBackFromNullTest()
		{
			this.inverseBoolConverter.ConvertBack(
				null, typeof(bool), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertBackToNullTest()
		{
			this.inverseBoolConverter.ConvertBack(
				true, null, null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertBackWrongDestinationTest()
		{
			this.inverseBoolConverter.ConvertBack(
				true, typeof(string), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertBackWrongSourceTest()
		{
			this.inverseBoolConverter.ConvertBack(
				new object(), typeof(bool), null, null);
		}
	}
}
