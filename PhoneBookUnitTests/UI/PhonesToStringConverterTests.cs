using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PhoneBook.DAL.Models;
using PhoneBook.UI;

namespace PhoneBook.UnitTests.UI
{
	[TestClass]
	public class PhonesToStringConverterTests
	{
		PhonesToStringConverter converter =
			new PhonesToStringConverter();

		[TestMethod]
		public void Convert()
		{
			var phones = new[]
			{
				new UserPhone { Number = "111" },
				new UserPhone { Number = "222" }
			};

			string convertedPhones = this.converter.Convert(
				phones, typeof(string), null, null) as string;

			Assert.AreEqual(
				$"111{Environment.NewLine}222",
				convertedPhones);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertFromNull()
		{
			this.converter.Convert(null, typeof(string), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertToNull()
		{
			this.converter.Convert(
				new[] { new UserPhone() }, null, null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertWrongSourceType()
		{
			this.converter.Convert(new object(), typeof(string), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ConvertWrongDestinationType()
		{
			this.converter.Convert(
				new[] { new UserPhone() }, typeof(object), null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void ConvertBack()
		{
			this.converter.ConvertBack(null, null, null, null);
		}
	}
}
