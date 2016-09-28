using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Companies")]
	public class Company : EntityBase
	{
		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		[Required]
		[StringLength(300)]
		public string Description { get; set; }

		[StringLength(20)]
		public string Email { get; set; }

		[NotMapped]
		public double Rating { get; set; }

		public int UserId { get; set; }

		[Required]
		public int AddressId { get; set; }

		[Required]
		public int CategoryId { get; set; }

		[ForeignKey("UserId")]
		public User CreatedBy { get; set; }

		[ForeignKey("AddressId")]
		public Address Address { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; }

		public virtual ICollection<CompanyPhone> Phones { get; set; } =
			new HashSet<CompanyPhone>();
	}
}
