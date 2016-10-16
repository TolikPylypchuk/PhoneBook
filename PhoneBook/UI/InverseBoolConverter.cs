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
			if (targetType != typeof(bool))
			{
				throw new InvalidOperationException(
					"The target must be a bool.");
			}

			return !(bool)value;
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
