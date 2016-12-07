using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using PhoneBook.DAL.Models;
using PhoneBook.DAL.Repositories;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Fields

		App currentApp = Application.Current as App;

		private PageInfo peoplePageInfo = new PageInfo
		{
			EntriesPerPage = 10
		};

		private PageInfo companiesPageInfo = new PageInfo
		{
			EntriesPerPage = 10
		};

		#endregion

		public MainWindow()
		{
			this.InitializeComponent();
		}

		public bool IsPersonalInfoContextMenuItemVisible
		{
			get
			{
				User selectedUser = (User)this.peopleListView.SelectedItem;
				return (selectedUser != null &&
					this.currentApp.CurrentUser.Id == selectedUser.Id);
			}
		}

		#region Event handlers

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			IRepository<User> repoPeople = new UserRepository();
			IRepository<Company> repoCompanies = new CompanyRepository();

			this.peoplePageInfo.TotalEntries =
				await repoPeople.GetAll().CountAsync();

			this.companiesPageInfo.TotalEntries =
				await repoCompanies.GetAll().CountAsync();
		}

		private async void TabControl_SelectionChanged(
			object sender,
			SelectionChangedEventArgs e)
		{
			if (!(e.Source is TabControl))
			{
				return;
			}

			TabItem item = this.entriesTabControl.SelectedItem as TabItem;

			if (item == this.peopleTabItem)
			{
				this.entriesPanel.DataContext = this.peoplePageInfo;

				await this.UpdatePeopleListBoxAsync();
			} else if (item == this.companiesTabItem)
			{
				this.entriesPanel.DataContext = this.companiesPageInfo;

				await this.UpdateCompaniesListBoxAsync();
			}
		}

		private async void previousPage_Click(
			object sender,
			RoutedEventArgs e)
		{
			if (this.entriesTabControl.SelectedItem ==
				this.peopleTabItem)
			{
				this.peoplePageInfo.CurrentPage--;
				await this.UpdatePeopleListBoxAsync();
			} else if (this.entriesTabControl.SelectedItem ==
				this.companiesTabItem)
			{
				this.companiesPageInfo.CurrentPage--;
				await this.UpdateCompaniesListBoxAsync();
			}
		}

		private async void nextPage_Click(
			object sender,
			RoutedEventArgs e)
		{
			if (this.entriesTabControl.SelectedItem ==
				this.peopleTabItem)
			{
				this.peoplePageInfo.CurrentPage++;
				await this.UpdatePeopleListBoxAsync();
			} else if (this.entriesTabControl.SelectedItem ==
				this.companiesTabItem)
			{
				this.companiesPageInfo.CurrentPage++;
				await this.UpdateCompaniesListBoxAsync();
			}
		}

		private void MenuSignInClick(object sender, RoutedEventArgs e)
		{
			var result = new SignInWindow { Owner = this }.ShowDialog();

			if (result != true)
			{
				return;
			}

			this.Title =
				this.currentApp.CurrentUser.FullName + " - " + this.Title;

			this.signInMenuItem.Visibility = Visibility.Collapsed;
			this.signUpMenuItem.Visibility = Visibility.Collapsed;

			this.signOutMenuItem.Visibility = Visibility.Visible;
			this.personInfoMenuItem.Visibility = Visibility.Visible;
		}

		private async void MenuSignUpClick(object sender, RoutedEventArgs e)
		{
			var result = new SignUpWindow { Owner = this }.ShowDialog();

			if (result != true)
			{
				return;
			}

			this.Title =
				this.currentApp.CurrentUser.FullName + " - " + this.Title;

			this.signInMenuItem.Visibility = Visibility.Collapsed;
			this.signUpMenuItem.Visibility = Visibility.Collapsed;

			this.signOutMenuItem.Visibility = Visibility.Visible;
			this.personInfoMenuItem.Visibility = Visibility.Visible;

			TabItem item = this.entriesTabControl.SelectedItem as TabItem;

			if (item == this.peopleTabItem)
			{
				this.entriesPanel.DataContext = this.peoplePageInfo;

				await this.UpdatePeopleListBoxAsync();
			} else if (item == this.companiesTabItem)
			{
				this.entriesPanel.DataContext = this.companiesPageInfo;

				await this.UpdateCompaniesListBoxAsync();
			}
		}

		private void MenuSignOutClick(object sender, RoutedEventArgs e)
		{
			this.currentApp.CurrentUser = null;

			this.Title = this.Title.Split('-')[1].Trim();

			this.signInMenuItem.Visibility = Visibility.Visible;
			this.signUpMenuItem.Visibility = Visibility.Visible;

			this.signOutMenuItem.Visibility = Visibility.Collapsed;
			this.personInfoMenuItem.Visibility = Visibility.Collapsed;
		}

		private void MenuCompanyInfoClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not implemented!", "Error");
        }

		private void MenuPersonInfoClick(object sender, RoutedEventArgs e)
		{
			this.openPersonInfoWindow(this.currentApp.CurrentUser, false);
        }

		private void MenuExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void MenuAboutClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show(
				"The phone book can be used " +
				"to find addresses and phones of people and companies.",
				"Info");            
        }
		
		private async void peopleFindButton_Click(
			object sender,
			RoutedEventArgs e)
		{
			this.peoplePageInfo.CurrentPage = 1;
			await this.UpdatePeopleListBoxAsync();
		}

		private async void companiesFindButton_Click(
			object sender, RoutedEventArgs e)
		{
			this.companiesPageInfo.CurrentPage = 1;
			await this.UpdateCompaniesListBoxAsync();
		}
		
		private void peopleListView_MouseDoubleClick(
			object sender,
			MouseButtonEventArgs e)
		{
			this.openPersonInfoWindow(
				this.peopleListView.SelectedItem as User,
				true);
		}

		private void companiesListView_MouseDoubleClick(
			object sender,
			MouseButtonEventArgs e)
		{
			this.openCompanyInfoWindow();
		}

		private void UserFilter_TextChanged(
			object sender,
			TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(peopleListView.ItemsSource)
				.Refresh();
		}

		private void CompanyFilter_TextChanged(
			object sender,
			TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(companiesListView.ItemsSource)
				.Refresh();
		}
		
		private void personSeeMoreMenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.openPersonInfoWindow(this.peopleListView.SelectedItem as User, true);
		}

		private void companySeeMoreMenuItem_Click(object sender, RoutedEventArgs e)
		{
			this.openCompanyInfoWindow();
		}

		#endregion

		#region Other methods

		private async Task UpdatePeopleListBoxAsync()
		{
			string firstName = this.UserFirstNameFilter.Text;
			string middleName = this.UserMiddleNameFilter.Text;
			string lastName = this.UserLastNameFilter.Text;

			IRepository<User> repo = new UserRepository();
			await this.LoadPeopleAsync(
				repo,
				this.peoplePageInfo,
				firstName,
				middleName,
				lastName);

			this.peopleListView.ItemsSource = repo.LocalData;
            CollectionView view =
				(CollectionView)CollectionViewSource.GetDefaultView(
					peopleListView.ItemsSource);
            view.Filter = UserFilter;
        }

		private async Task UpdateCompaniesListBoxAsync()
		{
			string name = this.CompanyNameFilter.Text;
			
			IRepository<Company> repo = new CompanyRepository();
			await this.LoadCompaniesAsync(
				repo,
				this.companiesPageInfo,
				name,
				0,
				5);

			this.companiesListView.ItemsSource = repo.LocalData;
            CollectionView view =
				(CollectionView)CollectionViewSource.GetDefaultView(
					companiesListView.ItemsSource);
            view.Filter = CompanyFilter;
        }
		
		private async Task LoadPeopleAsync(
			IRepository<User> repo,
			PageInfo info,
			string firstName,
			string middleName,
			string lastName)
		{
			await repo.GetAll()
					  .Where(user =>
						user.IsVisible &&
						(String.IsNullOrEmpty(firstName) ||
						user.FirstName.Contains(firstName)) &&
						(String.IsNullOrEmpty(middleName) ||
						user.MiddleName.Contains(middleName)) &&
						(String.IsNullOrEmpty(lastName) ||
						user.LastName.Contains(lastName)))
					  .OrderBy(user => user.LastName)
					  .Skip((info.CurrentPage - 1) * info.EntriesPerPage)
					  .Take(info.EntriesPerPage)
					  .LoadAsync();
		}
		
		private async Task LoadCompaniesAsync(
			IRepository<Company> repo,
			PageInfo info,
			string name,
			double minRating,
			double maxRating)
		{
			await repo.GetAll()
					  .Where(company =>
						(String.IsNullOrEmpty(name) ||
						company.Name.Contains(name)))
					  .OrderBy(company => company.Name)
					  .Skip((info.CurrentPage - 1) * info.EntriesPerPage)
					  .Take(info.EntriesPerPage)
					  .LoadAsync();
		}

        private bool UserFilter(object item)
        {
			User user = item as User;

			if (user == null)
			{
				return false;
			}

			if (String.IsNullOrEmpty(this.UserFirstNameFilter.Text) &&
				String.IsNullOrEmpty(this.UserMiddleNameFilter.Text) &&
				String.IsNullOrEmpty(this.UserLastNameFilter.Text) &&
				String.IsNullOrEmpty(this.UserEmailFilter.Text) &&
				String.IsNullOrEmpty(this.UserPhoneFilter.Text))
			{
				return true;
			} else
			{
				bool anyPhones = user.Phones.Any(
					t => t.Number.IndexOf(
						this.UserPhoneFilter.Text,
						StringComparison.OrdinalIgnoreCase) >= 0);

				return user.FirstName.IndexOf(
							this.UserFirstNameFilter.Text,
							StringComparison.OrdinalIgnoreCase) >= 0 &&
					   user.MiddleName.IndexOf(
						   this.UserMiddleNameFilter.Text,
						   StringComparison.OrdinalIgnoreCase) >= 0 &&
					   user.LastName.IndexOf(
						   this.UserLastNameFilter.Text,
						   StringComparison.OrdinalIgnoreCase) >= 0 &&
					   user.Email.IndexOf(
						   this.UserEmailFilter.Text,
						   StringComparison.OrdinalIgnoreCase) >= 0 &&
					   anyPhones;
			}
        }

        private bool CompanyFilter(object item)
        {
			Company company = item as Company;

			if (company == null)
			{
				return false;
			}

			if (String.IsNullOrEmpty(this.CompanyNameFilter.Text) &&
				String.IsNullOrEmpty(this.CompanyEmailFilter.Text) &&
				String.IsNullOrEmpty(this.CompanyPhoneFilter.Text))
			{
				return true;
			} else
			{
				bool anyPhones = company.Phones.Any(
					t => t.Number.IndexOf(
						this.CompanyPhoneFilter.Text,
						StringComparison.OrdinalIgnoreCase) >= 0);

				return company.Name.IndexOf(
							this.CompanyNameFilter.Text,
							StringComparison.OrdinalIgnoreCase) >= 0 &&
					   company.Email.IndexOf(
						   this.CompanyEmailFilter.Text,
						   StringComparison.OrdinalIgnoreCase) >= 0 &&
					   anyPhones;
			}
        }

		private void openPersonInfoWindow(User user, bool isReadOnly)
		{
			new PersonInfoWindow
			{
				Person = user,
				IsReadOnly = isReadOnly,
				Owner = this
			}.ShowDialog();
		}

		private void openCompanyInfoWindow()
		{
			new CompanyInfoWindow
			{
				Company = this.companiesListView.SelectedItem as Company,
				Owner = this
			}.ShowDialog();
		}

		#endregion
	}
}
