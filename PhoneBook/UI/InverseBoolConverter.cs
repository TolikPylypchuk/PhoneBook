using System;
using System.Globalization;
using System.Windows.Data;

namespace PhoneBook.UI
{
	[ValueConversion(typeof(bool), typeof(bool))]
	class InverseBoolConverter : IValueConverter
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

			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException(
					"Can only convert to bool.");
			}

			bool actualValue;

			try
			{
				actualValue = (bool)value;
			} catch (InvalidCastException)
			{
				throw new InvalidOperationException(
					"Can only convert from bool.");
			}

			return !actualValue;
		}

		public object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			return this.Convert(value, targetType, parameter, culture);
		}
	}
}
