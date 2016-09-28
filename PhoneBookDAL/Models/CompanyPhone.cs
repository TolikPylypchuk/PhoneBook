using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("CompanyPhones")]
	public class CompanyPhone : EntityBase
	{
		[Required]
		[StringLength(15)]
		public string Number { get; set; }

		[Required]
		public int CompanyId { get; set; }

		[ForeignKey("CompanyId")]
		public Company Company { get; set; }
	}
}
