using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhoneBook.UI
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BoolVisibilityConverter : IValueConverter
	{
		public object Convert(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			Visibility rv = Visibility.Visible;
			try
			{
				var x = bool.Parse(value.ToString());
				if (x)
				{
					rv = Visibility.Visible;
				}
				else
				{
					rv = Visibility.Collapsed;
				}
			}
			catch (Exception)
			{
				throw new InvalidOperationException(
					"The value must be a bool.");
			}
			return rv;
		}

		public object ConvertBack(
			object value,
			Type targetType,
			object parameter,
			CultureInfo culture)
		{
			switch(value.ToString())
			{
				case "Visible":
					return 1;
				case "Hidden":
				case "Collapsed":
					return 0;
				default:
					throw new ArgumentException(
					"The value must be a Visibility.");
			}
		}

	}
}
