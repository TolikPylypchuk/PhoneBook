using System.Windows;

using PhoneBook.DAL.Models;

namespace PhoneBook
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public User CurrentUser { get; set; }
	}
}
