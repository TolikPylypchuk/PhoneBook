using System.Windows;

using PhoneBook.DAL.Models;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for CompanyInfoWindow.xaml
	/// </summary>
	public partial class CompanyInfoWindow : Window
	{
		public static readonly DependencyProperty CompanyProperty =
			DependencyProperty.Register(
				nameof(CompanyInfoData),
				typeof(Company),
				typeof(CompanyInfoWindow));

		public CompanyInfoWindow()
		{
			this.InitializeComponent();
		}

		public Company CompanyInfoData
		{
			get { return (Company)this.GetValue(CompanyProperty); }
			set { this.SetValue(CompanyProperty, value); }
		}
	}
}
