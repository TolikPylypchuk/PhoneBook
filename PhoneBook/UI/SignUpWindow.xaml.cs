using System;
using System.Linq;
using System.Windows;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;
using PhoneBook.Services;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for SignUpWindow.xaml
	/// </summary>
	public partial class SignUpWindow : Window
	{
		public static readonly DependencyProperty UserProperty;

		static SignUpWindow()
		{
			UserProperty = DependencyProperty.Register(
				nameof(User),
				typeof(User),
				typeof(SignUpWindow),
				new PropertyMetadata
				{
					DefaultValue = new User
					{
						IsVisible = true,
						Address = new Address()
					}
				});
		}

		public SignUpWindow()
		{
			this.InitializeComponent();
			this.DataContext = this.User;
		}

		public User User
		{
			get { return (User)this.GetValue(UserProperty); }
			set { this.SetValue(UserProperty, value); }
		}

		public PhoneBookContext Context { get; set; } =
			new PhoneBookContext();

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			this.SetPhones();
			
			if (!this.SetPassword())
			{
				MessageBox.Show(
					"Passwords are different.",
					"Error",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}

			var result = UserManager.SignUp(this.User, this.Context);
			
			if (result == UserManager.SignUpResult.Success)
			{
				try
				{
					UserManager.SignIn(
						this.User.Email,
						this.User.PasswordHash,
						Application.Current as App,
						this.Context);
				} catch
				{
					MessageBox.Show(
						"Some error occured.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
				}

				this.DialogResult = true;
				this.Context.Dispose();
				return;
			}

			switch (result)
			{
				case UserManager.SignUpResult.EmailAlreadyOccupied:
					MessageBox.Show(
						"The email is already occupied.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					break;
				case UserManager.SignUpResult.NotAllPropertiesSet:
					MessageBox.Show(
						"Not all fields are filled out.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					break;
				case UserManager.SignUpResult.ValidationError:
					MessageBox.Show(
						"Please enter a valid email and valid phone numbers.\n" +
						"Phone numbers cannot contain spaces or hyphens.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					break;
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Context.Dispose();
			this.DialogResult = false;
		}

		private void SetPhones()
		{
			var query = this.phonesTextBox.Text
				.Split(
					new[] { Environment.NewLine },
					StringSplitOptions.RemoveEmptyEntries)
				.Select(number => new UserPhone
				{
					Number = number,
					User = this.User
				});

			foreach (var phone in query)
			{
				this.User.Phones.Add(phone);
			}
		}

		private bool SetPassword()
		{
			if (this.passwordBox.Password != this.confirmPasswordBox.Password)
			{
				return false;
			}

			UserManager.SetPassword(this.User, this.passwordBox.Password);

			return true;
		}
	}
}
