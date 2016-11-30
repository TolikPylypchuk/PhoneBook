using System.Windows;

using PhoneBook.Services;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for SignInWindow.xaml
	/// </summary>
	public partial class SignInWindow : Window
	{
		public static readonly DependencyProperty EmailProperty;

		static SignInWindow()
		{
			EmailProperty = DependencyProperty.Register(
				nameof(Email),
				typeof(string),
				typeof(SignInWindow));
		}

		public SignInWindow()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}

		public string Email
		{
			get { return (string)this.GetValue(EmailProperty); }
			set { this.SetValue(EmailProperty, value); }
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			var result = UserManager.SignIn(
				this.Email,
				this.passwordBox.Password,
				Application.Current as App);

			if (result == null)
			{
				MessageBox.Show(
						"Wrong email or password.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);

				this.DialogResult = false;
			}

			this.DialogResult = true;
			return;
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}
