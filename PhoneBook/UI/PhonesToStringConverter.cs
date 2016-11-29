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
			if (value == null)
			{
				throw new ArgumentNullException(
					nameof(value),
					"The value cannot be null.");
			}

			if (targetType == null)
			{
				throw new ArgumentNullException(
					nameof(targetType),
					"The target type cannot be null.");
			}

			var phones = value as IEnumerable<PhoneBase>;

			if (phones == null || targetType != typeof(string))
			{
				throw new InvalidOperationException(
					"Can only convert IEnumerable<PhoneBase> to string.");
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
				"Converting from string to phones is not supported.");
		}
	}
}
