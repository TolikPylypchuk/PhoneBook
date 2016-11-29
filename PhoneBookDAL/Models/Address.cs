using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.DAL.Models
{
	[Table("Addresses")]
	public partial class Address : EntityBase
	{
		[Required]
		[StringLength(20)]
		public string Country { get; set; }

		[Required]
		[StringLength(50)]
		public string City { get; set; }

		[Required]
		[StringLength(50)]
		public string Street { get; set; }

		[Required]
		public string Number { get; set; }

		public int? Apartment { get; set; }

		[Required]
		[Display(Name = "Postal code")]
		public int PostalCode { get; set; }
	}
}
