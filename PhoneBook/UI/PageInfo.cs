using System;
using System.Windows;

namespace PhoneBook.UI
{
	class PageInfo : DependencyObject
	{
		#region Dependency properties

		public static readonly DependencyProperty TotalEntriesProperty;
		public static readonly DependencyProperty EntriesPerPageProperty;
		public static readonly DependencyProperty CurrentPageProperty;
		public static readonly DependencyProperty TotalPagesProperty;
		public static readonly DependencyProperty IsFirstPageProperty;
		public static readonly DependencyProperty IsLastPageProperty;

		#endregion

		static PageInfo()
		{
			TotalEntriesProperty = DependencyProperty.Register(
				nameof(TotalEntries),
				typeof(int),
				typeof(PageInfo),
				new PropertyMetadata(TotalEntriesPropertyChanged));

			EntriesPerPageProperty = DependencyProperty.Register(
				nameof(EntriesPerPage),
				typeof(int),
				typeof(PageInfo),
				new PropertyMetadata(EntriesPerPagePropertyChanged));

			CurrentPageProperty = DependencyProperty.Register(
				nameof(CurrentPage),
				typeof(int),
				typeof(PageInfo),
				new PropertyMetadata(1, CurrentPagePropertyChanged));

			TotalPagesProperty = DependencyProperty.Register(
				nameof(TotalPages),
				typeof(int),
				typeof(PageInfo));

			IsFirstPageProperty = DependencyProperty.Register(
				nameof(IsFirstPage),
				typeof(bool),
				typeof(PageInfo),
				new PropertyMetadata(true, null));

			IsLastPageProperty = DependencyProperty.Register(
				nameof(IsLastPage),
				typeof(bool),
				typeof(PageInfo),
				new PropertyMetadata(false, null));
		}

		#region Properties

		public int TotalEntries
		{
			get { return (int)this.GetValue(TotalEntriesProperty); }
			set { this.SetValue(TotalEntriesProperty, value); }
		}

		public int EntriesPerPage
		{
			get { return (int)this.GetValue(EntriesPerPageProperty); }
			set { this.SetValue(EntriesPerPageProperty, value); }
		}

		public int CurrentPage
		{
			get { return (int)this.GetValue(CurrentPageProperty); }
			set { this.SetValue(CurrentPageProperty, value); }
		}

		public int TotalPages
		{
			get { return (int)this.GetValue(TotalPagesProperty); }
			set { this.SetValue(TotalPagesProperty, value); }
		}

		public bool IsFirstPage
		{
			get { return (bool)this.GetValue(IsFirstPageProperty); }
			set { this.SetValue(IsFirstPageProperty, value); }
		}

		public bool IsLastPage
		{
			get { return (bool)this.GetValue(IsLastPageProperty); }
			set { this.SetValue(IsLastPageProperty, value); }
		}

		#endregion

		#region Callbacks

		private static void TotalEntriesPropertyChanged(
			DependencyObject sender,
			DependencyPropertyChangedEventArgs e)
		{
			(sender as PageInfo)?.UpdateTotalPages();
		}

		private static void EntriesPerPagePropertyChanged(
			DependencyObject sender,
			DependencyPropertyChangedEventArgs e)
		{
			(sender as PageInfo)?.UpdateTotalPages();
		}

		private static void CurrentPagePropertyChanged(
			DependencyObject sender,
			DependencyPropertyChangedEventArgs e)
		{
			(sender as PageInfo)?.UpdateIsFirstOrLastPage();
		}

		#endregion

		#region Methods

		public void UpdateTotalPages()
		{
			this.TotalPages = (int)Math.Ceiling(
				(double)this.TotalEntries / this.EntriesPerPage);

			this.UpdateIsFirstOrLastPage();
		}

		public void UpdateIsFirstOrLastPage()
		{
			this.IsFirstPage = this.CurrentPage == 1;
			this.IsLastPage = this.TotalPages == 0 ||
				this.CurrentPage == this.TotalPages;
		}

		#endregion
	}
}
