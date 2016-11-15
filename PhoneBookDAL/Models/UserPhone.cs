using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("UserPhones")]
	public partial class UserPhone : PhoneBase
	{
		[Required]
		[Display(Name = "User ID")]
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
