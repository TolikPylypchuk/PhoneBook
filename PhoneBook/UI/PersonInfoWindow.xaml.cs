using System.Windows;

using PhoneBook.DAL.Models;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for PersonInfoWindow.xaml
	/// </summary>
	public partial class PersonInfoWindow : Window
	{
		public static readonly DependencyProperty PersonProperty =
			DependencyProperty.Register(
				nameof(Person),
				typeof(User),
				typeof(PersonInfoWindow));

		public PersonInfoWindow()
		{
			this.InitializeComponent();
		}

		public User Person
		{
			get { return (User)this.GetValue(PersonProperty); }
			set { this.SetValue(PersonProperty, value); }
		}
	}
}
