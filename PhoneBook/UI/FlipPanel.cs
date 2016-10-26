using System.Windows;
using System.Windows.Controls;

namespace PhoneBook.UI
{
	[TemplateVisualState(Name = "Normal", GroupName = "ViewStates")]
	[TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
	public class FlipPanel : Control
	{
		public static readonly DependencyProperty FrontContentProperty;
		public static readonly DependencyProperty BackContentProperty;
		public static readonly DependencyProperty IsFlippedProperty;

		static FlipPanel()
		{
			FrontContentProperty = DependencyProperty.Register(
				nameof(FrontContent),
				typeof(object),
				typeof(FlipPanel));

			BackContentProperty = DependencyProperty.Register(
				nameof(BackContent),
				typeof(object),
				typeof(FlipPanel));

			IsFlippedProperty = DependencyProperty.Register(
				nameof(IsFlipped),
				typeof(bool),
				typeof(FlipPanel),
				new PropertyMetadata(
					(sender, e) => (sender as FlipPanel)?.ChangeState()));

			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(FlipPanel),
				new FrameworkPropertyMetadata(typeof(FlipPanel)));
		}
		
		public object FrontContent
		{
			get { return this.GetValue(FrontContentProperty); }
			set { this.SetValue(FrontContentProperty, value); }
		}

		public object BackContent
		{
			get { return this.GetValue(BackContentProperty); }
			set { this.SetValue(BackContentProperty, value); }
		}

		public bool IsFlipped
		{
			get { return (bool)this.GetValue(IsFlippedProperty); }
			set { this.SetValue(IsFlippedProperty, value); }
		}

		public void Flip()
		{
			this.IsFlipped = !this.IsFlipped;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.ChangeState();
		}

		private void ChangeState()
		{
			VisualStateManager.GoToState(
				this,
				this.IsFlipped ? "Flipped" : "Normal",
				false);

			UIElement front = FrontContent as UIElement;
			if (front != null)
			{
				front.Visibility = this.IsFlipped
					? Visibility.Hidden
					: Visibility.Visible;
			}

			UIElement back = BackContent as UIElement;
			if (back != null)
			{
				back.Visibility = this.IsFlipped
					? Visibility.Visible
					: Visibility.Hidden;
			}
		}
	}
}
