using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Users")]
	public class User : EntityBase
	{
		[Required]
		[StringLength(50)]
		public string Email { get; set; }

		[StringLength(50)]
		public string Password { get; set; }

		[Required]
		[StringLength(15)]
		public string FirstName { get; set; }

		[StringLength(15)]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(15)]
		public string LastName { get; set; }

		[Required]
		public int AddressId { get; set; }

		[ForeignKey("AddressId")]
		public Address Address { get; set; }

		public virtual ICollection<UserPhone> Phones { get; set; } =
			new HashSet<UserPhone>();
	}
}
