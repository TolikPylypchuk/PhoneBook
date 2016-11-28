using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Users")]
	public partial class User : EntityBase
	{
		[Required]
		[EmailAddress]
		[StringLength(50)]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		[StringLength(15)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[StringLength(15)]
		[Display(Name = "Middle name")]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(15)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Required]
		[Display(Name = "Address ID")]
		public int AddressId { get; set; }

		[ForeignKey("AddressId")]
		public virtual Address Address { get; set; }

		public virtual ICollection<UserPhone> Phones { get; set; } =
			new HashSet<UserPhone>();

		[NotMapped]
		public string FullName =>
			$"{this.FirstName} {this.MiddleName} {this.LastName}";
	}
}
