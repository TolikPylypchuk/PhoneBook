using System.Linq;
using System.Windows;
using System.Windows.Input;

using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for MyCompaniesWindow.xaml
	/// </summary>
	public partial class MyCompaniesWindow : Window
	{
		public static readonly DependencyProperty PersonProperty =
			DependencyProperty.Register(
				nameof(Person),
				typeof(User),
				typeof(PersonInfoWindow));

		public MyCompaniesWindow()
		{
			InitializeComponent();
		}
		
		public User Person
		{
			get { return (User)this.GetValue(PersonProperty); }
			set { this.SetValue(PersonProperty, value); }
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.LoadMyCompanies();
		}

		private void myCompaniesListView_MouseDoubleClick(
			object sender, 
			MouseButtonEventArgs e)
		{
			this.OpenCompanyInfoWindow(
				this.myCompaniesListView.SelectedItem as Company,
				true);
		}

		private void companySeeMoreMenuItem_Click(
			object sender, 
			RoutedEventArgs e)
		{
			this.OpenCompanyInfoWindow(
				this.myCompaniesListView.SelectedItem as Company,
				true);
		}

		private void companyEditMenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.OpenCompanyInfoWindow(
				this.myCompaniesListView.SelectedItem as Company,
				false);
		}

		private void companyDeleteMenuItem_Click(
			object sender,
			RoutedEventArgs e)
		{
			var result = MessageBox.Show(
				"Do you really want to delete this your company?",
				"Delete company",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question);

			if (result == MessageBoxResult.No)
			{
				return;
			}

			var companyToDelete =
				this.myCompaniesListView.SelectedItem as Company;

			new CompanyRepository().Delete(companyToDelete);

			MessageBox.Show(
				"Your company was successfully deleted.",
				"Company deleted.",
				MessageBoxButton.OK,
				MessageBoxImage.Information);

			this.LoadMyCompanies();
		}

		private void OpenCompanyInfoWindow(Company company, bool isReadOnly)
		{
			if (company != null)
			{
				new CompanyInfoWindow
				{
					Company = company,
					IsReadOnly = isReadOnly,
					Owner = this
				}.ShowDialog();
			}
		}

		private void LoadMyCompanies()
		{
			var companies = new CompanyRepository()
				.GetAll()
				.Where(
					c => c.CreatedBy.Id == this.Person.Id);

			foreach (var company in companies)
			{
				this.myCompaniesListView.Items.Add(company);
			}
		}
	}
}
