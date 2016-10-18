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
        
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuLogInClick(object sender, RoutedEventArgs e)
        {
            //
        }

        private void MenuLogOutClick(object sender, RoutedEventArgs e)
        {
            //
        }

        private void MenuCompanyInfoClick(object sender, RoutedEventArgs e)
        {
            //
        }

        private void MenuPersonInfoClick(object sender, RoutedEventArgs e)
        {
            //
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuAboutClick(object sender, RoutedEventArgs e)
        {
            //
        }
        #endregion

        #region Other methods

        private void UpdatePeopleListBox()
		{
			IRepository<User> repo = new UserRepository();
			this.LoadPeople(repo, this.peoplePageInfo);

			this.peopleListBox.ItemsSource = repo.LocalData;
		}

		private void UpdateCompaniesListBox()
		{
			IRepository<Company> repo = new CompanyRepository();
			this.LoadCompanies(repo, this.companiesPageInfo);

			this.companiesListBox.ItemsSource = repo.LocalData;
		}

		private void LoadPeople(
			IRepository<User> repo,
			PageInfo info)
		{
			repo.GetAll()
				.OrderBy(user => user.LastName)
				.Skip((info.CurrentPage - 1) * info.EntriesPerPage)
				.Take(info.EntriesPerPage)
				.Load();
		}

		private void LoadCompanies(
			IRepository<Company> repo,
			PageInfo info)
		{
			repo.GetAll()
				.OrderBy(company => company.Name)
				.Skip((info.CurrentPage - 1) * info.EntriesPerPage)
				.Take(info.EntriesPerPage)
				.LoadAsync();
		}

        #endregion
    }
}
