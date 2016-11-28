namespace PhoneBook.DAL.Models
{
	public abstract partial class PhoneBase
	{
		public override string ToString() => this.Number;
	}
}
