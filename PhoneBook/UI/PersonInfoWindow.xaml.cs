using System.Windows;

using PhoneBook.DAL.Models;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for PersonInfoWindow.xaml
	/// </summary>
	public partial class PersonInfoWindow : Window
	{
		public static readonly DependencyProperty IsReadOnlyProperty =
			DependencyProperty.Register(
				nameof(IsReadOnly),
				typeof(bool),
				typeof(PersonInfoWindow));

		public static readonly DependencyProperty PersonProperty =
			DependencyProperty.Register(
				nameof(Person),
				typeof(User),
				typeof(PersonInfoWindow));

		public PersonInfoWindow()
        {
            this.InitializeComponent();

            if (!IsReadOnly)
            {
                this.OKButton.Visibility = Visibility.Collapsed;
                this.CancelButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.OKButton.Visibility = Visibility.Visible;
                this.CancelButton.Visibility = Visibility.Visible;
            }

        }

		public bool IsReadOnly
		{
			get { return (bool)this.GetValue(IsReadOnlyProperty); }
			set { this.SetValue(IsReadOnlyProperty, value); }
		}

		public User Person
		{
			get { return (User)this.GetValue(PersonProperty); }
			set { this.SetValue(PersonProperty, value); }
		}

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
