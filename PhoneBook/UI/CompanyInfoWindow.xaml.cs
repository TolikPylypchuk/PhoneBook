using System.Windows;

using PhoneBook.DAL.Models;

namespace PhoneBook.UI
{
	/// <summary>
	/// Interaction logic for CompanyInfoWindow.xaml
	/// </summary>
	public partial class CompanyInfoWindow : Window
	{
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsReadOnly),
                typeof(bool),
                typeof(CompanyInfoWindow));

        public static readonly DependencyProperty CompanyProperty =
			DependencyProperty.Register(
				nameof(Company),
				typeof(Company),
				typeof(CompanyInfoWindow));

		public CompanyInfoWindow()
		{
			this.InitializeComponent();
			this.DataContext = this.Company;
		}

        public bool IsReadOnly
        {
            get { return (bool)this.GetValue(IsReadOnlyProperty); }
            set { this.SetValue(IsReadOnlyProperty, value); }
        }

        public Company Company
		{
			get { return (Company)this.GetValue(CompanyProperty); }
			set { this.SetValue(CompanyProperty, value); }
		}

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
