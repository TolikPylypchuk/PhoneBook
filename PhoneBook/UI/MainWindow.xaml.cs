using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

		#region Event handlers

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			IRepository<User> repoPeople = new UserRepository();
			IRepository<Company> repoCompanies = new CompanyRepository();
			
			this.individualInfoFlipPanel.Flip();
			this.filterFlipPanel.Flip();

			this.peoplePageInfo.TotalEntries =
				await repoPeople.GetAll().CountAsync();

			this.companiesPageInfo.TotalEntries =
				await repoCompanies.GetAll().CountAsync();
		}

		private void TabControl_SelectionChanged(
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

				this.UpdatePeopleListBox();
			} else if (item == this.companiesTabItem)
			{
				this.entriesPanel.DataContext = this.companiesPageInfo;

				this.UpdateCompaniesListBox();
			}

			this.individualInfoFlipPanel.Flip();
			this.filterFlipPanel.Flip();
		}

		private void previousPage_Click(
			object sender,
			RoutedEventArgs e)
		{
			if (this.entriesTabControl.SelectedItem ==
				this.peopleTabItem)
			{
				this.peoplePageInfo.CurrentPage--;
				this.UpdatePeopleListBox();
			} else if (this.entriesTabControl.SelectedItem ==
				this.companiesTabItem)
			{
				this.companiesPageInfo.CurrentPage--;
				this.UpdateCompaniesListBox();
			}
		}

		private void nextPage_Click(
			object sender,
			RoutedEventArgs e)
		{
			if (this.entriesTabControl.SelectedItem ==
				this.peopleTabItem)
			{
				this.peoplePageInfo.CurrentPage++;
				this.UpdatePeopleListBox();
			} else if (this.entriesTabControl.SelectedItem ==
				this.companiesTabItem)
			{
				this.companiesPageInfo.CurrentPage++;
				this.UpdateCompaniesListBox();
			}
		}

		private void MenuLogInClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not implemented!", "Error");
        }

        private void MenuLogOutClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not implemented!", "Error");
        }

		private void MenuCompanyInfoClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not implemented!", "Error");
        }

		private void MenuPersonInfoClick(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not implemented!", "Error");
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
		
		private void peopleFindButton_Click(
			object sender,
			RoutedEventArgs e)
		{
			this.peoplePageInfo.CurrentPage = 1;
			this.UpdatePeopleListBox();
		}

		private void companiesFindButton_Click(
			object sender, RoutedEventArgs e)
		{
			this.companiesPageInfo.CurrentPage = 1;
			this.UpdateCompaniesListBox();
		}
		
		private void peopleClearFiltersButton_Click(
			object sender,
			RoutedEventArgs e)
		{
			this.firstNameTextBox.Text = string.Empty;
			this.middleNameTextBox.Text = string.Empty;
			this.lastNameTextBox.Text = string.Empty;
		}

		private void companiesClearFiltersButton_Click(
			object sender,
			RoutedEventArgs e)
		{
			this.companyNameTextBox.Text = string.Empty;
			this.minRatingTextBox.Text = string.Empty;
			this.maxRatingTextBox.Text = string.Empty;
		}
		#endregion

		#region Other methods

		private void UpdatePeopleListBox()
		{
			string firstName = this.firstNameTextBox.Text;
			string middleName = this.middleNameTextBox.Text;
			string lastName = this.lastNameTextBox.Text;

			IRepository<User> repo = new UserRepository();
			this.LoadPeople(
				repo,
				this.peoplePageInfo,
				firstName,
				middleName,
				lastName);

			this.peopleListView.ItemsSource = repo.LocalData;
		}

		private void UpdateCompaniesListBox()
		{
			string name = this.companyNameTextBox.Text;
			double minRating = 0.0;
			double maxRating = 5.0;

			bool isInputValid = this.ValidateInput(
				ref minRating,
				ref maxRating);

			if (!isInputValid)
			{
				MessageBox.Show("Wrong input.", "Error");
			}
			
			IRepository<Company> repo = new CompanyRepository();
			this.LoadCompanies(
				repo,
				this.companiesPageInfo,
				name,
				minRating,
				maxRating);

			this.companiesListView.ItemsSource = repo.LocalData;
		}
		
		private void LoadPeople(
			IRepository<User> repo,
			PageInfo info,
			string firstName,
			string middleName,
			string lastName)
		{
			repo.GetAll()
				.Where(user =>
					(String.IsNullOrEmpty(firstName) ||
						user.FirstName.Contains(firstName)) &&
					(String.IsNullOrEmpty(middleName) ||
						user.MiddleName.Contains(middleName)) &&
					(String.IsNullOrEmpty(lastName) ||
						user.LastName.Contains(lastName)))
				.OrderBy(user => user.LastName)
				.Skip((info.CurrentPage - 1) * info.EntriesPerPage)
				.Take(info.EntriesPerPage)
				.Load();
		}
		
		private void LoadCompanies(
			IRepository<Company> repo,
			PageInfo info,
			string name,
			double minRating,
			double maxRating)
		{
			repo.GetAll()
				.Where(company =>
					(String.IsNullOrEmpty(name) ||
						company.Name.Contains(name)) &&
					(company.Rating > minRating) &&
					(company.Rating < maxRating))
				.OrderBy(company => company.Name)
				.Skip((info.CurrentPage - 1) * info.EntriesPerPage)
				.Take(info.EntriesPerPage)
				.LoadAsync();
		}

		private bool ValidateInput(
			ref double minRating,
			ref double maxRating)
		{
			bool isInputValid = true;

			if (this.minRatingTextBox.Text != string.Empty)
			{
				if (!double.TryParse(this.minRatingTextBox.Text, out minRating))
				{
					isInputValid = false;
					this.minRatingTextBox.Text = string.Empty;
				}
				else
				{
					if (minRating < 0.0 || minRating > 5.0)
					{
						isInputValid = false;
						this.minRatingTextBox.Text = string.Empty;
					}
				}
			}

			if (this.maxRatingTextBox.Text != string.Empty)
			{
				if (!double.TryParse(this.maxRatingTextBox.Text, out maxRating))
				{
					isInputValid = false;
					this.maxRatingTextBox.Text = string.Empty;
				}
				else
				{
					if (maxRating < 0.0 || maxRating > 5.0)
					{
						isInputValid = false;
						this.maxRatingTextBox.Text = string.Empty;
					}
				}
			}

			if (minRating > maxRating)
			{
				isInputValid = false;
				this.minRatingTextBox.Text = string.Empty;
				this.maxRatingTextBox.Text = string.Empty;
			}

			return isInputValid;
		}
		#endregion
	}
}
