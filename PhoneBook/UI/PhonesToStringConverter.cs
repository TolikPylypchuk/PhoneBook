using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

using PhoneBook.DAL.Models;

namespace PhoneBook.UI
{
	[ValueConversion(typeof(IEnumerable<PhoneBase>), typeof(string))]
	public class PhonesToStringConverter : IValueConverter
	{
		public object Convert(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			var phones = value as IEnumerable<PhoneBase>;

			if (phones == null || targetType != typeof(string))
			{
				return null;
			}

			StringBuilder builder = new StringBuilder();
			phones.Aggregate(
				builder,
				(b, phone) => b.Append(phone.Number)
							   .Append(Environment.NewLine));

			return builder.Remove(
				builder.Length - Environment.NewLine.Length,
				Environment.NewLine.Length).ToString();
		}

		public object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			throw new NotSupportedException(
				"Converting from string to a collection " +
				"of phones is not supported.");
		}
	}
}
