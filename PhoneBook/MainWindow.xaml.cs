using System;
using System.Data.Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using PhoneBook.DAL.Repositories;

namespace PhoneBook
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			UserRepository repo = new UserRepository();

			StringBuilder message = new StringBuilder("The list of users:");

			repo.GetAll().Load();

			foreach (var user in repo.Context.Users.Local)
			{
				message.Append($"\n {user.FirstName} {user.LastName}");
			}

			MessageBox.Show(message.ToString());
		}
	}
}
