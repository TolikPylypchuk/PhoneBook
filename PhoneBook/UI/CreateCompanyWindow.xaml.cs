using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

using PhoneBook.DAL.EF;
using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;
using PhoneBook.Services;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for CreateCompanyWindow.xaml
	/// </summary>
	public partial class CreateCompanyWindow : Window
	{
		public static DependencyProperty CompanyProperty;
		public static DependencyProperty UserProperty;

		static CreateCompanyWindow()
		{
			CompanyProperty = DependencyProperty.Register(
				nameof(Company),
				typeof(Company),
				typeof(CreateCompanyWindow),
				new PropertyMetadata
				{
					DefaultValue = new Company { Address = new Address() }
				});

			UserProperty = DependencyProperty.Register(
				nameof(User),
				typeof(User),
				typeof(CreateCompanyWindow));
		}

		public CreateCompanyWindow()
		{
			this.InitializeComponent();
			this.DataContext = this.Company;
		}

		public Company Company
		{
			get { return this.GetValue(CompanyProperty) as Company; }
			set { this.SetValue(CompanyProperty, value); }
		}

		public User User
		{
			get { return this.GetValue(UserProperty) as User; }
			set { this.SetValue(UserProperty, value); }
		}

		public PhoneBookContext Context { get; set; } =
			new PhoneBookContext();
		
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			IRepository<Category> categories =
				new CategoryRepository(this.Context);

			await categories.GetAll().LoadAsync();

			this.categoryComboBox.ItemsSource = categories.LocalData;
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			this.Company.CreatedBy = this.User;
			this.SetPhones();

			var result = CompanyManager.Create(this.Company, this.Context);

			if (result == CompanyManager.CreateResult.Success)
			{
				this.DialogResult = true;
				this.Context.Dispose();
				return;
			}

			switch (result)
			{
				case CompanyManager.CreateResult.EmailAlreadyOccupied:
					MessageBox.Show(
						"The email is already occupied.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					break;
				case CompanyManager.CreateResult.NotAllPropertiesSet:
					MessageBox.Show(
						"Not all fields are filled out.",
						"Error",
						MessageBoxButton.OK,
						MessageBoxImage.Error);
					break;
				case CompanyManager.CreateResult.ValidationError:
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
				.Select(number => new CompanyPhone
				{
					Number = number,
					Company = this.Company
				});

			foreach (var phone in query)
			{
				this.Company.Phones.Add(phone);
			}
		}
	}
}
