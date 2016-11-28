using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DAL.Models
{
	public abstract partial class PhoneBase : EntityBase
	{
		[Required]
		[Phone]
		[StringLength(15)]
		public string Number { get; set; }
	}
}
